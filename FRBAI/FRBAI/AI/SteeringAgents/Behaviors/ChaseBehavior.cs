using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FlatRedBall;
using Microsoft.Xna.Framework;
using FlatRedBallAI.AI.SteeringAgents.Helpers;

namespace FlatRedBallAI.AI.SteeringAgents.Behaviors
{
    public class ChaseBehavior : IBehavior
    {
        public ChaseBehavior()
        {
            Targets = new List<PositionedObject>();
            MaxSpeed = 1;
            Weight = 1;
            Probability = 1;
            Name = "Chase";
            StopDistance = 1f;
            TargetPosition = new Vector3();
        }

        public int MaxSpeed { get; set; }
        public List<PositionedObject> Targets { get; set; }

        #region IBehavior Members

        public float Weight{ get; set; }
        public float Probability { get; set; }
        public string Name { get; set; }
        public float StopDistance { get; set; } //distance to stop at the seek destination
        public Vector3 TargetPosition { get; set; }

        Vector3 IBehavior.Calculate(PositionedObject pAgent)
        {
            if (Targets != null)
            {
                PositionedObject target = SteeringHelper.GetClosestTarget(pAgent.Position, Targets);

                if(target != null)
                    return SteeringHelper.Pursuit(pAgent, target, MaxSpeed, StopDistance);
            }

            return Vector3.Zero;
        }

        #endregion
    }
}
