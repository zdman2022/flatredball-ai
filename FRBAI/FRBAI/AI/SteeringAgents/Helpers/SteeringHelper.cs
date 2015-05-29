using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FlatRedBall;
using Microsoft.Xna.Framework;
using FlatRedBall.Math.Geometry;
using FlatRedBall.AI.Pathfinding;
using FlatRedBall.Math;

namespace FlatRedBallAI.AI.SteeringAgents.Helpers
{
    public class SteeringHelper
    {
        private static Random mRandom = new Random();
        internal static Vector3 ZeroAxis = new Vector3(1, 0, 0);
        private static Vector3 ZAxis = new Vector3(0, 0, 1);

        /// <summary>
        /// Gets the angle between 2 vectors.  From -PI to PI based on first vector.
        /// </summary>
        /// <param name="pVector1">First Vector</param>
        /// <param name="pVector2">Second Vector</param>
        /// <returns>Angle in between.</returns>
        internal static float AngleBetween2Vectors(Vector3 pVector1, Vector3 pVector2)
        {
            pVector1.Normalize();
            pVector2.Normalize();

            //return (float)(Math.Atan2(pVector1.X * pVector2.Y - pVector2.X * pVector1.Y, pVector1.X * pVector2.X + pVector1.Y * pVector1.Y) % (2 * Math.PI));
            return (float)(Math.Atan2(pVector2.Y, pVector2.X) - Math.Atan2(pVector1.Y, pVector1.X));
        }

        /// <summary>
        /// Converts a local point to world coordinates.
        /// </summary>
        /// <param name="pLocalPoint">Local point to convert.</param>
        /// <param name="pZeroAxis">Axis considered zero.</param>
        /// <param name="pDirection">Direction facing.  What to rotate to from local.</param>
        /// <param name="pTranslation">Position.  What to move 0, 0 to.</param>
        /// <returns>Local point's world coordinate.</returns>
        internal static Vector3 PointToWorldSpace(Vector3 pLocalPoint, Vector3 pZeroAxis, Vector3 pDirection, Vector3 pTranslation)
        {
            float angle;

            //Calculate angle
            if (pDirection.Length() == 0)
            {
                angle = (float)mRandom.Next(6283) / (float)1000;
            }
            else
            {
                Vector3 vec1 = pZeroAxis;
                Vector3 vec2 = pDirection;

                angle = AngleBetween2Vectors(vec1, vec2);
            }

            //Create Rotation Matrix
            Matrix rotMatrix = Matrix.CreateFromAxisAngle(ZAxis, angle);

            //Create Translation Matrix
            Matrix tranMatrix = Matrix.CreateTranslation(pTranslation);

            //Rotate
            pLocalPoint = Vector3.Transform(pLocalPoint, rotMatrix);

            //Transform
            pLocalPoint = Vector3.Transform(pLocalPoint, tranMatrix);

            return pLocalPoint;
        }

        /// <summary>
        /// Converts a world point to local coordinates.
        /// </summary>
        /// <param name="pWorldPoint">World point to convert.</param>
        /// <param name="pZeroAxis">Axis considered zero.</param>
        /// <param name="pDirection">Direction facing.  What to rotate to from local.</param>
        /// <param name="pTranslation">Position.  What to consider 0, 0.</param>
        /// <returns>World point's local coordinate.</returns>
        internal static Vector3 PointToLocalSpace(Vector3 pWorldPoint, Vector3 pZeroAxis, Vector3 pDirection, Vector3 pTranslation)
        {
            float angle;

            //Calculate angle
            if (pDirection.Length() == 0)
            {
                angle = (float)mRandom.Next(6283) / (float)1000;
            }
            else
            {
                Vector3 vec1 = pDirection;
                Vector3 vec2 = pZeroAxis;

                angle = AngleBetween2Vectors(vec1, vec2);
            }

            //Create Rotation Matrix
            Matrix rotMatrix = Matrix.CreateFromAxisAngle(ZAxis, angle);

            //Create Translation Matrix
            Matrix tranMatrix = Matrix.CreateTranslation(-pTranslation);

            //Transform
            pWorldPoint = Vector3.Transform(pWorldPoint, tranMatrix);

            //Rotate
            pWorldPoint = Vector3.Transform(pWorldPoint, rotMatrix);

            return pWorldPoint;
        }

