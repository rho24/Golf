using System;
using System.Windows;
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
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e) {
            var kernel = InitialiseNinject();

            var mainWindow = kernel.Get<MainWindow>();


            mainWindow.Events.Initialize(mainWindow.GameEngine);

            mainWindow.Show();

            base.OnStartup(e);
        }

        IKernel InitialiseNinject() {
            var kernel = new StandardKernel();
            kernel.Bind<IGameEngine>().To<GameEngine>();
            kernel.Bind<IPhysicsEngine>().To<PhysicsEngine>();
            kernel.Bind<IEventManager>().To<EventManager>().InSingletonScope();
            kernel.Bind<IEventAggregator>().To<IEventManager>();

            return kernel;
        }
    }
}