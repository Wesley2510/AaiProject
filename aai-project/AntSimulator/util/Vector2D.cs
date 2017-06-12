using System;

namespace AntSimulator.util
{
    public class Vector2D
    {
        public double X { get; set; }
        public double Y { get; set; }

        public Vector2D() : this(0, 0)
        {
        }

        public Vector2D(double x, double y)
        {
            X = x;
            Y = y;
        }

        public double Length()
        {
            return Math.Sqrt(X * X + Y * Y);
        }

        public double LengthSquared()
        {
            return X * X + Y * Y;
        }

        public static Vector2D operator +(Vector2D v1, Vector2D v2)
        {
            return new Vector2D(v1.X + v2.X, v1.Y + v2.Y);
        }

        public static Vector2D operator -(Vector2D v1, Vector2D v2)
        {
            return new Vector2D(v1.X - v2.X, v1.Y - v2.Y);
        }

        public static Vector2D operator *(Vector2D v1, Vector2D v2)
        {
            return new Vector2D(v1.X * v2.X, v1.Y * v2.Y);
        }

        public static Vector2D operator /(Vector2D v1, Vector2D v2)
        {
            return new Vector2D(v1.X / v2.X, v1.Y / v2.Y);
        }

        public Vector2D Add(Vector2D v)
        {
            X += v.X;
            Y += v.Y;
            return this;
        }

        public Vector2D Sub(Vector2D v)
        {
            X -= v.X;
            Y -= v.Y;
            return this;
        }

        public Vector2D Multiply(double value)
        {
            X *= value;
            Y *= value;
            return this;
        }

        public Vector2D Divide(double value)
        {
            X /= value;
            Y /= value;
            return this;
        }

        public Vector2D Normalize()
        {
            return new Vector2D(X /= Length(), Y /= Length());
        }

        public Vector2D Truncate(double maX)
        {
            if (!(Length() > maX)) return this;
            Normalize();
            Multiply(maX);
            return this;
        }

        public double Distance(Vector2D v1)
        {
            var xSeparation = v1.X - X;
            var ySeparation = v1.Y - Y;
            return Math.Sqrt(ySeparation * ySeparation + xSeparation * xSeparation);

        }

        public double DistanceSquared(Vector2D v1)
        {
            var xSeparation = v1.X - X;
            var ySeparation = v1.Y - Y;
            return ySeparation * ySeparation + xSeparation * xSeparation;

        }

        public Vector2D Clone()
        {
            return new Vector2D(X, Y);
        }

        public override string ToString()
        {
            return $"({X},{Y})";
        }
    }


}
