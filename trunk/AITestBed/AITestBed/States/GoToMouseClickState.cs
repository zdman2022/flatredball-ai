using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FlatRedBallAI.FiniteStateMachine;
using AITestBed.Entities;
using FlatRedBall.Input;
using FlatRedBallAI.AI.SteeringAgents.Agents;
using FlatRedBallAI.AI.SteeringAgents.Behaviors;
using FlatRedBallAI.AI.SteeringAgents.ForceCalculations;
using Microsoft.Xna.Framework;
using FlatRedBall;

namespace AITestBed.States
{
    class GoToMouseClickState : IState<Chaser>
    {
        private double pAICalcTime;
        private SteeringAgent mSA;
        #region IState<Chaser> Members

        public void Enter(Chaser pInput)
        {
            mSA = new SteeringAgent();

            mSA.ForceCalculation = new WTRSP();
            mSA.Initialize(pInput, 1);
            pAICalcTime = 0.0;
        }

        public void Execute(Chaser pInput)
        {
            if(InputManager.Mouse.ButtonPushed(Mouse.MouseButtons.LeftButton))
            {
                float xWorldPosition = InputManager.Mouse.WorldXAt(pInput.Z);
                float yWorldPosition = InputManager.Mouse.WorldYAt(pInput.Z);

                GoToPointBehavior tempBehavior = new GoToPointBehavior(new Vector3(xWorldPosition, yWorldPosition, pInput.Z));

                tempBehavior.Deceleration = .25f;

                mSA.Behaviors.Clear();
                mSA.Behaviors.Add(tempBehavior);
            }

            if (TimeManager.CurrentTime >= pAICalcTime)
            {
                pInput.Velocity += mSA.Calculate();
                pAICalcTime += .005;
            }
        }

        public void Exit(Chaser pInput)
        {
            mSA = null;
        }

        #endregion
    }
}
