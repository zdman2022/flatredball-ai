using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FlatRedBall;
using Microsoft.Xna.Framework;

namespace FlatRedBallAI.AI.SteeringAgents.Behaviors
{
    /// <summary>
    /// Interface for behaviors for a steering agent.
    /// </summary>
    public interface IBehavior
    {
        /// <summary>
        /// Weight behavior should be applied.  Between 0 and 1.
        /// </summary>
        float Weight { get; set; }

        /// <summary>
        /// Probability behavior should be applied.  Between 0 and 1.
        /// </summary>
        float Probability { get; set; }

        /// <summary>
        /// Calculates force to apply.
        /// </summary>
        /// <param name="pAgent">Source Agent</param>
        /// <returns>Force to apply to source agent.</returns>
        Vector3 Calculate(PositionedObject pAgent);
    }
}
