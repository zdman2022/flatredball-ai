using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FlatRedBall;
using Microsoft.Xna.Framework;
using FlatRedBallAI.AI.SteeringAgents.Helpers;

namespace FlatRedBallAI.AI.SteeringAgents.Behaviors
{
    public class FlockBehavior : IBehavior
    {
        public FlockBehavior()
        {
            MaxSpeed = 1;
            FlockingRadius = 10;
            FlockingAgents = new List<PositionedObject>();
            SeperationWeight = .8f;
            AlignmentWeight = .5f;
            CohesionWeight = .5f;
            Weight = 1;
            Probability = 1;
            Name = "Flock";
            StopDistance = 1f;
        }

        public List<PositionedObject> FlockingAgents { get; set; }
        public float FlockingRadius { get; set; }
        public int MaxSpeed { get; set; }

        public float SeperationWeight { get; set; }
        public float AlignmentWeight { get; set; }
        public float CohesionWeight { get; set; }

        #region IBehavior Members

        public float Weight{ get; set; }
        public float Probability { get; set; }
        public string Name { get; set; }
        public float StopDistance { get; set; } //distance to stop at the seek destination

        Vector3 IBehavior.Calculate(PositionedObject pAgent)
        {
            Vector3 SteeringForce = Vector3.Zero;

            if (FlockingAgents != null && FlockingAgents.Count > 0)
            {
                List<PositionedObject> Neighbors = SteeringHelper.GetNeighbors(pAgent, FlockingAgents, FlockingRadius);

                SteeringForce += SteeringHelper.Seperation(pAgent, Neighbors) * SeperationWeight;
                SteeringForce += SteeringHelper.Alignment(pAgent, Neighbors) * AlignmentWeight;
                SteeringForce += SteeringHelper.Cohesion(pAgent, Neighbors, MaxSpeed, StopDistance) * CohesionWeight;
            }

            return SteeringForce;
        }

        #endregion
    }
}
