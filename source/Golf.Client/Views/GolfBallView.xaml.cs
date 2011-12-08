using System;
using System.ComponentModel;
using Golf.Core.GameObjects;

namespace Golf.Client.Views
{
    /// <summary>
    /// Interaction logic for GolfBall.xaml
    /// </summary>
    public partial class GolfBallView : INotifyPropertyChanged
    {
        public GolfBall Model { get; private set; }

        public GolfBallView(GolfBall model) {
            Model = model;
            InitializeComponent();
            DataContext = this;
        }

        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion
        
        public void UpdatePosition() {
            var propertyChanged = PropertyChanged;

            if (propertyChanged != null) {
                propertyChanged(this, new PropertyChangedEventArgs(null));
            }
        }
    }
}