using System;
using System.ComponentModel;

namespace Golf.Client.ViewModels
{
    public abstract class ViewModelBase : INotifyPropertyChanged
    {
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