using System;
using System.ComponentModel;
using Golf.Core.GameObjects;

namespace Golf.Client.ViewModels
{
    public class ShotControlModel : INotifyPropertyChanged
    {
        public GolfBall PlayersBall { get; set; }
        public double PowerX { get; set; }
        public double PowerY { get; set; }

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