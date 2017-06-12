using AntSimulator.behaviour;
using AntSimulator.util;
using AntSimulator.world;
using System;

namespace AntSimulator.entity
{
    public abstract class MovingEntity : BaseGameEntity
    {
        public Vector2D Velocity { get; set; }
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
            // to do
            Console.WriteLine(ToString());
        }

        public override string ToString()
        {
            return $"{Velocity}";
        }
    }
}
