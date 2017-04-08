using System;
using SteeringCS.behaviour;
using SteeringCS.util;
using SteeringCS.world;
using SteeringCS.State;

namespace SteeringCS.entity
{
    public abstract class MovingEntity : BaseGameEntity
    {
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
        public State.State CurrentState;


        public SteeringBehaviour Steeringbehaviour { get; set; }

        public MovingEntity(Vector2D pos, World w) : base(pos, w)
        {
            Mass = 30;
            MaxSpeed = 10;
            Velocity = new Vector2D();
        }        

        public override void Update(float timeElapsed)
        {
            CurrentState?.Execute(this);

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
        public void ChangeState(State.State newState)
        {
            CurrentState?.Exit(this);
            CurrentState = newState;
            CurrentState.Enter(this);
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
         /**   if (Vector2D.DistanceSquared(Target.Pos, this.Pos) > panicDistance)
            {
                return false;
            }*/
            return true;
        }


    public override string ToString()
        {
            return String.Format("{0}", Velocity);
        }
    }
}