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

        float m_dWanderJitter = 50.0f;
     
        public WanderBehaviour(Ant movingEntity) : base(movingEntity)
        {
           // Target = new Vector2D((NewHeadingRoute() * m_dWanderJitter), (NewHeadingRoute() * m_dWanderJitter));
     //       Thread.Sleep(250);
         
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
            return rand.Next((int)floor, (int)ceil);

          //  targetRotation = new Vector3(heading, heading, 0);
        }
        public override Vector2D Calculate()
        {
            Target = new Vector2D((NewHeadingRoute() * m_dWanderJitter), (NewHeadingRoute() * m_dWanderJitter));
            var velocity = Vector2D.Normalize(Target - Ant.Pos) * Ant.MaxSpeed;
            var steering = velocity - Ant.Velocity;
            steering = Vector2D.Truncate(steering, Ant.MaxSpeed);
            Console.WriteLine(Target.X+ " " + Target.Y);
            Thread.Sleep(1000);
            return steering;
        }
    }
}
