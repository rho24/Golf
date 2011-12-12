﻿using System;
using System.Reactive.Linq;
using System.Windows.Controls;
using Golf.Core.Events;

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

        public void Initialize(IObservable<IGameEvent> events) {
            events.Where(e => !(e is ShouldRender)).ObserveOnDispatcher().Subscribe(
                e => Events.Children.Add(new TextBlock {Text = e.ToString()}));
        }
    }
}