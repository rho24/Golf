using System;
using Golf.Core.GameObjects;

namespace Golf.Client.ViewModels
{
    public class ShotControlModel : ViewModelBase
    {
        public GolfBall PlayersBall { get; set; }
        public double PowerX { get; set; }
        public double PowerY { get; set; }
    }
}