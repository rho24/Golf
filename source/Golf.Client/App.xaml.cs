using System;
using System.Reflection;
using System.Windows;
using Golf.Client.ViewModels;
using Golf.Core;
using Golf.Core.Events;
using Golf.Core.Physics;
using Golf.Core.Physics.Surfaces;
using Ninject;
using EventManager = Golf.Core.EventManager;

namespace Golf.Client
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App
    {
        protected override void OnStartup(StartupEventArgs e) {
            FixStartupUri();

            var kernel = InitialiseNinject();
            var mainWindow = kernel.Get<MainWindow>();
            var viewModel = kernel.Get<MainWindowViewModel>();
            mainWindow.DataContext = viewModel;
            mainWindow.Events.Initialize(kernel.Get<IObservable<IGameEvent>>());
            viewModel.Initialize();

            mainWindow.Show();

            base.OnStartup(e);
        }

        IKernel InitialiseNinject() {
            var kernel = new StandardKernel();
            kernel.Bind<EventManager>().ToSelf().InSingletonScope();

            kernel.Bind<IGameEngine>().To<GameEngine>();
            kernel.Bind<IEventTriggerer>().ToMethod(c => c.Kernel.Get<EventManager>());
            kernel.Bind<IPhysicsEngine>().To<PhysicsEngine>();
            kernel.Bind<ISurfaceManager>().To<SurfaceManager>();
            kernel.Bind<IObservable<IGameEvent>>().ToMethod(c => c.Kernel.Get<EventManager>().Events);

            kernel.Bind<IViewController>().To<ViewController>();

            return kernel;
        }

        // Dangit blend! Stop inserting a stupid StartupUri    
        void FixStartupUri() {
            var type = typeof (Application);
            var startupUri = type.GetField("_startupUri", BindingFlags.Public
                                                          | BindingFlags.NonPublic
                                                          | BindingFlags.Instance);
            startupUri.SetValue(this, null);
        }
    }
}