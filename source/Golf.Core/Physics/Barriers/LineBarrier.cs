using System;
using Golf.Core.GameObjects;
using Golf.Core.Maths;
using Golf.Core.Physics.Collisions;

namespace Golf.Core.Physics.Barriers
{
    public class LineBarrier : IBarrier
    {
        readonly Vector2 _leftPoint;
        readonly Line _line;
        readonly Vector2 _rightPoint;

        public LineBarrier(Vector2 leftPoint, Vector2 rightPoint) {
            _leftPoint = leftPoint;
            _rightPoint = rightPoint;
            _line = new Line(_leftPoint, _rightPoint - _leftPoint);
        }

        #region IBarrier Members

        public ICollision CalculateCollision(GameObjectBase gameObject, TimeSpan tickPeriod) {
            if (gameObject.Body.Velocity == Vector2.Zero)
                return null;

            if (gameObject is GolfBall)
                return CalculateCollision((GolfBall) gameObject, tickPeriod);

            return null;
        }

        #endregion

        public ICollision CalculateCollision(GolfBall ball, TimeSpan tickPeriod) {
            var ballPath = new Line(ball.Body.Position, ball.Body.Velocity);
            var intersection = _line.Intersect(ballPath);

            // no collision if parrallel
            if (intersection == null)
                return null;

            var collisionTime = (intersection - ball.Body.Position).Length/ball.Body.Velocity.Length;
            
            //var collisionTime = (intersection - ball.Body.Position).Dot(ball.Body.Velocity);
            if (collisionTime < 0.0 || collisionTime >= tickPeriod.TotalSeconds)
                return null;

            var leftDistanceOnLine = _line.Normal.Dot(_leftPoint);
            var rightDistanceOnLine = _line.Normal.Dot(_rightPoint);
            var collisionDistanceOnLine = _line.Normal.Dot(intersection);

            if (leftDistanceOnLine > collisionDistanceOnLine || collisionDistanceOnLine > rightDistanceOnLine)
                return null;

            var perp = _line.Normal.Perpendicular();
            var impulse = perp*-2*perp.Dot(ball.Body.Velocity); //TODO: requires mass;

            return new SingleBallCollision(TimeSpan.FromSeconds(collisionTime), ball, impulse);
        }
    }

    public class Line
    {
        readonly Vector2 _perpendicular;

        public Line(Vector2 start, Vector2 direction) {
            Start = start;
            Normal = direction.Normal;
            _perpendicular = direction.Perpendicular();
        }

        public Vector2 Start { get; private set; }
        public Vector2 Normal { get; private set; }

        public Vector2 Intersect(Line line) {
            var velocityRelativeToL1 = Normal.Cross(line.Normal);

            if (velocityRelativeToL1 == 0.0)
                return null;

            var l1DistanceFromPerp = _perpendicular.Dot(Start);
            var l2DistanceFromPerp = _perpendicular.Dot(line.Start);

            var lambda = (l1DistanceFromPerp - l2DistanceFromPerp)/velocityRelativeToL1;

            return line.Start + (lambda*line.Normal);
        }
    }
}