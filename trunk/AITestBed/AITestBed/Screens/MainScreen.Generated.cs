using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FlatRedBall;
using FlatRedBall.Graphics;
using FlatRedBall.ManagedSpriteGroups;
using FlatRedBall.Math;
using FlatRedBall.Math.Geometry;
using FlatRedBall.AI.Pathfinding;
using FlatRedBall.Graphics.Animation;
using FlatRedBall.Input;
using FlatRedBall.Graphics.Particle;
using FlatRedBall.Instructions;
using FlatRedBall.Math.Splines;
using BitmapFont = FlatRedBall.Graphics.BitmapFont;

using Cursor = FlatRedBall.Gui.Cursor;
using GuiManager = FlatRedBall.Gui.GuiManager;

#if FRB_XNA || SILVERLIGHT
using Keys = Microsoft.Xna.Framework.Input.Keys;
using Vector3 = Microsoft.Xna.Framework.Vector3;
using Texture2D = Microsoft.Xna.Framework.Graphics.Texture2D;
using Microsoft.Xna.Framework.Media;
#endif

// Generated Usings
using FlatRedBall.Broadcasting;
using AITestBed.Entities;
using FlatRedBall;
using FlatRedBall.Math.Geometry;

namespace AITestBed.Screens
{
	public partial class MainScreen : Screen
	{
        protected bool IsPaused = false;

        protected static double mAccumulatedPausedTime = 0;

        public static double PauseAdjustedCurrentTime
        {
            get { return TimeManager.CurrentTime - mAccumulatedPausedTime; }
        }

		// Generated Fields
		private Scene StarBackground;
		private Scene GameWorld;
		private ShapeCollection WorldCollision;

		private PositionedObjectList<Character> Characters;

		public MainScreen()
			: base("MainScreen")
		{
            mAccumulatedPausedTime = 0;
		}

        public override void Initialize(bool addToManagers)
        {
			// Generated Initialize
			StarBackground = FlatRedBallServices.Load<Scene>("content/screens/mainscreen/starbackground.scnx", ContentManagerName);
			GameWorld = FlatRedBallServices.Load<Scene>("content/screens/mainscreen/gameworld.scnx", ContentManagerName);
			WorldCollision = FlatRedBallServices.Load<ShapeCollection>("content/screens/mainscreen/worldcollision.shcx", ContentManagerName);
			LoadStaticContent(ContentManagerName);



			Characters = new PositionedObjectList<Character>();
			SetCustomVariables();
			PostInitialize();
			if(addToManagers)
			{
				AddToManagers();
			}

        }

// Generated AddToManagers

        public override void AddToManagers()
        {
			StarBackground.AddToManagers();

			GameWorld.AddToManagers();

			WorldCollision.AddToManagers();

			CustomInitialize();

        }


		public override void Activity(bool firstTimeCalled)
		{
			// Generated Activity
			if(!IsPaused)
			{
				StarBackground.ManageAll();				GameWorld.ManageAll();				;
				for(int i = Characters.Count - 1; i > -1; i--)
				{
					Characters[i].Activity();
				}
			}
			else
			{
			}

            if (IsPaused)
            {
                mAccumulatedPausedTime += TimeManager.SecondDifference;
            }

			base.Activity(firstTimeCalled);

			CustomActivity(firstTimeCalled);
		}

		public override void Destroy()
		{
			// Generated Destroy
			StarBackground.RemoveFromManagers();

			GameWorld.RemoveFromManagers();

			WorldCollision.RemoveFromManagers();


			while(Characters.Count != 0)
			{
				Characters[0].Destroy();
			}

			base.Destroy();

			CustomDestroy();

            if (IsPaused)
            {
                UnpauseThisScreen();
            }
		}

        void PauseThisScreen()
        {
            this.IsPaused = true;
            InstructionManager.PauseEngine();

        }

        void UnpauseThisScreen()
        {
            InstructionManager.UnpauseEngine();
            this.IsPaused = false;
        }

        public static double PauseAdjustedSecondsSince(double time)
        {
            return PauseAdjustedCurrentTime - time;
        }

		// Generated Methods
		public virtual void PostInitialize()
		{
		}
		public virtual void ConvertToManuallyUpdated()
		{
			StarBackground.ConvertToManuallyUpdated();
			GameWorld.ConvertToManuallyUpdated();
			for(int i = 0; i < Characters.Count; i++)
			{
				Characters[i].ConvertToManuallyUpdated();
			}
		}
		protected virtual void SetCustomVariables()
		{
		}

		public static void LoadStaticContent(string contentManagerName)
		{
			bool registerUnload = false;
			const bool throwExceptionIfAfterInitialize = false;
			if(throwExceptionIfAfterInitialize && registerUnload && ScreenManager.CurrentScreen != null && ScreenManager.CurrentScreen.ActivityCallCount > 0 && !ScreenManager.CurrentScreen.IsActivityFinished)
			{
				throw new InvalidOperationException("Content is being loaded after the current Screen is initialized.  This exception is being thrown because of a setting in Glue.");
			}
			CustomLoadStaticContent(contentManagerName);
		}


	}
}
