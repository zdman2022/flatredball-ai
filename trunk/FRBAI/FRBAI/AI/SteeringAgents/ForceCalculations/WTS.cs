using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using FlatRedBall;
using FlatRedBallAI.AI.SteeringAgents.Behaviors;

namespace FlatRedBallAI.AI.SteeringAgents.ForceCalculations
{
    /// <summary>
    /// Weighted Truncated Sum
    /// </summary>
    public class WTS : IForceCalculation
    {
        #region IForceCalculation Members

        /// <summary>
        /// Calculates force by applying all behaviors and truncating if greater than max force.
        /// </summary>
        /// <param name="pAgent">Source Agent</param>
        /// <param name="pBehaviors">List of Behaviors to Apply.</param>
        /// <param name="pMaxForce">Max Force that can be applied.</param>
        /// <returns>Force to apply to source agent.</returns>
        public Vector3 Calculate(PositionedObject pAgent, List<IBehavior> pBehaviors, float pMaxForce)
        {
            Vector3 steeringForce = new Vector3();

            foreach (IBehavior var in pBehaviors)
            {
                steeringForce = steeringForce + var.Calculate(pAgent);
            }

            if(steeringForce.Length() > pMaxForce)
            {
                steeringForce.Normalize();
                return Vector3.Multiply(steeringForce, pMaxForce);
            }else{
                return steeringForce;
            }
        }

        #endregion
    }
}
