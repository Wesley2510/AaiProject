using System;
using SteeringCS.behaviour;
using SteeringCS.util;
using SteeringCS.world;

namespace SteeringCS.entity
{
    public abstract class Ant : BaseGameEntity
    {
        public int Id { get; set; }
        public Vector2D Velocity { get; set; }        
        public float Mass { get; set; }
        public float MaxSpeed { get; set; }
        public SteeringBehaviour Steeringbehaviour { get; set; }

        public Ant(Vector2D pos, World w) : base(pos, w)
        {
            Mass = 30;
            MaxSpeed = 10;
            Velocity = new Vector2D();
        }        

        public override void Update(float timeElapsed)
        {
            Vector2D steering = Steeringbehaviour.Calculate();
            steering.Truncate(MaxSpeed);
            steering /= Mass;
            Velocity = (Velocity + steering).Truncate(MaxSpeed);
            Pos += Velocity;
        }

        public override string ToString()
        {
            return $"{Velocity}";
        }
    }
}