using BitmapFont = FlatRedBall.Graphics.BitmapFont;

using Cursor = FlatRedBall.Gui.Cursor;
using GuiManager = FlatRedBall.Gui.GuiManager;

#if XNA4 || WINDOWS_8
using Color = Microsoft.Xna.Framework.Color;
#elif FRB_MDX
using Color = System.Drawing.Color;
#else
using Color = Microsoft.Xna.Framework.Graphics.Color;
#endif

#if FRB_XNA || SILVERLIGHT
using Keys = Microsoft.Xna.Framework.Input.Keys;
using Vector3 = Microsoft.Xna.Framework.Vector3;
using Texture2D = Microsoft.Xna.Framework.Graphics.Texture2D;
using Microsoft.Xna.Framework.Media;
#endif

// Generated Usings
using AITestBed.Entities;
using FlatRedBall;
using FlatRedBall.Screens;
using System;
using System.Collections.Generic;
using System.Text;
using FlatRedBall.Math;
using FlatRedBall.Math.Geometry;

namespace AITestBed.Screens
{
	public partial class MainScreen : Screen
	{
		// Generated Fields
		#if DEBUG
		static bool HasBeenLoadedWithGlobalContentManager = false;
		#endif
		protected FlatRedBall.Scene StarBackground;
		protected FlatRedBall.Scene GameWorld;
		protected FlatRedBall.Math.Geometry.ShapeCollection WorldCollision;
		
		private PositionedObjectList<AITestBed.Entities.Character> Characters;

		public MainScreen()
			: base("MainScreen")
		{
		}

        public override void Initialize(bool addToManagers)
        {
			// Generated Initialize
			LoadStaticContent(ContentManagerName);
			if (!FlatRedBallServices.IsLoaded<FlatRedBall.Scene>(@"content/screens/mainscreen/starbackground.scnx", ContentManagerName))
			{
			}
			StarBackground = FlatRedBallServices.Load<FlatRedBall.Scene>(@"content/screens/mainscreen/starbackground.scnx", ContentManagerName);
			if (!FlatRedBallServices.IsLoaded<FlatRedBall.Scene>(@"content/screens/mainscreen/gameworld.scnx", ContentManagerName))
			{
			}
			GameWorld = FlatRedBallServices.Load<FlatRedBall.Scene>(@"content/screens/mainscreen/gameworld.scnx", ContentManagerName);
			if (!FlatRedBallServices.IsLoaded<FlatRedBall.Math.Geometry.ShapeCollection>(@"content/screens/mainscreen/worldcollision.shcx", ContentManagerName))
			{
			}
			WorldCollision = FlatRedBallServices.Load<FlatRedBall.Math.Geometry.ShapeCollection>(@"content/screens/mainscreen/worldcollision.shcx", ContentManagerName);
			Characters = new PositionedObjectList<AITestBed.Entities.Character>();
			
			
			PostInitialize();
			base.Initialize(addToManagers);
			if (addToManagers)
			{
				AddToManagers();
			}

        }
        
// Generated AddToManagers
		public override void AddToManagers ()
		{
			base.AddToManagers();
			AddToManagersBottomUp();
			CustomInitialize();
		}


		public override void Activity(bool firstTimeCalled)
		{
			// Generated Activity
			if (!IsPaused)
			{
				
				for (int i = Characters.Count - 1; i > -1; i--)
				{
					if (i < Characters.Count)
					{
						// We do the extra if-check because activity could destroy any number of entities
						Characters[i].Activity();
					}
				}
			}
			else
			{
			}
			base.Activity(firstTimeCalled);
			if (!IsActivityFinished)
			{
				CustomActivity(firstTimeCalled);
			}
			StarBackground.ManageAll();
			GameWorld.ManageAll();


				// After Custom Activity
				
            
		}

		public override void Destroy()
		{
			// Generated Destroy
			if (this.UnloadsContentManagerWhenDestroyed && ContentManagerName != "Global")
			{
				StarBackground.RemoveFromManagers(ContentManagerName != "Global");
			}
			else
			{
				StarBackground.RemoveFromManagers(false);
			}
			if (this.UnloadsContentManagerWhenDestroyed && ContentManagerName != "Global")
			{
				GameWorld.RemoveFromManagers(ContentManagerName != "Global");
			}
			else
			{
				GameWorld.RemoveFromManagers(false);
			}
			if (this.UnloadsContentManagerWhenDestroyed && ContentManagerName != "Global")
			{
				WorldCollision.RemoveFromManagers(ContentManagerName != "Global");
			}
			else
			{
				WorldCollision.RemoveFromManagers(false);
			}
			
			for (int i = Characters.Count - 1; i > -1; i--)
			{
				Characters[i].Destroy();
			}

			base.Destroy();

			CustomDestroy();

		}

		// Generated Methods
		public virtual void PostInitialize ()
		{
			bool oldShapeManagerSuppressAdd = FlatRedBall.Math.Geometry.ShapeManager.SuppressAddingOnVisibilityTrue;
			FlatRedBall.Math.Geometry.ShapeManager.SuppressAddingOnVisibilityTrue = true;
			FlatRedBall.Math.Geometry.ShapeManager.SuppressAddingOnVisibilityTrue = oldShapeManagerSuppressAdd;
		}
		public virtual void AddToManagersBottomUp ()
		{
			CameraSetup.ResetCamera(SpriteManager.Camera);
			StarBackground.AddToManagers(mLayer);
			GameWorld.AddToManagers(mLayer);
			WorldCollision.AddToManagers(mLayer);
		}
		public virtual void ConvertToManuallyUpdated ()
		{
			StarBackground.ConvertToManuallyUpdated();
			GameWorld.ConvertToManuallyUpdated();
			for (int i = 0; i < Characters.Count; i++)
			{
				Characters[i].ConvertToManuallyUpdated();
			}
		}
		public static void LoadStaticContent (string contentManagerName)
		{
			if (string.IsNullOrEmpty(contentManagerName))
			{
				throw new ArgumentException("contentManagerName cannot be empty or null");
			}
			#if DEBUG
			if (contentManagerName == FlatRedBallServices.GlobalContentManager)
			{
				HasBeenLoadedWithGlobalContentManager = true;
			}
			else if (HasBeenLoadedWithGlobalContentManager)
			{
				throw new Exception("This type has been loaded with a Global content manager, then loaded with a non-global.  This can lead to a lot of bugs");
			}
			#endif
			CustomLoadStaticContent(contentManagerName);
		}
		[System.Obsolete("Use GetFile instead")]
		public static object GetStaticMember (string memberName)
		{
			return null;
		}
		public static object GetFile (string memberName)
		{
			return null;
		}
		object GetMember (string memberName)
		{
			switch(memberName)
			{
				case  "StarBackground":
					return StarBackground;
				case  "GameWorld":
					return GameWorld;
				case  "WorldCollision":
					return WorldCollision;
			}
			return null;
		}


	}
}
