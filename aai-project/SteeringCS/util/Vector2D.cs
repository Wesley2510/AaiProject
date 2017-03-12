using System;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;

namespace SteeringCS.util
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

        #region Old methods

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
            return Multiply(1.0 / value);
        }

        public Vector2D Normalize()
        {
            var length = Length();
            if (length == 0) return this;
            X /= length;
            Y /= length;
            return this;
        }

        public double Length()
        {
            return Math.Sqrt(X * X + Y * Y);
        }

        public Vector2D Truncate(double maX)
        {
            if (Length() > maX)
            {
                Normalize();
                Multiply(maX);
            }
            return this;
        }
        #endregion

        public static double Length(Vector2D vector)
        {
            return Math.Sqrt(vector.X * vector.X + vector.Y * vector.Y);
        }

        public double LengthSquared()
        {
            throw new NotImplementedException();
        }

        public static Vector2D Normalize(Vector2D vector)
        {
            var length = Length(vector);
            if (length == 0) return vector;
            vector.X /= length;
            vector.Y /= length;
            return vector;
        }

        public static Vector2D Truncate(Vector2D vector, double maX)
        {
            if (!(Length(vector) > maX)) return vector;
            vector = Normalize(vector);
            vector = vector * maX;
            return vector;
        }

        public Vector2D Clone()
        {
            return new Vector2D(X, Y);
        }

        public static Vector2D operator +(Vector2D v1, Vector2D v2)
        {
            return new Vector2D(v1.X + v2.X, v1.Y + v2.Y);
        }

        public static Vector2D operator -(Vector2D v1, Vector2D v2)
        {
            return new Vector2D(v1.X - v2.X, v1.Y - v2.Y);
        }

        public static Vector2D operator *(Vector2D v1, double value)
        {
            return new Vector2D(v1.X * value, v1.Y * value);
        }

        public static Vector2D operator /(Vector2D v1, double value)
        {
            return new Vector2D(v1.X / value, v1.Y / value);
        }

        public override string ToString()
        {
            return String.Format("({0},{1})", X, Y);
        }
    }
}