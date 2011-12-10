﻿using System;
using System.Windows.Input;
using Golf.Core.GameObjects;

namespace Golf.Client.ViewModels
{
    public class ShotControlModel : ViewModelBase
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

        public event EventHandler CanExecuteChanged;

        #endregion
    }
}