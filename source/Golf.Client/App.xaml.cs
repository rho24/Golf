using System;
using System.Diagnostics;
using System.Reactive.Linq;
using System.Windows;
using System.Windows.Browser;
using System.Windows.Controls;
using Golf.Core;
using Golf.Core.Events;
using Golf.Core.Physics;
using Ninject;

namespace Golf.Client
{
    public partial class App : Application
    {
        public App() {
            Startup += Application_Startup;
            Exit += Application_Exit;
            UnhandledException += Application_UnhandledException;

            InitializeComponent();
        }

        void Application_Startup(object sender, StartupEventArgs e) {
            var kernel = NinjectBootstrap();

            var mainPage = new MainPage();
            var gameEngine = kernel.Get<IGameEngine>();

            gameEngine.Events.OfType<GameObjectCreated>().Subscribe(
                m => mainPage.LayoutRoot.Children.Add(new TextBlock {Text = "Object created!!!!"}));

            RootVisual = mainPage;

            gameEngine.Start();
        }

        IKernel NinjectBootstrap() {
            var kernel = new StandardKernel();
            kernel.Bind<IGameEngine>().To<GameEngine>();
            kernel.Bind<IPhysicsEngine>().To<PhysicsEngine>();
            kernel.Bind<IMessageBus>().To<MessageBus>();
            return kernel;
        }

        void Application_Exit(object sender, EventArgs e) {}

        void Application_UnhandledException(object sender, ApplicationUnhandledExceptionEventArgs e) {
            // If the app is running outside of the debugger then report the exception using
            // the browser's exception mechanism. On IE this will display it a yellow alert 
            // icon in the status bar and Firefox will display a script error.
            if (!Debugger.IsAttached) {
                // NOTE: This will allow the application to continue running after an exception has been thrown
                // but not handled. 
                // For production applications this error handling should be replaced with something that will 
                // report the error to the website and stop the application.
                e.Handled = true;
                Deployment.Current.Dispatcher.BeginInvoke(delegate { ReportErrorToDOM(e); });
            }
        }

        void ReportErrorToDOM(ApplicationUnhandledExceptionEventArgs e) {
            try {
                string errorMsg = e.ExceptionObject.Message + e.ExceptionObject.StackTrace;
                errorMsg = errorMsg.Replace('"', '\'').Replace("\r\n", @"\n");

                HtmlPage.Window.Eval("throw new Error(\"Unhandled Error in Silverlight Application " + errorMsg + "\");");
            }
            catch (Exception) {}
        }
    }

    public class ViewCreator : ISubscriber<GameObjectCreated>
    {
        readonly Canvas _layoutRoot;

        public ViewCreator(Canvas layoutRoot) {
            _layoutRoot = layoutRoot;
        }

        #region ISubscriber<GameObjectCreated> Members

        public void Receive(GameObjectCreated message) {}

        #endregion
    }
}