using System;
using System.ComponentModel;

namespace Golf.Client.GameObjects
{
    /// <summary>
    /// Interaction logic for GolfBall.xaml
    /// </summary>
    public partial class GolfBall : INotifyPropertyChanged
    {
        double _x;
        double _y;

        public GolfBall() {
            InitializeComponent();
            DataContext = this;
        }

        public double X {
            get { return _x; }
            set {
                _x = value;
                NotifyPropertyChanged("X");
            }
        }

        public double Y {
            get { return _y; }
            set {
                _y = value;
                NotifyPropertyChanged("Y");
            }
        }

        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        void NotifyPropertyChanged(string name) {
            var propertyChanged = PropertyChanged;

            if (propertyChanged != null) {
                propertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }
    }
}