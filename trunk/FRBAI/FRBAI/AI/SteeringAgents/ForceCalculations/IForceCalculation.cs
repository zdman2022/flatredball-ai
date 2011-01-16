using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FlatRedBall;
using Microsoft.Xna.Framework;
using FlatRedBallAI.AI.SteeringAgents.Behaviors;

namespace FlatRedBallAI.AI.SteeringAgents.ForceCalculations
{
    public interface IForceCalculation
    {
       Vector3 Calculate(PositionedObject pAgent, List<IBehavior> pBehaviors, float pMaxForce);
    }
}
