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
    class ChasingTeam : IState<Chaser>
    {
        private double pAICalcTime;
        private SteeringAgent mSA;

        private List<Chaser> mFlockWith;
        private List<Runner> mChasing;
        private List<Circle> mCObstacles;
        private List<AxisAlignedRectangle> mRObstacles;

        #region IState<Runner> Members
        public ChasingTeam(List<Runner> pChasing, List<Chaser> pFlockWith, List<Circle> pCObstacles, List<AxisAlignedRectangle> pRObstacles)
        {
            mChasing = pChasing;
            mCObstacles = pCObstacles;
            mRObstacles = pRObstacles;
            mFlockWith = pFlockWith;
        }

        public void Enter(Chaser pInput)
        {
            mSA = new SteeringAgent();

            mSA.ForceCalculation = new WTRSP();
            mSA.Initialize(pInput, 10);
            pAICalcTime = 0.0;

            ////Behaviors////
            

            //Avoid Obstacles
            AvoidObstaclesBehavior avoidO = new AvoidObstaclesBehavior();

            avoidO.AgentRadius = pInput.Collision.Radius + 2;
            avoidO.CircleObstacles = mCObstacles;
            avoidO.DetectionLength = 5;
            avoidO.MaxSpeed = pInput.MaxSpeed;
            avoidO.RectangleObstacles = mRObstacles;
            avoidO.Weight = .9f;

            mSA.Behaviors.Add(avoidO);


            //Chasing
            ChaseBehavior newChase = new ChaseBehavior();

            newChase.Targets = mChasing.ConvertAll<PositionedObject>(delegate(Runner item) { return (PositionedObject)item; });
            newChase.MaxSpeed = pInput.MaxSpeed;
            newChase.Weight = .5f;

            mSA.Behaviors.Add(newChase);


            ////Wandering
            //WanderBehavior wander = new WanderBehavior();

            //wander.WanderDistance = 20;
            //wander.WanderRadius = 10;
            //wander.WanderJitter = .2f;
            //wander.Weight = .3f;

            //mSA.Behaviors.Add(wander);
        }

        public void Execute(Chaser pInput)
        {
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