        /// <summary>
        /// Rotates a vector on the Z plane
        /// </summary>
        /// <param name="pVector">Vector to rotate</param>
        /// <param name="pAngle">Angle to rotate</param>
        /// <returns>Rotated Vector</returns>
        internal static Vector3 RotateVectorOnZPlane(Vector3 pVector, float pAngle)
        {
            Matrix rotMatrix = Matrix.CreateFromAxisAngle(ZAxis, pAngle);

            pVector = Vector3.Transform(pVector, rotMatrix);

            return pVector;
        }

        /// <summary>
        /// Converts a vector to world coordinates.
        /// </summary>
        /// <param name="pLocalPoint">Local vector to convert.</param>
        /// <param name="pZeroAxis">Axis considered zero.</param>
        /// <param name="pDirection">Direction facing.  What to rotate to from local.</param>
        /// <returns>Local vector's world coordinate.</returns>
        internal static Vector3 VectorToWorldSpace(Vector3 pLocalPoint, Vector3 pZeroAxis, Vector3 pDirection)
        {
            float angle;

            //Calculate angle
            if (pDirection.Length() == 0)
            {
                angle = (float)mRandom.Next(6283) / (float)1000;
            }
            else
            {
                Vector3 vec1 = pZeroAxis;
                Vector3 vec2 = pDirection;

                angle = AngleBetween2Vectors(vec1, vec2);
            }

            //Create Rotation Matrix
            Matrix rotMatrix = Matrix.CreateFromAxisAngle(new Vector3(0, 0, 1), angle);

            //Rotate
            pLocalPoint = Vector3.Transform(pLocalPoint, rotMatrix);

            return pLocalPoint;
        }

        /// <summary>
        /// Converts a world vector to local coordinates.
        /// </summary>
        /// <param name="pWorldPoint">World vector to convert.</param>
        /// <param name="pZeroAxis">Axis considered zero.</param>
        /// <param name="pDirection">Direction facing.  What to rotate to from local.</param>
        /// <returns>World vector's local coordinate.</returns>
        internal static Vector3 VectorToLocalSpace(Vector3 pWorldPoint, Vector3 pZeroAxis, Vector3 pDirection)
        {
            float angle;

            //Calculate angle
            if (pDirection.Length() == 0)
            {
                angle = (float)mRandom.Next(6283) / (float)1000;
            }
            else
            {
                Vector3 vec1 = pDirection;
                Vector3 vec2 = pZeroAxis;

                angle = AngleBetween2Vectors(vec1, vec2);
            }

            //Create Rotation Matrix
            Matrix rotMatrix = Matrix.CreateFromAxisAngle(new Vector3(0, 0, 1), angle);

            //Rotate
            pWorldPoint = Vector3.Transform(pWorldPoint, rotMatrix);

            return pWorldPoint;
        }

        /// <summary>
        /// Returns a list of circles that are within a certain distance of a point.
        /// </summary>
        /// <param name="pOrigin">Central point to calculate from.</param>
        /// <param name="pObjects">Circles to check if close enough.</param>
        /// <param name="pDistance">Distance to check.</param>
        /// <returns>List of circles that are within the specified distance.</returns>
        internal static PositionedObjectList<Circle> GetCloseObjects(Vector3 pOrigin, PositionedObjectList<Circle> pObjects, float pDistance)
        {
            PositionedObjectList<Circle> results = new PositionedObjectList<Circle>();

            foreach (Circle var in pObjects)
            {
                if (pDistance > Vector3.Distance(pOrigin, var.Position) - var.Radius)
                {
                    results.Add(var);
                }
            }

            return results;
        }

        /// <summary>
        /// Returns closest target.
        /// </summary>
        /// <returns>Closest target.</returns>
        internal static PositionedObject GetClosestTarget(Vector3 pOrigin, List<PositionedObject> pTargets)
        {
            PositionedObject closestTarget = null;
            float closestDistance = float.MaxValue;

            foreach (PositionedObject target in pTargets)
            {
                float distance = Vector3.DistanceSquared(target.Position, pOrigin);
                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    closestTarget = target;
                }
            }

            return closestTarget;
        }

