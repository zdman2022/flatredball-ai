﻿using System;
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
            Name = "FleePoint";
        }

        public float PanicDistance { get; set; }
        public int MaxSpeed { get; set; }
        private Vector3 mTargetPos;

        #region IBehavior Members

        public float Weight{ get; set; }
        public float Probability { get; set; }
        public string Name { get; set; }
        public Vector3 TargetPosition
        {
            get { return mTargetPos; }
            set { mTargetPos = value; }
        }
   

        Vector3 IBehavior.Calculate(PositionedObject pAgent)
        {
            return SteeringHelper.Flee(pAgent, mTargetPos, PanicDistance, MaxSpeed);
        }

        #endregion
    }
}
