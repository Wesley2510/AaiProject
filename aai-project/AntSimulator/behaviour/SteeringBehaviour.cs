using AntSimulator.entity;
using AntSimulator.util;

namespace AntSimulator.behaviour
{
    public abstract class SteeringBehaviour
    {
        public MovingEntity Movingentity { get; set; }
        public abstract Vector2D Calculate();

        public SteeringBehaviour(MovingEntity movingentity)
        {
            Movingentity = movingentity;
        }
    }


}
