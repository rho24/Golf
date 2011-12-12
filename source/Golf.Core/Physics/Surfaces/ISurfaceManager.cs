using System;
using System.Collections.Generic;

namespace Golf.Core.Physics.Surfaces
{
    public interface ISurfaceManager
    {
        IEnumerable<Surface> Surfaces { get; set; }
    }
}