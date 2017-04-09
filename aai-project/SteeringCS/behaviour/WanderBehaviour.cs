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
            
            //WanderTarget += new Vector2D((rand.Next(-1,1) * m_dWanderJitter), (rand.Next(-1, 1) * m_dWanderJitter));
            //WanderTarget.Normalize();
            //WanderTarget *= m_dWanderRadius;
            //Vector2D target = WanderTarget + new Vector2D(m_dWanderDistance, 0);

           

            Target = new Vector2D((NewHeadingRoute() * m_dWanderJitter), (NewHeadingRoute() * m_dWanderJitter));
            var velocity = Vector2D.Normalize(Target - ant.Pos) * ant.MaxSpeed;
            var steering = velocity - ant.Velocity;
            steering = Vector2D.Truncate(steering, ant.MaxSpeed);
            Console.WriteLine(Target.X+ " " + Target.Y);
          //  Thread.Sleep(250);
            return steering;
        }
    }
}