        /// <summary>
        /// Returns the best hiding spot given an obstacle and a target.
        /// </summary>
        /// <param name="pTarget">Target to hide from.</param>
        /// <param name="pObstacle">Obstacle to hide behind.</param>
        /// <param name="pDistanceFromBoundary">How far from the obstacle to hide.</param>
        /// <returns>Position of hiding spot.</returns>
        internal static Vector3 GetHidingPosition(Vector3 pTarget, Circle pObstacle, float pDistanceFromBoundary)
        {
            float bufferRoom = pObstacle.Radius + pDistanceFromBoundary;

            Vector3 TargetDirection = (pObstacle.Position - pTarget);
            TargetDirection.Normalize();

            return (TargetDirection * bufferRoom) + pObstacle.Position;
        }

        /// <summary>
        /// Approaches the target at the fastest possible speed.
        /// </summary>
        /// <param name="pAgent">Source Agent</param>
        /// <param name="pTargetPos">Where to travel to.</param>
        /// <param name="pMaxSpeed">Maximum Speed of source agent.</param>
        /// <param name="pStopDistance">Distance to stop at the destination</param>
        /// <returns>Vector3 with the direction and speed to move.</returns>
        public static Vector3 Seek(PositionedObject pAgent, Vector3 pTargetPos, int pMaxSpeed, float pStopDistance)
        {
            Vector3 DesiredVelocity;
            Vector3 Result;

            float distance = Vector3.Distance(pTargetPos, pAgent.Position);

            if (distance > pStopDistance)
            {
                DesiredVelocity = Vector3.Normalize(pTargetPos - pAgent.Position) * pMaxSpeed;
            }
            else
            {
                //stop entity velocity after entity has reached target destination
                DesiredVelocity = Vector3.Zero;
                pAgent.Velocity = Vector3.Zero;
            }
           
            
            Result = (DesiredVelocity - pAgent.Velocity);
            return Result;
        }

        /// <summary>
        /// Runs from the target at the fastest possible speed.
        /// </summary>
        /// <param name="pAgent">Source Agent</param>
        /// <param name="pTargetPos">Where to run from.</param>
        /// <param name="pPanicDistance">Max distance to run.</param>
        /// <param name="pMaxSpeed">Max speed of source agent.</param>
        /// <returns>Vector3 with the direction and speed to move.</returns>
        public static Vector3 Flee(PositionedObject pAgent, Vector3 pTargetPos, float pPanicDistance, int pMaxSpeed)
        {
            float distance = Vector3.Distance(pAgent.Position, pTargetPos);

            if (distance > pPanicDistance)
            {
                return Vector3.Zero;
            }

            return Vector3.Normalize(pAgent.Position - pTargetPos) * ((float)(1f - (distance / (float)pMaxSpeed)) * (float)pMaxSpeed);
        }

        /// <summary>
        /// Approaches the target with deceleration.
        /// </summary>
        /// <param name="pAgent">Source Agent</param>
        /// <param name="pTargetPos">Where to travel to.</param>
        /// <param name="pDeceleration">How fast to decelerate.  Lower the number, the faster the deceleration
        /// <param name="pStopDistance">Distance to stop at the destination</param>
        /// Suggested: Fast = 0.3, Normal = 0.6, Slow = 0.9</param>
        /// <returns>Vector3 with the direction and speed to move.</returns>
        public static Vector3 Arrive(PositionedObject pAgent, Vector3 pTargetPos, float pDeceleration, float pStopDistance)
        {
            //Get the distance
            float dist = Vector3.Distance(pTargetPos, pAgent.Position);

            if (dist > pStopDistance)
            {
                //Calculate speed to reach using deceleration
                float speed = dist / pDeceleration;

                //Do regular seek without normalize
                Vector3 DesiredVelocity = (pTargetPos - pAgent.Position) * speed / dist;

                return (DesiredVelocity - pAgent.Velocity);
            }
            else
            {
                pAgent.Velocity = Vector3.Zero;
                return Vector3.Zero;
            }

        }

