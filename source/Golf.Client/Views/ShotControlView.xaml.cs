using System;
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
        public ShotControlView() {
            InitializeComponent();

            Observable.FromEventPattern<MouseEventHandler, MouseEventArgs>(
                h => CaptureSurface.PreviewMouseMove += h,
                h => CaptureSurface.PreviewMouseMove -= h)
                .Subscribe(e => {
                               if (e == null) return;
                               var pos = e.EventArgs.GetPosition(CentreCursor);
                               var model = (ShotControlModel) DataContext;
                               model.PowerX = pos.X;
                               model.PowerY = pos.Y;
                               model.UpdatePosition();
                           });
        }
    }
}