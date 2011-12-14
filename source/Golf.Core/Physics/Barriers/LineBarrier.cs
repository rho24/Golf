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

        //public ICollision CalculateCollision(GolfBall ball, TickTime tickTime) {
        //    var ballPath = new Line(ball.Body.Position, ball.Body.Velocity);
        //    var intersection = _line.Intersect(ballPath);

        //    // no collision if parrallel
        //    if (intersection == null)
        //        return null;

        //    var collisionTime = ball.Body.Velocity.Normal.Dot(intersection - ball.Body.Position)/
        //                        ball.Body.Velocity.Length;

        //    //var collisionTime = (intersection - ball.Body.Position).Normal.Dot(ball.Body.Velocity);

        //    if (collisionTime < 0.0 || collisionTime >= tickTime.TickElapsed.TotalSeconds)
        //        return null;

        //    var leftDistanceOnLine = _line.Normal.Dot(_leftPoint);
        //    var rightDistanceOnLine = _line.Normal.Dot(_rightPoint);
        //    var collisionDistanceOnLine = _line.Normal.Dot(intersection);

        //    if (leftDistanceOnLine > collisionDistanceOnLine || collisionDistanceOnLine > rightDistanceOnLine)
        //        return null;

        //    var perp = _line.Normal.Perpendicular();
        //    var impulse = perp*-2*perp.Dot(ball.Body.Velocity); //TODO: requires mass;

        //    return new SingleBallCollision(TimeSpan.FromSeconds(collisionTime), ball, impulse);
        //}

        #region IBarrier Members

        public bool IsCollision(DynamicBody body, BodyState startState, BodyState endState) {
            var perp = _line.Normal.Perpendicular();

            var barrierDistance = perp.Dot(_leftPoint);

            var startDistance = perp.Dot(startState.Position) - barrierDistance;
            var endDistance = perp.Dot(endState.Position) - barrierDistance;

            if (Math.Sign(startDistance) != Math.Sign(endDistance)) {
                var path = new Line(startState.Position, endState.Position);

                var pathPerp = path.Normal.Perpendicular();

                var ballDistance = pathPerp.Dot(startState.Position);
                var leftDistance = pathPerp.Dot(_leftPoint);
                var rightDistance = pathPerp.Dot(_rightPoint);

                if (leftDistance <= ballDistance && ballDistance <= rightDistance)
                    return true;
            }

            return false;
        }

        public void ApplyCollision(IEventTriggerer eventTriggerer, DynamicBody body) {
            var perp = _line.Normal.Perpendicular();
            var impulse = perp*-1.8*perp.Dot(body.Velocity); //TODO: requires mass;

            body.State = new BodyState(body.Position, body.Velocity + impulse, false);
        }

        #endregion

        public ICollision CalculateCollision(GameObjectBase gameObject, TickTime tickTime) {
            if (gameObject.Body.Velocity == Vector2.Zero)
                return null;

            if (gameObject is GolfBall)
                return CalculateCollision((GolfBall) gameObject, tickTime);

            return null;
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