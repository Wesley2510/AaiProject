using SteeringCS.entity;
using SteeringCS.util;

namespace SteeringCS.behaviour
{
    public abstract class SteeringBehaviour
    {
        public Ant Ant { get; set; }
        public abstract Vector2D Calculate();

        public SteeringBehaviour(Ant ant)
        {
            Ant = ant;
        }        
    }    
}
