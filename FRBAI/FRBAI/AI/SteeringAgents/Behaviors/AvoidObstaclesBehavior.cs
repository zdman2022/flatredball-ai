using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FlatRedBall;
using Microsoft.Xna.Framework;
using FlatRedBallAI.AI.SteeringAgents.Helpers;
using FlatRedBall.Math.Geometry;
using FlatRedBall.Math;

namespace FlatRedBallAI.AI.SteeringAgents.Behaviors
{
    public class AvoidObstaclesBehavior : IBehavior
    {
        public AvoidObstaclesBehavior()
        {
            CircleObstacles = new PositionedObjectList<Circle>(); //allow to call .Add   e.g, avoidO.CircleObstacles.Add(CollisionCircle);
            RectangleObstacles = new PositionedObjectList<AxisAlignedRectangle>(); //allow to call .Add   e.g, avoidO.RectangleObstacles.Add(CollisionAARect);
            PolygonObstacles = new PositionedObjectList<Polygon>(); //allow to call .Add   e.g, avoidO.PolygonObstacles.Add(CollisionPolygon);
            AgentRadius = 1;
            MaxSpeed = 5; 
            DetectionLength = 5; 
            BreakWeight = .2f;
            Weight = 1;
            Probability = 1;
            Name = "AvoidObstacles";
            TargetPosition = new Vector3();
        }

        public PositionedObjectList<Circle> CircleObstacles { get; set; }
        public PositionedObjectList<AxisAlignedRectangle> RectangleObstacles { get; set; }
        public PositionedObjectList<Polygon> PolygonObstacles { get; set; }
        public float AgentRadius { get; set; }
        public int MaxSpeed { get; set; }
        public float DetectionLength { get; set; }
        public float BreakWeight { get; set; }

        #region IBehavior Members

        public float Weight{ get; set; }
        public float Probability { get; set; }
        public string Name { get; set; }
        public Vector3 TargetPosition { get; set; }

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

            if (PolygonObstacles != null)
            {
                returnValue += SteeringHelper.BarrierAvoidanceWithThreeFeelersPolygon(pAgent, PolygonObstacles, DetectionLength);
            }

            return returnValue;
        }

        #endregion
    }
}