        /// <summary>
        /// Chases another moving object smartly.
        /// </summary>
        /// <param name="pAgent">Source Agent</param>
        /// <param name="pTarget">Moving object to pursue.</param>
        /// <param name="pMaxSpeed">Maximum Speed of source agent.</param>
        /// <returns>Vector3 with the direction and speed to move.</returns>
        public static Vector3 Pursuit(PositionedObject pAgent, PositionedObject pTarget, int pMaxSpeed, float pStopDistance)
        {
            //If just ahead, then just seek
            Vector3 ToTarget = pTarget.Position - pAgent.Position;

            float RelativeHeading = Vector3.Dot(pAgent.Velocity, pTarget.Velocity);

            if (Vector3.Dot(ToTarget, pAgent.Velocity) > 0 && RelativeHeading < -0.95)  //acos(0.95) = 18 degs
            {
                return Seek(pAgent, pTarget.Position, pMaxSpeed, pStopDistance);
            }


            //Not ahead

            //Look ahead time is proportional to the distance between the target and the pursuer
            //And is inversely proportional to the sum of the agents velocities

            float LookAheadTime = ToTarget.Length() / (pMaxSpeed + pTarget.Velocity.Length());

            //Now seek to the predicted future position of the evader
            return Seek(pAgent, pTarget.Position + pTarget.Velocity * LookAheadTime, pMaxSpeed, pStopDistance);
        }

        /// <summary>
        /// Runs from another moving object smartly.
        /// </summary>
        /// <param name="pAgent">Source Agent</param>
        /// <param name="pTarget">Moving object to run from.</param>
        /// <param name="pPanicDistance">Max distance to run.</param>
        /// <param name="pMaxSpeed">Max speed of source agent.</param>
        /// <returns>Vector3 with the direction and speed to move.</returns>
        public static Vector3 Evade(PositionedObject pAgent, PositionedObject pTarget, float pPanicDistance, int pMaxSpeed)
        {
            Vector3 ToTarget = pTarget.Position - pAgent.Position;

            //Look ahead time is proportional to the distance between the target and the pursuer
            //And is inversely proportional to the sum of the agents velocities

            float LookAheadTime = ToTarget.Length() / (pMaxSpeed + pTarget.Velocity.Length());

            //Now flee away from predicted future position of the target
            return Flee(pAgent, pTarget.Position + pTarget.Velocity * LookAheadTime, pPanicDistance, pMaxSpeed);
        }

        /// <summary>
        /// Applies a force to a random point projected forward.
        /// </summary>
        /// <param name="pAgent">Source Agent</param>
        /// <param name="pWanderRadius">Radius of circle the point that's projected can be generated.</param>
        /// <param name="pWanderDistance">How far the circle of the projected point will be.</param>
        /// <param name="pWanderJitter">Maximum amount of random displacement that can be added.</param>
        /// <returns>Vector3 with the direction and speed to move.</returns>
        public static Vector3 Wander(PositionedObject pAgent, ref Vector3 pTarget, float pWanderRadius, float pWanderDistance, float pWanderJitter)
        {
            //Jitter Target Point
            pTarget += new Vector3((((float)mRandom.Next(200) / 100f) - 1) * pWanderJitter, (((float)mRandom.Next(200) / 100f) - 1) * pWanderJitter, 0);

            //Reset Vector Length
            pTarget.Normalize();

            //Push Vector to edge of circle
            pTarget = Vector3.Multiply(pTarget, pWanderRadius);

            //Move point ahead
            Vector3 localTargetLocation = pTarget + new Vector3(pWanderDistance, 0, 0);

            //Convert to world coordinates
            Vector3 worldTargetLocation = PointToWorldSpace(localTargetLocation, ZeroAxis, pAgent.Velocity, pAgent.Position);

            return worldTargetLocation - pAgent.Position;
        }

