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
using Golf.Core.Physics.Surfaces;

namespace Golf.Client
{
    public class ViewController : IViewController
    {
        readonly ICollection<ViewModelBase> _viewModels;

        public ViewController(IObservable<IGameEvent> events) {
            events.Where(e => e is Tick || e is PositionChanged).ObserveOnDispatcher().Subscribe(e => UpdatePositions());
            events.OfType<SurfaceAdded>().ObserveOnDispatcher().Subscribe(OnSurfaceAdded);
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

        void OnSurfaceAdded(SurfaceAdded e) {
            if (e.Surface is RectangleSurface) {
                var surface = (RectangleSurface) e.Surface;
                var view = new RectangleSurfaceView();
                var viewModel = new RectangleSurfaceViewModel {Model = surface};
                view.DataContext = viewModel;
                Views.Add(view);
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