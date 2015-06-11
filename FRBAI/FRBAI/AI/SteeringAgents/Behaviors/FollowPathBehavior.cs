using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FlatRedBall;
using Microsoft.Xna.Framework;
using FlatRedBallAI.AI.SteeringAgents.Helpers;
using FlatRedBall.AI.Pathfinding;

namespace FlatRedBallAI.AI.SteeringAgents.Behaviors
{
    public class FollowPathBehavior : IBehavior
    {
        private bool mFinished;
     
        public FollowPathBehavior()
        {
            WayPointArrivedDistance = 10;
            mFinished = false;
            Loop = false;
            Weight = 1;
            Probability = 1;
            MaxSpeed = 1;
            Deceleration = .6f;
            Name = "FollowPath";
            StopDistance = 1f;
            nodePath = new List<PositionedNode>();
            TargetPosition = new Vector3();
            ReversePathAfterReachingTarget = false;
        }

        public List<PositionedNode> nodePath { get; set; }
        public List<PositionedNode> tempNodePath { get; set; }
        public float WayPointArrivedDistance { get; set; }
        public bool Loop { get; set; }
        public bool ReversePathAfterReachingTarget { get; set; }
        public int MaxSpeed { get; set; }
        public float Deceleration { get; set; }
        public float StopDistance { get; set; }

        #region IBehavior Members

        public float Weight{ get; set; }
        public float Probability { get; set; }
        public string Name { get; set; }
        public Vector3 TargetPosition { get; set; }

        Vector3 IBehavior.Calculate(PositionedObject pAgent)
        {
            //Make sure there is a path to follow
            if (nodePath != null && nodePath.Count != 0)
            {
                if (Loop == true)
                {
                    //create tempNodePath when you want to keep looping the paths
                    if (tempNodePath == null)
                    {
                        tempNodePath = new List<PositionedNode>(nodePath);
                    }
                    else if (tempNodePath.Count <= nodePath.Count)
                    {
                        tempNodePath = new List<PositionedNode>(nodePath);
                    }
                }

                if (nodePath.Count == 1)
                {
                    if (Loop == true)
                    {
                        mFinished = false;
                    }
                    else
                    {
                        mFinished = true;
                    }
                }
                else
                {
                    mFinished = false;
                }

                PositionedNode node = nodePath[0];

                //Check if we have reached a waypoint
                if ((pAgent.Position - node.Position).Length() < WayPointArrivedDistance)
                {
                    nodePath.RemoveAt(0);

                    //reached the last waypoint, so set velocity to zero
                    //can still improve this part by making it slows down instead of resetting the speed to zero immediately
                    if (nodePath.Count == 0)
                    {
                        pAgent.Velocity = Vector3.Zero;
                    }
                }
                //it hasn't reached a waypoint, so seek for destination
                else
                {
                    if (mFinished == false)
                    {
                        TargetPosition = node.Position;
                        return SteeringHelper.Seek(pAgent, node.Position, MaxSpeed, StopDistance);
                    }
                    else
                    {
                        TargetPosition = node.Position;
                        return SteeringHelper.Arrive(pAgent, node.Position, Deceleration, StopDistance); 
                    }
                }
            }
            else if (nodePath.Count == 0)
            {
                if (Loop == true)
                {
                    nodePath = new List<PositionedNode>(tempNodePath);

                    //reverse path direction of how it reached the target/end point
                    if (ReversePathAfterReachingTarget == true)
                    {
                        nodePath.Reverse(0, nodePath.Count);
                    }
                }
                else
                {
                    mFinished = true;
                }
            }

            return Vector3.Zero;
        }

        #endregion
    }
}