        /// <summary>
        /// Adjusts to avoid obstacles ahead
        /// </summary>
        /// <param name="pAgent">Source Agent</param>
        /// <param name="pAgentRadius">Collision Radius of Source Agent</param>
        /// <param name="pObstacles">List of circles to avoid</param>
        /// <param name="pMaxSpeed">Maximum speed that can be traveled.</param>
        /// <param name="pMinDetectionBoxLength">Minimum length to check ahead.</param>
        /// <param name="pBreakWeight">How hard to stop when approaching an obstacle.</param>
        /// <returns>Vector3 with the direction and speed to move.</returns>
        public static Vector3 AvoidObstacles(PositionedObject pAgent, float pAgentRadius, PositionedObjectList<Circle> pObstacles, int pMaxSpeed, float pMinDetectionBoxLength, float pBreakWeight)
        {
            if(pAgent.Velocity.Length() > 0)
            {
                float boxLength = pMinDetectionBoxLength + (pAgent.Velocity.Length() / pMaxSpeed) * pMinDetectionBoxLength;

                PositionedObjectList<Circle> closeObjects = GetCloseObjects(pAgent.Position, pObstacles, boxLength);

                Circle closestObject = null;
                double closestObjectDistance = double.MaxValue;
                Vector3 closestObjectLocalPos = new Vector3();

                foreach (Circle var in closeObjects)
                {
                    Vector3 LocalPosition = PointToLocalSpace(var.Position, new Vector3(1, 0, 0), pAgent.Velocity, pAgent.Position);

                    //Check if obstacle is ahead
                    if (LocalPosition.X >= 0)
                    {
                        //Check if obstacle is in the way
                        if (Math.Abs(LocalPosition.Y) < (var.Radius + pAgentRadius))
                        {
                            double distance = Vector3.Distance(Vector3.Zero, LocalPosition) - var.Radius;

                            if (distance > 0 && distance < closestObjectDistance)
                            {
                                closestObjectDistance = distance;
                                closestObject = var;
                                closestObjectLocalPos = LocalPosition;
                            }
                        }
                    }
                }

                if(closestObject != null)
                {
                    Vector3 steeringForce = new Vector3();

                    //Calculate steering force if obstacle found
                    float multiplier = 1.0f + (boxLength - closestObjectLocalPos.X) / boxLength;

                    //Lateral Force
                    steeringForce.Y = (closestObject.Radius - closestObjectLocalPos.Y) * multiplier;

                    //Brake
                    steeringForce.X = (closestObject.Radius - closestObjectLocalPos.X) * pBreakWeight;

                    return VectorToWorldSpace(steeringForce, ZeroAxis, pAgent.Velocity);
                }

            }

            return Vector3.Zero;
        }

        /// <summary>
        /// Avoids rectanglar objects
        /// </summary>
        /// <param name="pAgent">Source Agent</param>
        /// <param name="pBarriers">Objects to avoid</param>
        /// <param name="pFeelerLength">How far ahead to search.</param>
        /// <returns>Vector3 with the direction and speed to move.</returns>
        public static Vector3 BarrierAvoidanceWithThreeFeelers(PositionedObject pAgent, PositionedObjectList<AxisAlignedRectangle> pBarriers, float pFeelerLength)
        {
            List<Vector3> feelers = new List<Vector3>();

            feelers.Add(pAgent.Velocity);
            feelers.Add(RotateVectorOnZPlane(pAgent.Velocity, (float)(Math.PI / 4)));
            feelers.Add(RotateVectorOnZPlane(pAgent.Velocity, (float)(-Math.PI / 4)));

            FlatRedBall.Math.Geometry.Point origin = new FlatRedBall.Math.Geometry.Point(pAgent.Position.X, pAgent.Position.Y);

            Line feelerIntersectLine = new Line();

            Vector3 SteeringForce = Vector3.Zero;

            foreach (Vector3 varFeeler in feelers)
            {
                float barrierDist = 0f;

                float closestDistanceToBarrier = float.MaxValue;
                AxisAlignedRectangle closestBarrier = null;
                FlatRedBall.Math.Geometry.Point closestIntersectionPoint = new FlatRedBall.Math.Geometry.Point();
                Segment closestLine = new Segment();

                feelerIntersectLine.Position = pAgent.Position;
                feelerIntersectLine.RelativePoint1 = new Point3D();
                varFeeler.Normalize();
                feelerIntersectLine.RelativePoint2 = new Point3D(varFeeler * pFeelerLength);

                foreach (AxisAlignedRectangle var in pBarriers)
                {
                    if (feelerIntersectLine.CollideAgainst(var))
                    {
                        Segment segIntersect = new Segment(new FlatRedBall.Math.Geometry.Point(feelerIntersectLine.AbsolutePoint1.X, feelerIntersectLine.AbsolutePoint1.Y), 
                            new FlatRedBall.Math.Geometry.Point(feelerIntersectLine.AbsolutePoint2.X, feelerIntersectLine.AbsolutePoint2.Y));
                        FlatRedBall.Math.Geometry.Point intersectPoint;
                        
                        List<Segment> sides = new List<Segment>();

                        sides.Add(new Segment(new FlatRedBall.Math.Geometry.Point(var.Left, var.Top), new FlatRedBall.Math.Geometry.Point(var.Right, var.Top))); //Top
                        sides.Add(new Segment(new FlatRedBall.Math.Geometry.Point(var.Left, var.Bottom), new FlatRedBall.Math.Geometry.Point(var.Left, var.Top)));  //Left
                        sides.Add(new Segment(new FlatRedBall.Math.Geometry.Point(var.Left, var.Bottom), new FlatRedBall.Math.Geometry.Point(var.Right, var.Bottom)));    //Bottom
                        sides.Add(new Segment(new FlatRedBall.Math.Geometry.Point(var.Right, var.Bottom), new FlatRedBall.Math.Geometry.Point(var.Right, var.Top)));   //Right

                        foreach(Segment side in sides)
                        {
                            if(segIntersect.Intersects(side, out intersectPoint))
                            {
                                barrierDist = (float)FlatRedBall.Math.Geometry.Point.DistanceTo(origin, intersectPoint);
                                if (closestDistanceToBarrier > barrierDist)
                                {
                                    closestDistanceToBarrier = barrierDist;
                                    closestBarrier = var;
                                    closestIntersectionPoint = intersectPoint;
                                    closestLine = side;
                                }
                            }
                        }
                    }
                }

                //Calculate force if feeler found barrier
                if (closestBarrier != null)
                {
                    Vector3 EndPoint = new Vector3((float)feelerIntersectLine.AbsolutePoint2.X, (float)feelerIntersectLine.AbsolutePoint2.Y, 0);
                    Vector3 IntersectionPoint = closestIntersectionPoint.ToVector3();

                    Vector3 OverShoot = EndPoint - IntersectionPoint;
                    Vector3 LineDirection = closestLine.ToVector3();
                    Vector3 OverShootDirection = OverShoot;
                    OverShootDirection.Normalize();
                    LineDirection.Normalize();

                    if (Math.Abs(AngleBetween2Vectors(LineDirection, OverShootDirection)) > (Math.PI / 2))
                    {
                        LineDirection *= -1f;
                    }

                    Vector3 Normal = LineDirection - OverShootDirection;
                    Normal.Normalize();

                    SteeringForce += Vector3.Multiply(Normal, OverShoot.Length());
                }
            }

            return SteeringForce;
        }


