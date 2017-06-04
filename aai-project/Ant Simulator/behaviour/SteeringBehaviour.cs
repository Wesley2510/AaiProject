using SteeringCS.entity;

namespace SteeringCS
{
    abstract class SteeringBehaviour
    {
        public MovingEntity MovingEntity { get; set; }
        public abstract Vector2D Calculate();

        public SteeringBehaviour(MovingEntity me)
        {
            MovingEntity = me;
        }
    }

    
}
