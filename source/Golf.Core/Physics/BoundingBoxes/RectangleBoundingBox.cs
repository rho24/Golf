using System;
using Golf.Core.Maths;

namespace Golf.Core.Physics.BoundingBoxes
{
    public class RectangleBoundingBox : IBoundingBox
    {
        public RectangleBoundingBox(Vector2 topLeft, Vector2 bottomRight) {
            TopLeft = topLeft;
            BottomRight = bottomRight;
            Size = new Vector2(bottomRight.X - topLeft.X, bottomRight.Y - topLeft.Y);
        }

        public Vector2 TopLeft { get; private set; }
        public Vector2 BottomRight { get; private set; }
        public Vector2 Size { get; private set; }

        #region IBoundingBox Members

        public bool Contains(Vector2 position) {
            return position.X >= TopLeft.X
                   && position.X < BottomRight.X
                   && position.Y >= TopLeft.Y
                   && position.Y < BottomRight.Y;
        }

        #endregion
    }
}