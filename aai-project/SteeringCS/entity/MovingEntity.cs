using System;
using SteeringCS.behaviour;
using SteeringCS.util;
using SteeringCS.world;

namespace SteeringCS.entity
{
    public abstract class MovingEntity : BaseGameEntity
    {
        public Vector2D Velocity { get; set; }
        public Vector2D Steering { get; set; }
        public float Mass { get; set; }
        public float MaxSpeed { get; set; }

        public SteeringBehaviour Steeringbehaviour { get; set; }

        public MovingEntity(Vector2D pos, World w) : base(pos, w)
        {
            Mass = 30;
            MaxSpeed = 150;
            Velocity = new Vector2D();
        }        

        public override void Update(float timeElapsed)
        {            
                        
        }

        public override string ToString()
        {
            return String.Format("{0}", Velocity);
        }
    }
}