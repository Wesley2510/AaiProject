using AntSimulator.entity;
using AntSimulator.util;

namespace AntSimulator.behaviour
{
    public abstract class SteeringBehaviour
    {
        public MovingEntity MovingEntity { get; set; }
        public Vector2D Target { get; set; }
        public abstract Vector2D Calculate();

        protected SteeringBehaviour(MovingEntity movingentity)
        {
            MovingEntity = movingentity;
        }
    }


}
