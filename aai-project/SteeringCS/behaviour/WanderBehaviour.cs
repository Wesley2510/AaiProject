using SteeringCS.entity;
using SteeringCS.util;
using System;

namespace SteeringCS.behaviour
{
    class WanderBehaviour : SteeringBehaviour
    {
        Random rand = new Random();
        private double _wanderAngle;
        public float MaxHeadingChange = 25;
        private float _heading;
        private const float MdWanderJitter = 20.0f;
        private double _mDWanderRadius = 2.0;
        double _mDWanderDistance = 2.0;
        private Vector2D _wanderTarget;
        private MovingEntity _ant;


        public WanderBehaviour(MovingEntity movingEntity) : base(movingEntity)
        {
            _ant = movingEntity;
            _wanderTarget = movingEntity.Pos;
        }


        public float Clamp(float val, float minVal, float maxVal)
        {
            if (val < minVal) { val = minVal; return val; }
            if (val > maxVal) { val = maxVal; return val; }
            return val;
        }

        public float NewHeadingRoute()
        {

            var floor = Clamp(_heading - MaxHeadingChange, 0, 360);
            var ceiling = Clamp(_heading + MaxHeadingChange, 0, 360);
            _heading = rand.Next((int)floor, (int)ceiling);
            return _heading;
        }
        public override Vector2D Calculate()
        {
            Target = new Vector2D((NewHeadingRoute() * MdWanderJitter), (NewHeadingRoute() * MdWanderJitter));
            var velocity = Vector2D.Normalize(Target - MovingEntity.Pos) * MovingEntity.MaxSpeed;
            var steering = velocity - MovingEntity.Velocity;
            steering = Vector2D.Truncate(steering, MovingEntity.MaxSpeed);
            return steering;
        }
    }
}
