using SteeringCS.entity;
using SteeringCS.util;

namespace SteeringCS.behaviour
{
    public class SeekBehaviour : SteeringBehaviour
    {
        public Vector2D Target { get; set; }
        public SeekBehaviour(Ant ant, Vector2D target) : base(ant)
        {
            Target = target;
        }

        public override Vector2D Calculate()
        {
            var velocity = Vector2D.Normalize(Target - Ant.Pos) * Ant.MaxSpeed;
            var steering = velocity - Ant.Velocity;
            steering = Vector2D.Truncate(steering, Ant.MaxSpeed);

            return steering;
        }       
    }
}
