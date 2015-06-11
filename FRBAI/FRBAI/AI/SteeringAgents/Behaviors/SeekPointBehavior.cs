using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FlatRedBall;
using Microsoft.Xna.Framework;
using FlatRedBallAI.AI.SteeringAgents.Helpers;

namespace FlatRedBallAI.AI.SteeringAgents.Behaviors
{
    public class SeekPointBehavior : IBehavior
    {
        public SeekPointBehavior(Vector3 pTargetPos)
        {
            TargetPosition = pTargetPos;
            MaxSpeed = 1;
            Weight = 1;
            Probability = 1;
            Name = "SeekPoint";
            StopDistance = 1f;
            TargetPosition = new Vector3();
        }

        public int MaxSpeed { get; set; }
        public float StopDistance { get; set; } 

        #region IBehavior Members

        public float Weight{ get; set; }
        public float Probability { get; set; }
        public string Name { get; set; }
        public Vector3 TargetPosition { get; set; }

        Vector3 IBehavior.Calculate(PositionedObject pAgent)
        {
            return SteeringHelper.Seek(pAgent, TargetPosition, MaxSpeed, StopDistance);
        }

        #endregion
    }
}
