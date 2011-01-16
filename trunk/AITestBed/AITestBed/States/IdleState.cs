using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FlatRedBallAI.FiniteStateMachine;
using AITestBed.Entities;
using Microsoft.Xna.Framework;

namespace AITestBed.States
{
    class IdleState : IState<Chaser>, IState<Runner>
    {
        #region IState<Chaser> Members

        public void Enter(Chaser pInput)
        {
        }

        public void Execute(Chaser pInput)
        {
            pInput.Velocity = Vector3.Multiply(pInput.Velocity, .9f);

            if (pInput.Velocity.Length() < .05)
            {
                pInput.Velocity = new Vector3();
            }
        }

        public void Exit(Chaser pInput)
        {
        }

        #endregion

        #region IState<Runner> Members

        public void Enter(Runner pInput)
        {
        }

        public void Execute(Runner pInput)
        {
            pInput.Velocity = Vector3.Multiply(pInput.Velocity, .9f);

            if (pInput.Velocity.Length() < .05)
            {
                pInput.Velocity = new Vector3();
            }
        }

        public void Exit(Runner pInput)
        {
        }

        #endregion
    }
}
