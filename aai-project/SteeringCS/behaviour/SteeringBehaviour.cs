using SteeringCS.entity;
using SteeringCS.util;

namespace SteeringCS.behaviour
{
    public abstract class SteeringBehaviour
    {
        public MovingEntity MovingEntity { get; set; }
        public abstract Vector2D Calculate();

        public SteeringBehaviour(MovingEntity movingEntity)
        {
            MovingEntity = movingEntity;
        }
    }

    
}
