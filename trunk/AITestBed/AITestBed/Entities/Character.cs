using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FlatRedBall;
using FlatRedBall.Input;
using FlatRedBall.AI.Pathfinding;
using FlatRedBall.Graphics.Animation;
using FlatRedBall.Graphics.Particle;

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
	public partial class Character
	{
		private void CustomInitialize()
		{
		}

		private void CustomActivity()
		{


		}

		private void CustomDestroy()
		{


		}

        private static void CustomLoadStaticContent(string contentManagerName)
        {


        }

        public bool CollideAgainstStaticWorld(float thisMass, ShapeCollection worldCollision, float theirMass)
        {
            // Add your logic here
            return this.Collision.CollideAgainstMove(worldCollision, thisMass, theirMass);
        }

        public bool CollideAgainstCharacter(float thisMass, Character pCharacter, float theirMass)
        {
            // Add your logic here
            return this.Collision.CollideAgainstMove(pCharacter.Collision, thisMass, theirMass);
        }
	}
}
