using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FlatRedBall;
using FlatRedBall.Input;
using FlatRedBall.AI.Pathfinding;
using FlatRedBall.Graphics.Animation;
using FlatRedBall.Graphics.Particle;

using AITestBed.Entities;

using FlatRedBall.Graphics.Model;
using FlatRedBall.Math.Geometry;
using FlatRedBall.Math.Splines;

using Cursor = FlatRedBall.Gui.Cursor;
using GuiManager = FlatRedBall.Gui.GuiManager;

#if FRB_XNA || SILVERLIGHT
using Keys = Microsoft.Xna.Framework.Input.Keys;
using Vector3 = Microsoft.Xna.Framework.Vector3;
using Texture2D = Microsoft.Xna.Framework.Graphics.Texture2D;
#endif

namespace AITestBed.Screens
{
	public partial class MainScreen
	{
        private const float CameraSpeed = .5f;
        private List<Chaser> mChasers = new List<Chaser>();
        private List<Runner> mRunners = new List<Runner>();
        private Random mRandom = new Random();
 
		public void CustomInitialize()
		{
            FlatRedBallServices.Game.IsMouseVisible = true;

            Runner tempRunner;
            Chaser tempChaser;

            for (int i = 0; i < 5; i++)
            {
                tempRunner = new Runner(ContentManagerName);
                tempRunner.Initialize();
                tempRunner.Position = new Vector3(mRandom.Next(50) - 25, mRandom.Next(50) - 25, 0);
                tempRunner.MaxSpeed = 20;
                mRunners.Add(tempRunner);
                Characters.Add(tempRunner);
            }

            for (int i = 0; i < 2; i++)
            {
                tempChaser = new Chaser(ContentManagerName);
                tempChaser.Initialize();
                tempChaser.Position = new Vector3(mRandom.Next(50) - 25, mRandom.Next(50) - 25, 0);
                tempChaser.MaxSpeed = 16;
                mChasers.Add(tempChaser);
                Characters.Add(tempChaser);
            }

            foreach (Runner var in mRunners)
            {
                var.Setup(mChasers, mRunners, WorldCollision.Circles.ToList<Circle>(), WorldCollision.AxisAlignedRectangles.ToList<AxisAlignedRectangle>());
            }

            foreach (Chaser var in mChasers)
            {
                var.Setup(mRunners, mChasers, WorldCollision.Circles.ToList<Circle>(), WorldCollision.AxisAlignedRectangles.ToList<AxisAlignedRectangle>());
            }
		}

		public void CustomActivity(bool firstTimeCalled)
		{
            if(InputManager.Keyboard.KeyDown(Keys.Left))
            {
                SpriteManager.Camera.Position.X -= CameraSpeed;
            }

            if(InputManager.Keyboard.KeyDown(Keys.Right))
            {
                SpriteManager.Camera.Position.X += CameraSpeed;
            }

            if(InputManager.Keyboard.KeyDown(Keys.Up))
            {
                SpriteManager.Camera.Position.Y += CameraSpeed;
            }

            if(InputManager.Keyboard.KeyDown(Keys.Down))
            {
                SpriteManager.Camera.Position.Y -= CameraSpeed;
            }

            if (InputManager.Keyboard.KeyDown(Keys.PageDown))
            {
                SpriteManager.Camera.Position.Z -= CameraSpeed;
            }

            if (InputManager.Keyboard.KeyDown(Keys.PageUp))
            {
                SpriteManager.Camera.Position.Z += CameraSpeed;
            }

            //Collision
            foreach (Character agent in Characters)
            {
                //Collide against world
                agent.CollideAgainstStaticWorld(0, WorldCollision, 1);

                //Collide against other characters
                foreach (Character otherAgent in Characters)
                {
                    if (!Object.ReferenceEquals(agent, otherAgent))
                    {
                        if (agent.CollideAgainstCharacter(1, otherAgent, 1))
                        {
                            if ((agent is Chaser || otherAgent is Chaser) && (agent is Runner || otherAgent is Runner))
                            {
                                if (agent is Runner)
                                {
                                    agent.Position = new Vector3(mRandom.Next(50) - 25, mRandom.Next(50) - 25, 0);
                                }
                                else
                                {
                                    otherAgent.Position = new Vector3(mRandom.Next(50) - 25, mRandom.Next(50) - 25, 0);
                                }
                            }
                        }
                    }
                }
            }

		}

		public void CustomDestroy()
		{


		}

        private static void CustomLoadStaticContent(string contentManagerName)
        {


        }

	}
}
