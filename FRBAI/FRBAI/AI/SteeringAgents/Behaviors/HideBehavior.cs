using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FlatRedBall;
using Microsoft.Xna.Framework;
using FlatRedBallAI.AI.SteeringAgents.Helpers;
using FlatRedBall.Math.Geometry;

namespace FlatRedBallAI.AI.SteeringAgents.Behaviors
{
    public class HideBehavior : IBehavior
    {
        public HideBehavior(PositionedObject pTarget)
        {
            HideFromTarget = pTarget;
            BufferSpace = 10;
            Deceleration = .9f;
            PanicDistance = 10;
            Weight = 1;
            Probability = 1;
        }

        public PositionedObject HideFromTarget { get; set; }
        public List<Circle> CircleObstacles { get; set; }
        public float BufferSpace { get; set; }
        public float Deceleration { get; set; }
        public float PanicDistance { get; set; }

        #region IBehavior Members

        public float Weight{ get; set; }
        public float Probability { get; set; }

        Vector3 IBehavior.Calculate(PositionedObject pAgent)
        {
            Vector3 returnValue = Vector3.Zero;

            if (CircleObstacles != null && HideFromTarget != null)
            {
                if(Vector3.Distance(pAgent.Position, HideFromTarget.Position) < PanicDistance)
                    returnValue += SteeringHelper.Hide(pAgent, HideFromTarget, CircleObstacles, BufferSpace, Deceleration);
            }

            return returnValue;
        }

        #endregion
    }
}
