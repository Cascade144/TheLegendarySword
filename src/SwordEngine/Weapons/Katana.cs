using SwordEngine.Entities;
using SwordEngine.States;
using System.Drawing;
using SwordEngine.Gfx;

namespace SwordEngine.Weapons
{
    public class Katana : Weapon
    {
        /// <summary>
        /// Default constructor.
        /// </summary>
        /// <param name="handler"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public Katana(Handler handler, int width, int height, float x, float y): base(handler, width, height)
        {
            displayTexture = Assets.katana;
            this.x = x;
            this.y = y;
            damage = 9999;
        }

        /// <summary>
        /// Method to update game logic when using weap.
        /// </summary>
        /// <param name="e"></param>
        public override void Update(Entity e)
        {
            Rectangle c = e.GetCollisionBounds(0, 0);
            hitbox.X = (int)x;
            hitbox.Y = (int)y;
            if (c.IntersectsWith(hitbox))
            {
                State.SetState(handler.GetGame().GetWinState());
            }
        }

        /// <summary>
        /// What to do on a cool down.
        /// </summary>
        public override void OnCoolDown()
        {
            // nothing this weapon is unusable 
        }
    }
}
