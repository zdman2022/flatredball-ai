﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FlatRedBall;
using Microsoft.Xna.Framework;
using FlatRedBallAI.AI.SteeringAgents.Helpers;

namespace FlatRedBallAI.AI.SteeringAgents.Behaviors
{
    public class GoToPointBehavior : IBehavior
    {
        private Vector3 mTargetPos;

        public GoToPointBehavior(Vector3 pTargetPos)
        {
            mTargetPos = pTargetPos;
            Deceleration = .3f;
            Weight = 1;
            Probability = 1;
        }

        public float Deceleration { get; set; }

        #region IBehavior Members

        public float Weight{ get; set; }
        public float Probability { get; set; }

        Vector3 IBehavior.Calculate(PositionedObject pAgent)
        {
            return SteeringHelper.Arrive(pAgent, mTargetPos, Deceleration);
        }

        #endregion
    }
}
