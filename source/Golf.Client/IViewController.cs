using System;
using System.Collections.ObjectModel;
using System.Windows.Controls;

namespace Golf.Client
{
    public interface IViewController
    {
        ObservableCollection<UserControl> Views { get; }
    }
}