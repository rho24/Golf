using System;

namespace Golf.Core.Maths
{
    public class Vector2
    {
        public Vector2(double x, double y) {
            X = x;
            Y = y;
        }

        public double X { get; private set; }
        public double Y { get; private set; }

        public double Length {
            get { return Math.Sqrt(LengthSquared); }
        }

        public double LengthSquared {
            get { return X*X + Y*Y; }
        }

        public Vector2 Normal {
            get { return this/Length; }
        }

        public static Vector2 Zero {
            get { return new Vector2(0.0, 0.0); }
        }

        public static Vector2 UnitX {
            get { return new Vector2(1.0, 0.0); }
        }

        public static Vector2 UnitY {
            get { return new Vector2(0.0, 1.0); }
        }

        public bool Equals(Vector2 other) {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return other.X.Equals(X) && other.Y.Equals(Y);
        }

        public override bool Equals(object obj) {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != typeof (Vector2)) return false;
            return Equals((Vector2) obj);
        }

        public override int GetHashCode() {
            unchecked {
                return (X.GetHashCode()*397) ^ Y.GetHashCode();
            }
        }

        public static bool operator ==(Vector2 v1, Vector2 v2) {
            return v1.Equals(v2);
        }

        
        public static bool operator !=(Vector2 v1, Vector2 v2) {
            return !(v1 == v2);
        }

        public static Vector2 operator +(Vector2 v1, Vector2 v2) {
            return new Vector2(v1.X + v2.X, v1.Y + v2.Y);
        }

        public static Vector2 operator -(Vector2 v1, Vector2 v2) {
            return new Vector2(v1.X - v2.X, v1.Y - v2.Y);
        }

        public static Vector2 operator -(Vector2 v) {
            return new Vector2(-v.X, -v.Y);
        }

        public static Vector2 operator *(Vector2 v, double d) {
            return new Vector2(v.X*d, v.Y*d);
        }

        public static Vector2 operator *(double d, Vector2 v) {
            return v*d;
        }

        public static Vector2 operator /(Vector2 v, double d) {
            return new Vector2(v.X/d, v.Y/d);
        }

        public static Vector2 operator /(double d, Vector2 v) {
            return v/d;
        }

        public Vector2 Perpendicular() {
            return new Vector2(-Y, X).Normal;
        }

        public double Dot(Vector2 v) {
            return X*v.X + Y*v.Y;
        }

        public double Cross(Vector2 v) {
            return X*v.Y - Y*v.X;
        }
    }
}