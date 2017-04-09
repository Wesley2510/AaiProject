using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using SteeringCS.entity;
using SteeringCS.util;

namespace SteeringCS.behaviour
{   class WanderBehaviour : SteeringBehaviour
    {

        Random rand = new Random();
        private double wanderAngle;
        public float maxHeadingChange = 25;
        float heading;
        //ToDo  
        public Vector2D Target { get; set; }

        float m_dWanderJitter = 20.0f;
        private double m_dWanderRadius =2.0 ;
        double m_dWanderDistance = 2.0;

        private Vector2D WanderTarget;

        private Ant ant;
        public WanderBehaviour(Ant movingEntity) : base(movingEntity)
        float m_dWanderJitter = 50.0f;
     
        public WanderBehaviour(MovingEntity movingEntity) : base(movingEntity)
        {
            ant = movingEntity;
            WanderTarget = movingEntity.Pos;
        }
       
       
    public  float Clamp(float Val, float MinVal, float MaxVal)
     {
         if (Val < MinVal) { Val = MinVal; return Val; }
         if (Val > MaxVal) { Val = MaxVal; return Val; }
         return Val;
     }

        public float NewHeadingRoute()
        {
            
            var floor = Clamp(heading - maxHeadingChange, 0, 360);
            var ceil = Clamp(heading + maxHeadingChange, 0, 360);
            heading = rand.Next((int)floor, (int)ceil);
            return heading;

          //  targetRotation = new Vector3(heading, heading, 0);
        }
        public override Vector2D Calculate()
        {
            Target = new Vector2D((NewHeadingRoute() * m_dWanderJitter), (NewHeadingRoute() * m_dWanderJitter));
            var velocity = Vector2D.Normalize(Target - MovingEntity.Pos) * MovingEntity.MaxSpeed;
            var steering = velocity - MovingEntity.Velocity;
            steering = Vector2D.Truncate(steering, MovingEntity.MaxSpeed);
            //Console.WriteLine(Target.X+ " " + Target.Y);
            //Thread.Sleep(1000);
            return steering;
        }
    }
}
