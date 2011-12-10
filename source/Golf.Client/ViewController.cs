using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Reactive.Linq;
using System.Windows.Controls;
using Golf.Client.ViewModels;
using Golf.Client.Views;
using Golf.Core;
using Golf.Core.Events;
using Golf.Core.GameObjects;

namespace Golf.Client
{
    public class ViewController : IViewController
    {
        readonly IGameEngine _gameEngine;

        public ViewController(IGameEngine gameEngine) {
            _gameEngine = gameEngine;
            _gameEngine.Events.OfType<IGameObjectCreated<object>>().Subscribe(OnGameObjectCreated);
            Views = new ObservableCollection<UserControl>();
        }

        #region IViewController Members

        public ObservableCollection<UserControl> Views { get; private set; }
        
        #endregion

        void OnGameObjectCreated(IGameObjectCreated<object> e) {
            if (e is GameObjectCreated<GolfBall>) {
                var ball = ((GameObjectCreated<GolfBall>) e).GameObject;
                var view = new GolfBallView();
                var viewModel = new GolfBallViewModel {Model = ball};
                view.DataContext = viewModel;
                Views.Add(view);
            }
        }
    }
}