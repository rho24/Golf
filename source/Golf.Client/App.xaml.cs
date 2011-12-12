using System;
using System.Windows;
using Golf.Client.ViewModels;
using Golf.Core;
using Golf.Core.Events;
using Golf.Core.Physics;
using Ninject;
using EventManager = Golf.Core.Events.EventManager;

namespace Golf.Client
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App
    {
        protected override void OnStartup(StartupEventArgs e) {
            var kernel = InitialiseNinject();
            var mainWindow = kernel.Get<MainWindow>();
            var viewModel = kernel.Get<MainWindowViewModel>();
            mainWindow.DataContext = viewModel;
            viewModel.Initialize();
            mainWindow.Events.Initialize(kernel.Get<IObservable<IGameEvent>>());

            mainWindow.Show();

            base.OnStartup(e);
        }

        IKernel InitialiseNinject() {
            var kernel = new StandardKernel();
            kernel.Bind<IGameEngine>().To<GameEngine>().InSingletonScope();

            kernel.Bind<EventManager>().ToSelf().InSingletonScope();
            kernel.Bind<IEventTriggerer>().ToMethod(c => c.Kernel.Get<EventManager>());
            kernel.Bind<IPhysicsEngine>().To<PhysicsEngine>();
            kernel.Bind<IObservable<IGameEvent>>().ToMethod(c => c.Kernel.Get<EventManager>().Events);

            kernel.Bind<IViewController>().To<ViewController>();

            return kernel;
        }
    }
}