using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FlatRedBall;
using Microsoft.Xna.Framework;
using FlatRedBallAI.AI.SteeringAgents.Helpers;

namespace FlatRedBallAI.AI.SteeringAgents.Behaviors
{
    class OffsetPursuitBehavior : IBehavior
    {
        public OffsetPursuitBehavior(PositionedObject pTarget)
        {
            Target = pTarget;
            MaxSpeed = 1;
            Deceleration = .3f;
            Offset = new Vector3(-1, 0, 0);
            Weight = 1;
            Probability = 1;
            Name = "OffsetPursuit";
            StopDistance = 1f;
        }

        public int MaxSpeed { get; set; }
        public PositionedObject Target { get; set; }
        public float Deceleration { get; set; }
        public Vector3 Offset { get; set; }
        public float StopDistance { get; set; } 

        #region IBehavior Members

        public float Weight{ get; set; }
        public float Probability { get; set; }
        public string Name { get; set; }

        Vector3 IBehavior.Calculate(PositionedObject pAgent)
        {
            Vector3 Direction = pAgent.Velocity;
            Direction.Normalize();

            Vector3 WorldOffsetPosition = SteeringHelper.PointToWorldSpace(Offset, SteeringHelper.ZeroAxis, Direction, Target.Position);

            Vector3 ToOffset = WorldOffsetPosition - pAgent.Position;

            float LookAheadTime = ToOffset.Length() / (float)(MaxSpeed + pAgent.Velocity.Length());

            return SteeringHelper.Arrive(pAgent, WorldOffsetPosition + Target.Velocity * LookAheadTime, Deceleration, StopDistance);
        }


        #endregion
    }
}
