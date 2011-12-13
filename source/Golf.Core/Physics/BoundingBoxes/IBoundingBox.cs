using System;
using Golf.Core.Maths;

namespace Golf.Core.Physics.BoundingBoxes
{
    public interface IBoundingBox
    {
        bool Contains(Vector2 position);
    }
}