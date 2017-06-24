using System.Collections.Generic;
using AntSimulator.behaviour;
using AntSimulator.Goals;
using AntSimulator.util;
using AntSimulator.world;

namespace AntSimulator.entity
{
    public enum Deceleration
    {
        Slow,
        Normal,
        Fast
    }

    public abstract class MovingEntity : BaseGameEntity
    {
        public Vector2D Velocity { get; set; }
        public float Mass { get; set; }
        public float MaxSpeed { get; set; }
        public List<SteeringBehaviour> SteeringBehaviours { get; set; }

        protected MovingEntity(Vector2D pos, World w) : base(pos, w)
        {
            Mass = 30;
            Velocity = new Vector2D();
            SteeringBehaviours = new List<SteeringBehaviour>();
            ActivateSteering();
        }

        public override void Update(float timeElapsed)
        {
            var steering = SteeringBehaviour.CombineAllBehaviors(SteeringBehaviours);
            var acceleration = Vector2D.Truncate(steering, MaxSpeed) / Mass;
            Velocity += acceleration * timeElapsed;
            Velocity = Vector2D.Truncate(Velocity, MaxSpeed);
            Pos = Pos + Velocity * timeElapsed;
            
        }

        public void ActivateSteering()
        {          
            /*SteeringBehaviours.Add(new Arrival(this, MyWorld.Target.Pos, Deceleration.Fast));*/
            //SteeringBehaviours.Add(new Seek(this, MyWorld.Target.Pos));
            //SteeringBehaviours.Add(new ObstacleAvoidance(this));
            
        }
    }
}