using Golf.Core.Maths;

namespace Golf.Core.Physics.BoundingBoxes
{
    public class RectangleBoundingBox : IBoundingBox
    {
        readonly Vector2 _topLeft;
        readonly Vector2 _bottomRight;

        public RectangleBoundingBox(Vector2 topLeft, Vector2 bottomRight) {
            _topLeft = topLeft;
            _bottomRight = bottomRight;
        }

        public bool Contains(Vector2 position) {
            return position.X >= _topLeft.X
                   && position.X < _bottomRight.X
                   && position.Y >= _topLeft.Y
                   && position.Y < _bottomRight.Y;
        }
    }
}