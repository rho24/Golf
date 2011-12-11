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
            _gameEngine.Events.OfType<ShouldRender>().ObserveOnDispatcher().Subscribe(OnTick);
            _gameEngine.Events.OfType<IGameObjectCreated>().ObserveOnDispatcher().Subscribe(OnGameObjectCreated);
            Views = new ObservableCollection<UserControl>();
            _viewModels = new List<ViewModelBase>();
        }

        #region IViewController Members

        public ObservableCollection<UserControl> Views { get; private set; }
        
        #endregion

        void OnTick(ShouldRender e) {
            foreach (var model in _viewModels) {
                model.UpdatePosition();
            }
        }

        void OnGameObjectCreated(IGameObjectCreated e) {
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