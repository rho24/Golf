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
        readonly ICollection<ViewModelBase> _viewModels;

        public ViewController(IGameEngine gameEngine) {
            _gameEngine = gameEngine;
            _gameEngine.Events.OfType<TickEvent>().Subscribe(OnTick);
            _gameEngine.Events.OfType<IGameObjectCreated<object>>().Subscribe(OnGameObjectCreated);
            Views = new ObservableCollection<UserControl>();
            _viewModels = new List<ViewModelBase>();
        }

        #region IViewController Members

        public ObservableCollection<UserControl> Views { get; private set; }
        
        #endregion

        void OnTick(TickEvent e) {
            foreach (var model in _viewModels) {
                model.UpdatePosition();
            }
        }

        void OnGameObjectCreated(IGameObjectCreated<object> e) {
            if (e is GameObjectCreated<GolfBall>) {
                var ball = ((GameObjectCreated<GolfBall>) e).GameObject;
                var view = new GolfBallView();
                var viewModel = new GolfBallViewModel {Model = ball};
                view.DataContext = viewModel;
                Views.Add(view);
                _viewModels.Add(viewModel);
            }
        }
    }
}