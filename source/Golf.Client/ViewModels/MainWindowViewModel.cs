using System;
using System.Collections.ObjectModel;
using System.Reactive.Linq;
using System.Windows.Controls;
using System.Windows.Threading;
using Golf.Client.Views;
using Golf.Core;
using Golf.Core.Events;

namespace Golf.Client.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        readonly IObservable<IGameEvent> _events;
        readonly IViewController _viewController;
        ShotControlView _shotControlView;

        public MainWindowViewModel(IGameEngine gameEngine, IObservable<IGameEvent> events, IViewController viewController) {
            _events = events;
            _viewController = viewController;
            GameEngine = gameEngine;
        }

        public ObservableCollection<UserControl> SurfaceItems {
            get { return _viewController.Views; }
        }

        public IGameEngine GameEngine { get; private set; }

        public void Initialize() {
            GameEngine.Initialize();

            _events.OfType<ShotComplete>().ObserveOnDispatcher().Subscribe(e => AddShotControl());

            AddShotControl();
        }

        void AddShotControl() {
            if (_shotControlView != null) return;
            Dispatcher.CurrentDispatcher.Invoke((Action) (() => {
                                                              _shotControlView = new ShotControlView();
                                                              _shotControlView.DataContext = new ShotControlViewModel {
                                                                                                                          PlayersBall
                                                                                                                              =
                                                                                                                              GameEngine
                                                                                                                              .
                                                                                                                              PlayersBall,
                                                                                                                          Hit
                                                                                                                              =
                                                                                                                              new ActionCommand
                                                                                                                              (OnPlayerHit)
                                                                                                                      };
                                                              SurfaceItems.Add(_shotControlView);
                                                          }));
        }

        void OnPlayerHit() {
            if (_shotControlView == null) return;
            var model = (ShotControlViewModel) _shotControlView.DataContext;
            SurfaceItems.Remove(_shotControlView);
            _shotControlView = null;
            GameEngine.PlayShot(model.PowerX, model.PowerY);
        }
    }
}