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
            var shotControlView =
                new ShotControlView {
                                        DataContext = new ShotControlModel {
                                                                               PlayersBall =
                                                                                   GameEngine.
                                                                                   PlayersBall,
                                                                               Hit =
                                                                                   new ActionCommand(
                                                                                   () =>
                                                                                   GameEngine.Start())
                                                                           }
                                    };
            SurfaceItems.Add(shotControlView);
        }
    }
}