using System;
using System.Reactive.Linq;
using System.Windows;
using Golf.Client.GameObjects;
using Golf.Core;
using Golf.Core.Events;

namespace Golf.Client
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow(IGameEngine gameEngine) {
            GameEngine = gameEngine;

            var rand = new Random();
            GameEngine.Events.OfType<GameObjectCreated>().Subscribe(
                e => Canvas.Children.Add(new GolfBall {X = rand.NextDouble()*600, Y = rand.NextDouble()*400}));

            InitializeComponent();
        }

        public IGameEngine GameEngine { get; private set; }

        void Start_OnClick(object sender, RoutedEventArgs e) {
            GameEngine.Start();
        }
    }
}