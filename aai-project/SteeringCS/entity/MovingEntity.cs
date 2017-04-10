using SteeringCS.behaviour;
using SteeringCS.util;
using SteeringCS.world;

namespace SteeringCS.entity
{
    public abstract class MovingEntity : BaseGameEntity
    {
        public int Id { get; set; }
        public enum EntityStatus
        {
            Wandering,
            Searching,
            Fleeing
        }

        public EntityStatus Status { get; set; }
        public Vector2D Velocity { get; set; }
        public float Mass { get; set; }
        public float MaxSpeed { get; set; }
        public float Hunger { get; set; }
        public float Fatigue { get; set; }


        public SteeringBehaviour Steeringbehaviour { get; set; }

        public MovingEntity(Vector2D pos, World w) : base(pos, w)
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
        }

        public void ChangeStatus(EntityStatus entityStatus)
        {
            Status = entityStatus;
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
            /**if (Vector2D.DistanceSquared(Target.Pos, this.Pos) > panicDistance)
               {
                   return false;
               }*/
            return true;
        }


        public override string ToString()
        {
            return $"{Velocity}";
        }
    }
}