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
using FlatRedBall.Math.Geometry;

namespace AITestBed.States
{
    class RunningTeam : IState<Runner>
    {
        private double pAICalcTime;
        private SteeringAgent mSA;

        private List<Runner> mFlockWith;
        private List<Chaser> mRunFrom;
        private List<Circle> mCObstacles;
        private List<AxisAlignedRectangle> mRObstacles;

        #region IState<Runner> Members
        public RunningTeam(List<Chaser> pRunFrom, List<Runner> pFlockWith, List<Circle> pCObstacles, List<AxisAlignedRectangle> pRObstacles)
        {
            mRunFrom = pRunFrom;
            mCObstacles = pCObstacles;
            mRObstacles = pRObstacles;
            mFlockWith = pFlockWith;
        }

        public void Enter(Runner pInput)
        {
            mSA = new SteeringAgent();

            mSA.ForceCalculation = new WTRSP();
            mSA.Initialize(pInput, 15);
            pAICalcTime = 0.0;

            /////Behaviors//////

            //Evading
            foreach (Chaser chasedBy in mRunFrom)
            {
                EvadeBehavior newEvade = new EvadeBehavior(chasedBy);

                newEvade.MaxSpeed = pInput.MaxSpeed;
                newEvade.PanicDistance = 10;
                newEvade.Weight = .9f;

                mSA.Behaviors.Add(newEvade);
            }


            //Avoiding Obstacles
            AvoidObstaclesBehavior avoidO = new AvoidObstaclesBehavior();

            avoidO.AgentRadius = pInput.Collision.Radius + 2;
            avoidO.CircleObstacles = mCObstacles;
            avoidO.DetectionLength = 10;
            avoidO.MaxSpeed = pInput.MaxSpeed;
            avoidO.RectangleObstacles = mRObstacles;
            avoidO.Weight = .9f;

            mSA.Behaviors.Add(avoidO);

            //Hiding
            foreach (Chaser chasedBy in mRunFrom)
            {
                HideBehavior hide = new HideBehavior(chasedBy);

                hide.BufferSpace = 2;
                hide.CircleObstacles = mCObstacles;
                hide.Deceleration = .3f;
                hide.PanicDistance = 20f;
                hide.Weight = .5f;

                mSA.Behaviors.Add(hide);
            }


            //Flocking
            FlockBehavior flock = new FlockBehavior();

            flock.FlockingAgents = mFlockWith.ConvertAll<PositionedObject>(delegate(Runner item) { return (PositionedObject)item; });
            flock.FlockingRadius = 10;
            flock.MaxSpeed = pInput.MaxSpeed;
            flock.Weight = .3f;

            mSA.Behaviors.Add(flock);


            //Wandering
            WanderBehavior wander = new WanderBehavior();

            wander.WanderDistance = 20;
            wander.WanderRadius = 5;
            wander.WanderJitter = .2f;
            wander.Weight = .3f;

            mSA.Behaviors.Add(wander);
        }

        public void Execute(Runner pInput)
        {
            if (TimeManager.CurrentTime >= pAICalcTime)
            {
                pInput.Velocity += mSA.Calculate();
                pAICalcTime += .005;
            }
        }

        public void Exit(Runner pInput)
        {
            mSA = null;
        }

        #endregion
    }
}
