using System;
using System.Collections.Generic;
using System.Windows.Controls;
using Golf.Client.Views;

namespace Golf.Client
{
    public interface IViewController
    {
        void Initialize(Canvas canvas);
        IEnumerable<GolfBallView> Views { get; }
    }
}