using System;
using System.Windows.Input;
using Golf.Core.GameObjects;

namespace Golf.Client.ViewModels
{
    public class ShotControlViewModel : ViewModelBase
    {
        public GolfBall PlayersBall { get; set; }
        public double PowerX { get; set; }
        public double PowerY { get; set; }
        public ICommand Hit { get; set; }
    }

    public class ActionCommand : ICommand
    {
        readonly Action _action;

        public ActionCommand(Action action) {
            _action = action;
        }

        #region ICommand Members

        public void Execute(object parameter) {
            _action();
        }

        public bool CanExecute(object parameter) {
            return true;
        }

#pragma warning disable 67
        public event EventHandler CanExecuteChanged;
#pragma warning restore 67

        #endregion
    }
}