        /// <summary>
        /// Avoids polygon objects
        /// </summary>
        /// <param name="pAgent">Source Agent</param>
        /// <param name="pBarriers">Objects to avoid</param>
        /// <param name="pFeelerLength">How far ahead to search.</param>
        /// <returns>Vector3 with the direction and speed to move.</returns>
        public static Vector3 BarrierAvoidanceWithThreeFeelersPolygon(PositionedObject pAgent, PositionedObjectList<Polygon> pBarriers, float pFeelerLength)
        {
            List<Vector3> feelers = new List<Vector3>();

            feelers.Add(pAgent.Velocity);
            feelers.Add(RotateVectorOnZPlane(pAgent.Velocity, (float)(Math.PI / 4)));
            feelers.Add(RotateVectorOnZPlane(pAgent.Velocity, (float)(-Math.PI / 4)));

            FlatRedBall.Math.Geometry.Point origin = new FlatRedBall.Math.Geometry.Point(pAgent.Position.X, pAgent.Position.Y);

            Line feelerIntersectLine = new Line();

            Vector3 SteeringForce = Vector3.Zero;

            foreach (Vector3 varFeeler in feelers)
            {
                float barrierDist = 0f;

                float closestDistanceToBarrier = float.MaxValue;
                Polygon closestBarrier = null;
                FlatRedBall.Math.Geometry.Point closestIntersectionPoint = new FlatRedBall.Math.Geometry.Point();
                Segment closestLine = new Segment();

                feelerIntersectLine.Position = pAgent.Position;
                feelerIntersectLine.RelativePoint1 = new Point3D();
                varFeeler.Normalize();
                feelerIntersectLine.RelativePoint2 = new Point3D(varFeeler * pFeelerLength);

                foreach (Polygon var in pBarriers)
                {
                    if (feelerIntersectLine.CollideAgainst(var))
                    {
                        Segment segIntersect = new Segment(new FlatRedBall.Math.Geometry.Point(feelerIntersectLine.AbsolutePoint1.X, feelerIntersectLine.AbsolutePoint1.Y),
                            new FlatRedBall.Math.Geometry.Point(feelerIntersectLine.AbsolutePoint2.X, feelerIntersectLine.AbsolutePoint2.Y));
                        FlatRedBall.Math.Geometry.Point intersectPoint;

                        List<Segment> sides = new List<Segment>();

                        for (int i = 0; i < var.Points.Count; i++)
                        {
                            if (i < var.Points.Count - 1)
                            {
                                sides.Add(new Segment(new FlatRedBall.Math.Geometry.Point(var.Points[i].X + var.X, var.Points[i].Y + var.Y),
                                                                     new FlatRedBall.Math.Geometry.Point(var.Points[i + 1].X + var.X, var.Points[i + 1].Y + var.Y)));
                            }
                            else
                            {
                                sides.Add(new Segment(new FlatRedBall.Math.Geometry.Point(var.Points[i].X + var.X, var.Points[i].Y + var.Y),
                                                                     new FlatRedBall.Math.Geometry.Point(var.Points[0].X + var.X, var.Points[0].Y + var.Y)));
                            }
                        }

                        foreach (Segment side in sides)
                        {
                            if (segIntersect.Intersects(side, out intersectPoint))
                            {
                                barrierDist = (float)FlatRedBall.Math.Geometry.Point.DistanceTo(origin, intersectPoint);
                                if (closestDistanceToBarrier > barrierDist)
                                {
                                    closestDistanceToBarrier = barrierDist;
                                    closestBarrier = var;
                                    closestIntersectionPoint = intersectPoint;
                                    closestLine = side;
                                }
                            }
                        }
                    }
                }

                //Calculate force if feeler found barrier
                if (closestBarrier != null)
                {
                    Vector3 EndPoint = new Vector3((float)feelerIntersectLine.AbsolutePoint2.X, (float)feelerIntersectLine.AbsolutePoint2.Y, 0);
                    Vector3 IntersectionPoint = closestIntersectionPoint.ToVector3();

                    Vector3 OverShoot = EndPoint - IntersectionPoint;
                    Vector3 LineDirection = closestLine.ToVector3();
                    Vector3 OverShootDirection = OverShoot;
                    OverShootDirection.Normalize();
                    LineDirection.Normalize();

                    if (Math.Abs(AngleBetween2Vectors(LineDirection, OverShootDirection)) > (Math.PI / 2))
                    {
                        LineDirection *= -1f;
                    }

                    Vector3 Normal = (LineDirection - OverShootDirection);
                    Normal.Normalize();

                    SteeringForce += Vector3.Multiply(Normal, OverShoot.Length());
                }

            }

            return SteeringForce;
        }


