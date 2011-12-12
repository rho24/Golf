using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Reactive.Linq;
using System.Windows.Controls;
using Golf.Client.ViewModels;
using Golf.Client.Views;
using Golf.Core.Events;
using Golf.Core.GameObjects;

namespace Golf.Client
{
    public class ViewController : IViewController
    {
        readonly ICollection<ViewModelBase> _viewModels;

        public ViewController(IObservable<IGameEvent> events) {
            events.Where(e => e is Tick || e is PositionChanged).ObserveOnDispatcher().Subscribe(e => UpdatePositions());
            events.OfType<GameObjectAdded>().ObserveOnDispatcher().Subscribe(OnGameObjectAdded);
            Views = new ObservableCollection<UserControl>();
            _viewModels = new List<ViewModelBase>();
        }

        #region IViewController Members

        public ObservableCollection<UserControl> Views { get; private set; }

        #endregion

        void UpdatePositions() {
            foreach (var model in _viewModels) {
                model.UpdatePosition();
            }
        }

        void OnGameObjectAdded(GameObjectAdded e) {
            if (e.GameObject is GolfBall) {
                var ball = (GolfBall) e.GameObject;
                var view = new GolfBallView();
                var viewModel = new GolfBallViewModel {Model = ball};
                view.DataContext = viewModel;
                Views.Add(view);
                _viewModels.Add(viewModel);
            }
        }
    }
}