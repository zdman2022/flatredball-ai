using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FlatRedBall;
using Microsoft.Xna.Framework;
using FlatRedBallAI.AI.SteeringAgents.Helpers;

namespace FlatRedBallAI.AI.SteeringAgents.Behaviors
{
    public class FollowPathBehavior : IBehavior
    {
        private int mCurrentWayPoint;
        private bool mFinished;
        private List<Vector3> mPath;

        public FollowPathBehavior()
        {
            WayPointArrivedDistance = 10;
            mFinished = true;
            Loop = false;
            Weight = 1;
            Probability = 1;
            MaxSpeed = 1;
            Deceleration = .6f;
        }

        public float WayPointArrivedDistance { get; set; }
        public bool Loop { get; set; }
        public int MaxSpeed { get; set; }
        public float Deceleration { get; set; }

        #region IBehavior Members

        public float Weight{ get; set; }
        public float Probability { get; set; }

        Vector3 IBehavior.Calculate(PositionedObject pAgent)
        {
            //Make sure there is a path to follow
            if (mPath != null && mPath.Count > 0)
            {
                //Check if we have reached a waypoint
                if (Vector3.DistanceSquared(mPath[mCurrentWayPoint], pAgent.Position) <= WayPointArrivedDistance)
                {
                    //Go to next waypoint
                    mCurrentWayPoint += 1;

                    //Loop or marked finished if at the end
                    if (mCurrentWayPoint >= mPath.Count - 1)
                    {
                        if (Loop)
                        {
                            mCurrentWayPoint = 0;
                        }
                        else
                        {
                            mFinished = true;
                        }
                    }
                }

                if (!mFinished)
                {
                    return SteeringHelper.Seek(pAgent, mPath[mCurrentWayPoint], MaxSpeed);
                }else{
                    if(mCurrentWayPoint < mPath.Count)
                    {
                        return SteeringHelper.Arrive(pAgent, mPath[mCurrentWayPoint], Deceleration);
                    }
                }
            }

            return Vector3.Zero;
        }

        #endregion
    }
}