        /// <summary>
        /// Moves inbetween 2 objects
        /// </summary>
        /// <param name="pAgent">Source Agent</param>
        /// <param name="pTarget1">First target</param>
        /// <param name="pTarget2">Second target</param>
        /// <param name="pMaxSpeed">Maximum Speed of Source Agent</param>
        /// <param name="pDeceleration">How fast to get there.  See arrive.</param>
        /// <param name="pStopDistance">Distance to stop at the destination</param>
        /// <returns>Vector3 with the direction and speed to move.</returns>
        public static Vector3 Interpose(PositionedObject pAgent, PositionedObject pTarget1, PositionedObject pTarget2, int pMaxSpeed, float pDeceleration, float pStopDistance)
        {
            Vector3 MidPoint = (pTarget1.Position + pTarget2.Position) / 2f;

            float MidPointTime = Vector3.Distance(pAgent.Position, MidPoint) / (float)pMaxSpeed;

            Vector3 Target1NewPos = pTarget1.Position + pTarget1.Velocity * MidPointTime;
            Vector3 Target2NewPos = pTarget2.Position + pTarget2.Velocity * MidPointTime;

            MidPoint = (Target1NewPos + Target2NewPos) / 2f;

            return Arrive(pAgent, MidPoint, pDeceleration, pStopDistance);
        }

        /// <summary>
        /// Attempts to hide from a target.  If it can't hide, then just evades.
        /// </summary>
        /// <param name="pAgent">Source Agent</param>
        /// <param name="pTarget">Target to hide from.</param>
        /// <param name="pObstacles">Obstacles to hide behind.</param>
        /// <param name="pHideDistanceFromObstacle">How far away from the obstacle to position the source agent.</param>
        /// <param name="pPanicDistance">When evading, this distance determines when to run.  See evade.</param>
        /// <param name="pMaxSpeed">Max speed source agent can travel.</param>
        /// <param name="pDeceleration">How fast to reach hiding spot.  See Arrive.</param>
        /// <param name="pStopDistance">Distance to stop at the destination</param>
        /// <returns>Vector3 with the direction and speed to move.</returns>
        public static Vector3 Hide(PositionedObject pAgent, PositionedObject pTarget, PositionedObjectList<Circle> pObstacles, float pHideDistanceFromObstacle, float pDeceleration, float pStopDistance)
        {
            float Closest = float.MaxValue;
            Vector3 BestHidingSpot = Vector3.Zero;

            foreach(Circle obs in pObstacles)
            {
                Vector3 HidingSpot = GetHidingPosition(pTarget.Position, obs, pHideDistanceFromObstacle);

                float dist = Vector3.DistanceSquared(HidingSpot, pAgent.Position);

                if (dist < Closest)
                {
                    Closest = dist;

                    BestHidingSpot = HidingSpot;
                }
            }

            if (Closest == float.MaxValue)
            {
                return Vector3.Zero;
            }

            return Arrive(pAgent, BestHidingSpot, pDeceleration, pStopDistance);
        }

