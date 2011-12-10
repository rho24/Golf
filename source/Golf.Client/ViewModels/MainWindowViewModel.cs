using System;
using System.Collections.ObjectModel;
using System.Windows.Controls;
using Golf.Client.Views;
using Golf.Core;

namespace Golf.Client.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        readonly IViewController _viewController;
        ShotControlView _shotControlView;

        public MainWindowViewModel(IGameEngine gameEngine, IViewController viewController) {
            _viewController = viewController;
            GameEngine = gameEngine;
        }

        public ObservableCollection<UserControl> SurfaceItems {
            get { return _viewController.Views; }
        }

        public IGameEngine GameEngine { get; private set; }

        public void Initialize() {
            GameEngine.Initialize();

            AddShotControl();
        }

        void AddShotControl() {
            if (_shotControlView != null) return;
            _shotControlView = new ShotControlView();
            _shotControlView.DataContext = new ShotControlViewModel {
                                                                    PlayersBall = GameEngine.PlayersBall,
                                                                    Hit = new ActionCommand(OnPlayerHit)
                                                                };
            SurfaceItems.Add(_shotControlView);
        }

        void OnPlayerHit() {
            if (_shotControlView == null) return;
            var model = (ShotControlViewModel)_shotControlView.DataContext;
            SurfaceItems.Remove(_shotControlView);
            _shotControlView = null;
            GameEngine.PlayShot(model.PowerX, model.PowerY);
        }
    }
}