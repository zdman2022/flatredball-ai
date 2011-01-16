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
    /// Weighted Truncated Running Sum with Prioritization
    /// </summary>
    public class WTRSP : IForceCalculation
    {
        #region IForceCalculation Members

        /// <summary>
        /// Calculates force by applying forces until Max Force has been reached.
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
                if (!AccumulateForce(ref steeringForce, Vector3.Multiply(var.Calculate(pAgent), var.Weight), pMaxForce))
                {
                    return steeringForce;
                }
            }

            return steeringForce;
        }

        #endregion

        /// <summary>
        /// Adds the force to the total.
        /// </summary>
        /// <param name="pSteeringForce">Current Total Force</param>
        /// <param name="pAddForce">Force to Add</param>
        /// <param name="pMaxForce">Maximum Force that can be applied.</param>
        /// <returns>True if force was applied.</returns>
        private bool AccumulateForce(ref Vector3 pSteeringForce, Vector3 pAddForce, float pMaxForce)
        {
            float curMagnitude = pSteeringForce.Length();

            float magLeft = pMaxForce - curMagnitude;

            //No more force can be added.
            if (magLeft <= 0f)
                return false;

            float magAdd = pAddForce.Length();

            //Add only the remaining force if adding will go over
            if (magAdd >= magLeft)
            {
                pAddForce.Normalize();
                Vector3.Multiply(pAddForce, magLeft);
            }

            pSteeringForce += pAddForce;

            return true;
        }
    }
}
