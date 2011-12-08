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
        public MainWindow(IGameEngine gameEngine) {
            GameEngine = gameEngine;
            InitializeComponent();
        }

        public IGameEngine GameEngine { get; private set; }

        void Start_OnClick(object sender, RoutedEventArgs e) {
            GameEngine.Start();
        }
    }
}