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
            events.OfType<ShouldRender>().ObserveOnDispatcher().Subscribe(OnTick);
            events.OfType<IAddGameObjectRequest>().ObserveOnDispatcher().Subscribe(OnGameObjectCreated);
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

        void OnGameObjectCreated(IAddGameObjectRequest e) {
            if (e is AddGameObjectRequest<GolfBall>) {
                var ball = ((AddGameObjectRequest<GolfBall>) e).GameObject;
                var view = new GolfBallView();
                var viewModel = new GolfBallViewModel {Model = ball};
                view.DataContext = viewModel;
                Views.Add(view);
                _viewModels.Add(viewModel);
            }
        }
    }
}