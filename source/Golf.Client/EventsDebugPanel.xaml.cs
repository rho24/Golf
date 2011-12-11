using System;
using System.Windows.Controls;
using Golf.Core;
using Golf.Core.Events;
using System.Reactive.Linq;

namespace Golf.Client
{
    /// <summary>
    /// Interaction logic for EventsDebugPanel.xaml
    /// </summary>
    public partial class EventsDebugPanel : UserControl
    {
        public EventsDebugPanel() {
            InitializeComponent();
        }

        public void Initialize(IGameEngine gameEngine) {
            gameEngine.Events.Where(e => !(e is ShouldRender)).ObserveOnDispatcher().Subscribe(e => Events.Children.Add(new TextBlock {Text = e.ToString()}));
        }
    }
}