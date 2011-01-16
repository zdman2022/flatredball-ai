using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FlatRedBall;
using Microsoft.Xna.Framework;
using FlatRedBallAI.AI.SteeringAgents.Behaviors;
using FlatRedBallAI.AI.SteeringAgents.ForceCalculations;

namespace FlatRedBallAI.AI.SteeringAgents.Agents
{
    public class SteeringAgent
    {
        #region "Private Variables"
        private PositionedObject mAgent;
        private float mMaxForce;
        #endregion

        private List<IBehavior> mBehaviors = new List<IBehavior>();
        public List<IBehavior> Behaviors 
        { 
            get { return mBehaviors; }
        }

        private IForceCalculation mForceCalculation = new WTRSP();
        public IForceCalculation ForceCalculation
        {
            get { return mForceCalculation; }
            set
            {
                if (value != null)
                {
                    mForceCalculation = value;
                }
            }

        }

        #region "Public Methods"

        public void Initialize(PositionedObject pAgent, float pMaxForce)
        {
            mAgent = pAgent;
            mMaxForce = pMaxForce;
        }

        public Vector3 Calculate()
        {
            return mForceCalculation.Calculate(mAgent, Behaviors, mMaxForce);
        }

        #endregion
    }
}
