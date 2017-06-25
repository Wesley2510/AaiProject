using AntSimulator.behaviour;
using AntSimulator.goal;
using AntSimulator.util;
using AntSimulator.world;
using System.Collections.Generic;

namespace AntSimulator.entity
{
    public abstract class MovingEntity : BaseGameEntity
    {
        public Vector2D Velocity { get; set; }
        public Vector2D Heading { get; set; }
        public Vector2D Side { get; set; }
        public float Mass { get; set; }
        public float MaxSpeed { get; set; }
        public Goal Brain { get; set; }
        public bool IsThinking { get; set; }
        public List<SteeringBehaviour> SteeringBehaviours { get; set; }

        protected MovingEntity(Vector2D pos, World w) : base(pos, w)
        {
            Mass = 30;
            Velocity = new Vector2D();
            SteeringBehaviours = new List<SteeringBehaviour>();
        }

        public override void Update(float timeElapsed)
        {
            if (!IsThinking && Brain != null)
            {
                IsThinking = true;
                Brain.Process();
                IsThinking = false;
            }
            var steering = SteeringBehaviour.CombineAllBehaviors(SteeringBehaviours);
            var acceleration = Vector2D.Truncate(steering, MaxSpeed) / Mass;
            Velocity += acceleration * timeElapsed;
            Velocity = Vector2D.Truncate(Velocity, MaxSpeed);
            Pos = Pos + Velocity * timeElapsed;
            if (Velocity.LengthSquared() > 0.0000001)
            {
                Heading = Vector2D.Normalize(Velocity);
                Side = Heading.Perpendicular();
            }
        }
    }
}