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
using FlatRedBall.Math.Geometry;


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
	public partial class Character : PositionedObject
	{
        // This is made global so that static lazy-loaded content can access it.
        public static string ContentManagerName
        {
            get;
            set;
        }

		// Generated Fields
		private static ShapeCollection EntityCollisionFile;
		public int MaxSpeed = 0;
		static bool mHasRegisteredUnload = false;
		static object mLockObject = new object();

		private Circle mCollision;
		public Circle Collision
		{
			get{ return mCollision;}
		}

        Layer LayerProvidedByContainer = null;
        public Character(string contentManagerName) :
            this(contentManagerName, true)
        {
        }


        public Character(string contentManagerName, bool addToManagers) :
			base()
		{
			// Don't delete this:
            ContentManagerName = contentManagerName;
            Initialize(addToManagers);

		}

		protected virtual void Initialize(bool addToManagers)
		{
			// Generated Initialize
			LoadStaticContent(ContentManagerName);


			mCollision = EntityCollisionFile.Circles.FindByName("Circle1").Clone();
			SetCustomVariables();
			PostInitialize();
			if(addToManagers)
			{
				AddToManagers(null);
			}


		}

// Generated AddToManagers

        public virtual void AddToManagers(Layer layerToAddTo)
        {
			LayerProvidedByContainer = layerToAddTo;
			SpriteManager.AddPositionedObject(this);


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

			ShapeManager.AddToLayer(mCollision, layerToAddTo);
			mCollision.AttachTo(this, true);

            X = oldX;
            Y = oldY;
            Z = oldZ;
            RotationX = oldRotationX;
            RotationY = oldRotationY;
            RotationZ = oldRotationZ;
                			CustomInitialize();

        }

		public virtual void Activity()
		{
			// Generated Activity



			CustomActivity();
		}

		public virtual void Destroy()
		{
			// Generated Destroy
			SpriteManager.RemovePositionedObject(this);


			if(Collision != null)
			{
				ShapeManager.Remove(Collision);
			}


			CustomDestroy();
		}

		// Generated Methods
		public virtual void PostInitialize()
		{
		}
		public virtual void ConvertToManuallyUpdated()
		{
			SpriteManager.ConvertToManuallyUpdated(this);
		}
		protected virtual void SetCustomVariables()
		{
		}

		public static void LoadStaticContent(string contentManagerName)
		{
			ContentManagerName = contentManagerName;
			bool registerUnload = false;
			if(!FlatRedBallServices.IsLoaded(@"content/entities/character/entitycollisionfile.shcx", ContentManagerName))
			{
				registerUnload = true;
				EntityCollisionFile = FlatRedBallServices.Load<ShapeCollection>(@"content/entities/character/entitycollisionfile.shcx", ContentManagerName);
				FlatRedBallServices.AddNonDisposable(@"content/entities/character/entitycollisionfile.shcx", EntityCollisionFile, ContentManagerName);
			}
			else
			{
				EntityCollisionFile = (ShapeCollection)FlatRedBallServices.GetNonDisposable(@"content/entities/character/entitycollisionfile.shcx", ContentManagerName);
			}
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
						FlatRedBallServices.GetContentManagerByName(ContentManagerName).AddUnloadMethod("CharacterStaticUnload", UnloadStaticContent);
						mHasRegisteredUnload = true;
					}
				}
			}
			CustomLoadStaticContent(contentManagerName);
		}
		public static void UnloadStaticContent()
		{
			mHasRegisteredUnload = false;
			if(EntityCollisionFile != null)
			{
			}
		}

    }
}
