using System;
using System.Windows;
using Golf.Core;

namespace Golf.Client
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        readonly IViewController _viewController;

        public MainWindow(IGameEngine gameEngine, IViewController viewController) {
            GameEngine = gameEngine;
            _viewController = viewController;
            InitializeComponent();

            viewController.Initialize(Canvas);
        }

        public IGameEngine GameEngine { get; private set; }

        void Start_OnClick(object sender, RoutedEventArgs e) {
            GameEngine.Start();
        }

        void Tick_OnClick(object sender, RoutedEventArgs e) {
            var rand = new Random();
            foreach (var view in _viewController.Views) {
                view.Model.X = rand.NextDouble()*600;
                view.Model.Y = rand.NextDouble()*400;

                view.UpdatePosition();
            }
        }
    }
}