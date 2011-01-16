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
    public class AvoidObstaclesBehavior : IBehavior
    {
        public AvoidObstaclesBehavior()
        {
            AgentRadius = 1;
            MaxSpeed = 5;
            DetectionLength = 5;
            BreakWeight = .2f;
            Weight = 1;
            Probability = 1;
        }

        public List<Circle> CircleObstacles { get; set; }
        public List<AxisAlignedRectangle> RectangleObstacles { get; set; }
        public float AgentRadius { get; set; }
        public int MaxSpeed { get; set; }
        public float DetectionLength { get; set; }
        public float BreakWeight { get; set; }

        #region IBehavior Members

        public float Weight{ get; set; }
        public float Probability { get; set; }

        Vector3 IBehavior.Calculate(PositionedObject pAgent)
        {
            Vector3 returnValue = Vector3.Zero;

            if (CircleObstacles != null)
            {
                returnValue += SteeringHelper.AvoidObstacles(pAgent, AgentRadius, CircleObstacles, MaxSpeed, DetectionLength, BreakWeight);
            }

            if (RectangleObstacles != null)
            {
                returnValue += SteeringHelper.BarrierAvoidanceWithThreeFeelers(pAgent, RectangleObstacles, DetectionLength);
            }

            return returnValue;
        }

        #endregion
    }
}
