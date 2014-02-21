using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FlatRedBall;
using FlatRedBall.Input;
using FlatRedBall.AI.Pathfinding;
using FlatRedBall.Graphics.Animation;
using FlatRedBall.Graphics.Particle;

using FlatRedBallAI.FiniteStateMachine;
using AITestBed.States;

using FlatRedBall.Math.Geometry;
using FlatRedBall.Math.Splines;
using BitmapFont = FlatRedBall.Graphics.BitmapFont;
using Cursor = FlatRedBall.Gui.Cursor;
using GuiManager = FlatRedBall.Gui.GuiManager;

#if FRB_XNA || SILVERLIGHT
using Keys = Microsoft.Xna.Framework.Input.Keys;
using Vector3 = Microsoft.Xna.Framework.Vector3;
using Texture2D = Microsoft.Xna.Framework.Graphics.Texture2D;


#endif

namespace AITestBed.Entities
{
	public partial class Runner
	{
        public void Setup(List<Chaser> pEnemies, List<Runner> pAllies, List<Circle> pCObstacles, List<AxisAlignedRectangle> pRObstacles)
        {
            RunningTeam state = new RunningTeam(pEnemies, pAllies, pCObstacles, pRObstacles);

            mSM.ChangeState(state);
        }

        private StateMachine<Runner> mSM;
        private void CustomInitialize()
        {
            mSM = new StateMachine<Runner>(this);

            mSM.SetCurrentState(new States.IdleState());

            Collision.Color = Microsoft.Xna.Framework.Color.Green;
        }

        private void CustomActivity()
        {
            mSM.Update();
            if (Velocity.Length() > MaxSpeed)
            {
                Velocity.Normalize();
                Velocity = Vector3.Multiply(Velocity, (float)MaxSpeed);
            }
        }

        private void CustomDestroy()
        {
        }

        private static void CustomLoadStaticContent(string contentManagerName)
        {


        }
	}
}
