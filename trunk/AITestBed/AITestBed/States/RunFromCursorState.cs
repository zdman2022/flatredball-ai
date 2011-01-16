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
    class RunFromCursorState : IState<Chaser>
    {
        private double pAICalcTime;
        private SteeringAgent mSA;
        private FleePointBehavior mFleePointBehavior;
        #region IState<Chaser> Members

        public void Enter(Chaser pInput)
        {
            mSA = new SteeringAgent();

            mSA.ForceCalculation = new WTRSP();
            mSA.Initialize(pInput, 1);
            pAICalcTime = 0.0;
            mFleePointBehavior = new FleePointBehavior(new Vector3(pInput.X, pInput.Y, pInput.Z));

            mFleePointBehavior.MaxSpeed = 15;
            mFleePointBehavior.PanicDistance = 20;

            mSA.Behaviors.Add(mFleePointBehavior);
        }

        public void Execute(Chaser pInput)
        {
            if (TimeManager.CurrentTime >= pAICalcTime)
            {
                mFleePointBehavior.TargetPosition = new Vector3(InputManager.Mouse.WorldXAt(pInput.Z), InputManager.Mouse.WorldYAt(pInput.Z), 0);

                Vector3 force = mSA.Calculate();

                if (force.Length() == 0)
                {
                    pInput.Velocity = Vector3.Multiply(pInput.Velocity, .9f);
                    if (pInput.Velocity.Length() < .05)
                    {
                        pInput.Velocity = new Vector3();
                    }
                }
                else
                {
                    pInput.Velocity += force;
                }
                
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
