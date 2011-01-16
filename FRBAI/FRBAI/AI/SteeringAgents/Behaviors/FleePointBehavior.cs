using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FlatRedBall;
using Microsoft.Xna.Framework;
using FlatRedBallAI.AI.SteeringAgents.Helpers;

namespace FlatRedBallAI.AI.SteeringAgents.Behaviors
{
    public class FleePointBehavior : IBehavior
    {
        public FleePointBehavior(Vector3 pTargetPos)
        {
            mTargetPos = pTargetPos;
            MaxSpeed = 1;
            PanicDistance = 10;
            Weight = 1;
            Probability = 1;
        }

        public float PanicDistance { get; set; }
        public int MaxSpeed { get; set; }

        private Vector3 mTargetPos;
        public Vector3 TargetPosition
        {
            get { return mTargetPos; }
            set { mTargetPos = value; }
        }

        #region IBehavior Members

        public float Weight{ get; set; }
        public float Probability { get; set; }

        Vector3 IBehavior.Calculate(PositionedObject pAgent)
        {
            return SteeringHelper.Flee(pAgent, mTargetPos, PanicDistance, MaxSpeed);
        }

        #endregion
    }
}
