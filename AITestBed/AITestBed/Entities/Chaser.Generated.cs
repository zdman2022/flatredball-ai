using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FlatRedBall;
using FlatRedBall.ManagedSpriteGroups;
using FlatRedBall.Math;
using FlatRedBall.Math.Geometry;
using FlatRedBall.AI.Pathfinding;
using FlatRedBall.Graphics;
using FlatRedBall.Graphics.Animation;
using FlatRedBall.Graphics.Particle;
#if !SILVERLIGHT
using FlatRedBall.Graphics.Model;
#endif
using FlatRedBall.Input;

using FlatRedBall.Instructions;
using FlatRedBall.Math.Splines;
using BitmapFont = FlatRedBall.Graphics.BitmapFont;
using Cursor = FlatRedBall.Gui.Cursor;
using GuiManager = FlatRedBall.Gui.GuiManager;
// Generated Usings
using AITestBed.Screens;
using Matrix = Microsoft.Xna.Framework.Matrix;
using FlatRedBall.Broadcasting;
using AITestBed.Entities;


#if FRB_XNA || SILVERLIGHT
using Keys = Microsoft.Xna.Framework.Input.Keys;
using Vector3 = Microsoft.Xna.Framework.Vector3;
using Texture2D = Microsoft.Xna.Framework.Graphics.Texture2D;
#endif

#if FRB_XNA
using Model = Microsoft.Xna.Framework.Graphics.Model;
#endif

namespace AITestBed.Entities
{
	public partial class Chaser : Character
	{
        // This is made global so that static lazy-loaded content can access it.
        public static new string ContentManagerName
        {
            get{ return Character.ContentManagerName;}
            set{ Character.ContentManagerName = value;}
        }

		// Generated Fields
		static bool mHasRegisteredUnload = false;
		static object mLockObject = new object();


        Layer LayerProvidedByContainer = null;
        public Chaser(string contentManagerName) :
            this(contentManagerName, true)
        {
        }


        public Chaser(string contentManagerName, bool addToManagers) :
			base(contentManagerName, addToManagers)
		{
			// Don't delete this:
            ContentManagerName = contentManagerName;
           

		}

		protected override void Initialize(bool addToManagers)
		{
			// Generated Initialize
			LoadStaticContent(ContentManagerName);


			X = 0f;
			MaxSpeed = 12;
			PostInitialize();
			base.Initialize(addToManagers);


		}

// Generated AddToManagers

        public override void AddToManagers(Layer layerToAddTo)
        {
			LayerProvidedByContainer = layerToAddTo;


            // We move this back to the origin and unrotate it so that anything attached to it can just use its absolute position
            float oldRotationX = RotationX;
            float oldRotationY = RotationY;
            float oldRotationZ = RotationZ;

            float oldX = X;
            float oldY = Y;
            float oldZ = Z;

            X = 0;
            Y = 0;
            Z = 0;
            RotationX = 0;
            RotationY = 0;
            RotationZ = 0;

            X = oldX;
            Y = oldY;
            Z = oldZ;
            RotationX = oldRotationX;
            RotationY = oldRotationY;
            RotationZ = oldRotationZ;
                
            base.AddToManagers(layerToAddTo);
			CustomInitialize();

        }

		public override void Activity()
		{
			// Generated Activity
			base.Activity();



			CustomActivity();
		}

		public override void Destroy()
		{
			// Generated Destroy
			base.Destroy();



			CustomDestroy();
		}

		// Generated Methods
		public override void PostInitialize()
		{
		}
		public override void ConvertToManuallyUpdated()
		{
			SpriteManager.ConvertToManuallyUpdated(this);
		}
		protected override void SetCustomVariables()
		{
			X = 0f;
			MaxSpeed = 12;
		}

		public static void LoadStaticContent(string contentManagerName)
		{
			ContentManagerName = contentManagerName;
			bool registerUnload = false;
			const bool throwExceptionIfAfterInitialize = false;
			if(throwExceptionIfAfterInitialize && registerUnload && ScreenManager.CurrentScreen != null && ScreenManager.CurrentScreen.ActivityCallCount > 0 && !ScreenManager.CurrentScreen.IsActivityFinished)
			{
				throw new InvalidOperationException("Content is being loaded after the current Screen is initialized.  This exception is being thrown because of a setting in Glue.");
			}
			if(registerUnload)
			{
				lock(mLockObject)
				{
					if(!mHasRegisteredUnload)
					{
						FlatRedBallServices.GetContentManagerByName(ContentManagerName).AddUnloadMethod("ChaserStaticUnload", UnloadStaticContent);
						mHasRegisteredUnload = true;
					}
				}
			}
			CustomLoadStaticContent(contentManagerName);
		}
		public static void UnloadStaticContent()
		{
			mHasRegisteredUnload = false;
			Character.UnloadStaticContent();

		}

    }
}
