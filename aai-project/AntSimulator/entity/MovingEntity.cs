using AntSimulator.behaviour;
using AntSimulator.util;
using AntSimulator.world;

namespace AntSimulator.entity
{
    public enum Deceleration
    {
        Slow, Normal, Fast
    }
    public abstract class MovingEntity : BaseGameEntity
    {
        public Vector2D Velocity { get; set; }
        public float Mass { get; set; }
        public float MaxSpeed { get; set; }
        public SteeringBehaviour Steeringbehaviour { get; set; }

        protected MovingEntity(Vector2D pos, World w) : base(pos, w)
        {
            Mass = 10;
            MaxSpeed = 5;
            Velocity = new Vector2D();
        }

        public override void Update(float timeElapsed)
        {
            if (Steeringbehaviour == null) return;
            var steering = Steeringbehaviour.Calculate();
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
