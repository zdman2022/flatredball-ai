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
    /// Prioritized Dithering
    /// </summary>
    public class PD : IForceCalculation
    {
        private Random mRandom = new Random();
        #region IForceCalculation Members

        /// <summary>
        /// Calculates force by applying forces based on probability it needs to be applied.  Only one
        /// force will be applied each time.
        /// </summary>
        /// <param name="pAgent">Source Agent</param>
        /// <param name="pBehaviors">List of Behaviors to Apply.</param>
        /// <param name="pMaxForce">Max Force that can be applied.</param>
        /// <returns>Force to apply to source agent.</returns>
        public Vector3 Calculate(PositionedObject pAgent, List<IBehavior> pBehaviors, float pMaxForce)
        {
            Vector3 steeringForce;

            foreach (IBehavior var in pBehaviors)
            {
                if (mRandom.NextDouble() <= var.Probability)
                {
                    steeringForce = Vector3.Multiply(var.Calculate(pAgent), var.Weight / var.Probability);

                    if (steeringForce.Length() > 0)
                    {
                        if (steeringForce.Length() > pMaxForce)
                        {
                            steeringForce.Normalize();
                            Vector3.Multiply(steeringForce, pMaxForce);
                        }

                        return steeringForce;
                    }
                }
            }

            return new Vector3();
        }

        #endregion
    }
}
