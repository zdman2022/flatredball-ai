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
    class FollowMouseState : IState<Chaser>
    {
        private double pAICalcTime;
        private SteeringAgent mSA;
        private SeekPointBehavior mSeekPointBehavior;
        #region IState<Chaser> Members

        public void Enter(Chaser pInput)
        {
            mSA = new SteeringAgent();

            mSA.ForceCalculation = new WTRSP();
            mSA.Initialize(pInput, 1);
            pAICalcTime = 0.0;
            mSeekPointBehavior = new SeekPointBehavior(new Vector3(pInput.X, pInput.Y, pInput.Z));

            mSeekPointBehavior.MaxSpeed = 15;

            mSA.Behaviors.Add(mSeekPointBehavior);
        }

        public void Execute(Chaser pInput)
        {
            if (TimeManager.CurrentTime >= pAICalcTime)
            {
                mSeekPointBehavior.TargetPosition = new Vector3(InputManager.Mouse.WorldXAt(pInput.Z), InputManager.Mouse.WorldYAt(pInput.Z), 0);

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