        /// <summary>
        /// Tries to get away from neighbors
        /// </summary>
        /// <param name="pAgent">Source Agent</param>
        /// <param name="pNeighbors">Nearby Agents</param>
        /// <returns>Vector3 with the direction and speed to move.</returns>
        public static Vector3 Seperation(PositionedObject pAgent, List<PositionedObject> pNeighbors)
        {
            Vector3 SteeringForce = Vector3.Zero;

            foreach (PositionedObject Neighbor in pNeighbors)
            {
                Vector3 ToAgent = pAgent.Position - Neighbor.Position;
                float ToAgentLength = ToAgent.Length();
                ToAgent.Normalize();

                if (ToAgentLength != 0)
                    SteeringForce += ToAgent / ToAgentLength;
            }

            return SteeringForce;
        }

        /// <summary>
        /// Tries to keep source agent traveling in the same direction and speed as it's neighbors
        /// </summary>
        /// <param name="pAgent">Source Agent</param>
        /// <param name="pNeighbors">Neighboring agents</param>
        /// <returns>Vector3 with the direction and speed to move.</returns>
        public static Vector3 Alignment(PositionedObject pAgent, List<PositionedObject> pNeighbors)
        {
            Vector3 AverageHeading = Vector3.Zero;

            foreach (PositionedObject Neighbor in pNeighbors)
            {
                AverageHeading += Neighbor.Velocity;
            }

            if (pNeighbors.Count > 0)
            {
                AverageHeading = Vector3.Divide(AverageHeading, (float)pNeighbors.Count);

                AverageHeading -= pAgent.Velocity;
            }

            return AverageHeading;
        }

        /// <summary>
        /// Attempts to keep agents grouped together.
        /// </summary>
        /// <param name="pAgent">Source Agent</param>
        /// <param name="pNeighbors">Nearby agents.</param>
        /// <param name="MaxSpeed">Maximum speed source agent can travel.</param>
        /// <returns>Vector3 with the direction and speed to move.</returns>
        public static Vector3 Cohesion(PositionedObject pAgent, List<PositionedObject> pNeighbors, int MaxSpeed, float pStopDistance)
        {
            Vector3 CenterOfMass = Vector3.Zero;
            Vector3 SteeringForce = Vector3.Zero;

            foreach (PositionedObject Neighbor in pNeighbors)
            {
                CenterOfMass += Neighbor.Position;
            }

            if (pNeighbors.Count > 0)
            {
                CenterOfMass /= pNeighbors.Count;

                SteeringForce = Seek(pAgent, CenterOfMass, MaxSpeed, pStopDistance);
            }

            return SteeringForce;
        }

        /// <summary>
        /// Creates list of other agents within a certain distance.
        /// </summary>
        /// <param name="pAgent">Source Agent.</param>
        /// <param name="pOthers">Other Agents to check against.</param>
        /// <param name="pRadius">Radius around agent to consider.</param>
        /// <returns>List of nearby Agents.</returns>
        public static List<PositionedObject> GetNeighbors(PositionedObject pAgent, List<PositionedObject> pOthers, float pRadius)
        {
            List<PositionedObject> Neighbors = new List<PositionedObject>();

            foreach (PositionedObject other in pOthers)
            {
                if (!Object.ReferenceEquals(pAgent, other))
                {
                    Vector3 Distance = other.Position - pAgent.Position;

                    float range = pRadius + pRadius;

                    if (Distance.LengthSquared() < (range * range))
                    {
                        Neighbors.Add(other);
                    }
                }
            }

            return Neighbors;
        }
    }
}
