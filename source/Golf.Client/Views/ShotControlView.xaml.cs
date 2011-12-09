using System;
using System.Linq;
using System.Reactive.Linq;
using System.Windows.Controls;
using System.Windows.Input;
using Golf.Client.ViewModels;

namespace Golf.Client.Views
{
    /// <summary>
    /// Interaction logic for ShotControlView.xaml
    /// </summary>
    public partial class ShotControlView : UserControl
    {
        public ShotControlView(Canvas parent) {
            InitializeComponent();

            Observable.FromEventPattern<MouseEventHandler, MouseEventArgs>(h => parent.PreviewMouseMove += h,
                                                                           h => parent.PreviewMouseMove -= h)
                .ObserveOnDispatcher()
                .Subscribe(e => {
                               if (e == null) return;
                               var pos = e.EventArgs.GetPosition(LayoutRoot);
                               var model = (ShotControlModel) DataContext;
                               model.PowerX = pos.X;
                               model.PowerY = pos.Y;
                               model.UpdatePosition();
                           });
        }
    }
}