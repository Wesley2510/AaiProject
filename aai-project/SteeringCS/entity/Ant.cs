    using System;
using SteeringCS.behaviour;
    using SteeringCS.graphs;
    using SteeringCS.util;
using SteeringCS.world;
using SteeringCS.States;

namespace SteeringCS.entity
{
    public abstract class Ant : BaseGameEntity
    {
        public int Id { get; set; } 
        public enum Status
        {
            Wandering,
            Searching,
            Fleeing
        }

        public Status status { get; set; }
        public Vector2D Velocity { get; set; }
        public float Mass { get; set; }
        public float MaxSpeed { get; set; }
        public float Hunger { get; set; }
        public float Fatigue { get; set; }
        public States.State CurrentState;


        public SteeringBehaviour Steeringbehaviour { get; set; }

        public Ant(Vector2D pos, World w) : base(pos, w)
        {
            Mass = 10;
            MaxSpeed = 5;
            Velocity = new Vector2D();
        }        

        public override void Update(float timeElapsed)
        {
            if (Steeringbehaviour == null) return;

            Vector2D steering = Steeringbehaviour.Calculate();
            steering.Truncate(MaxSpeed);
            steering /= Mass;
            Velocity = (Velocity + steering).Truncate(MaxSpeed);
            Pos += Velocity;


            //Vector2D steering = Steeringbehaviour.Calculate();
            //steering.Truncate(MaxSpeed);
            //steering /= Mass;

            //Velocity = (Velocity + steering).Truncate(MaxSpeed);
            //Pos += Velocity;
        }
        public void ChangeState(State newState)
        {
            CurrentState?.Exit(this);
            CurrentState = newState;
            CurrentState.Enter(this, World.food);
        }

        public void ChangeStatus(Status status)
        {
            this.status = status;
        }
        public bool IsHungry()
        {
            return Hunger > 25f;
        }

        public void IncreaseHunger()
        {
            Hunger += 0.1f;
        }
        public bool IsSafe()
        {
            const double panicDistance = 50.0 * 50.0;
            if (Vector2D.DistanceSquared(this.Pos, World.Target.Pos) > panicDistance)
            {
                return false;
            }
            return true;
        }


    public override string ToString()
        {
            return $"{Velocity}";
        }
    }
}