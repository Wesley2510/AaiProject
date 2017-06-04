using System;

namespace SteeringCS
{

    public class Vector2D
    {
        public double X { get; set; }
        public double Y { get; set; }

        public Vector2D() : this(0,0)
        {
        }

        public Vector2D(double x, double y)
        {
            X = x;
            Y = y;
        }

        public double Length()
        {
            throw new NotImplementedException();
        }

        public double LengthSquared()
        {
            throw new NotImplementedException();
        }

        public Vector2D Add(Vector2D v)
        {
            throw new NotImplementedException();
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

        public Vector2D divide(double value)
        {
            throw new NotImplementedException();
        }

        public Vector2D Normalize()
        {
            throw new NotImplementedException();
        }

        public Vector2D truncate(double maX)
        {
            if (Length() > maX)
            {
                Normalize();
                Multiply(maX);
            }
            return this;
        }
        
        public Vector2D Clone()
        {
            return new Vector2D(X, Y);
        }
        
        public override string ToString()
        {
            return String.Format("({0},{1})", X, Y);
        }
    }


}
