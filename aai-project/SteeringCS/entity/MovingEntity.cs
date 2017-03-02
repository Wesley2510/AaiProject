using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SteeringCS.behaviour;
using SteeringCS.util;
using SteeringCS.world;

namespace SteeringCS.entity
{
    public abstract class MovingEntity : BaseGameEntity
    {
        public Vector2D Velocity { get; set; }
        public float Mass { get; set; }
        public float MaxSpeed { get; set; }

        public SteeringBehaviour SteeringBehaviour { get; set; }

        public MovingEntity(Vector2D pos, World w) : base(pos, w)
        {
            Mass = 30;
            MaxSpeed = 150;
            Velocity = new Vector2D();
        }

        public override void Update(float timeElapsed)
        {            
            Vector2D steeringForce = SteeringBehaviour.Calculate();
            Vector2D acceleration = steeringForce.Divide(Mass);
            Velocity.Add(acceleration.Multiply(timeElapsed));
            Velocity.Truncate(MaxSpeed);
            Pos.Add(Velocity.Multiply(timeElapsed));
            if (Velocity.LengthSquared() > 0.00000001)
            {
                Vector2D heading = Velocity.Normalize();
            }
            Console.WriteLine(ToString());
        }

        public override string ToString()
        {
            return String.Format("{0}", Velocity);
        }
    }
}