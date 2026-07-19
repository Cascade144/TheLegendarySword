using SwordEngine.Entities;
using SwordEngine.Gfx;
using System.Drawing;

namespace SwordEngine.Weapons
{
    public class Dagger : Weapon
    {
        public Dagger(Handler handler, int width, int height) : base(handler, width, height)
        {
            displayTexture = Assets.dagger[0];
            pickedUp = true;
            damage = 1;
        }

        public override void Die()
        {
            // Does nothing, shouldn't be called.
        }

        public override void Hurt()
        {
            // does nothing since this is the default weapon
        }


        public override void Update(Entity e)
        {
            Rectangle c = e.GetCollisionBounds(0, 0);
            switch (e.GetLastDirection())
            {
                case ('f'):
                    return;
                case ('u'):
                    attkTexture = Assets.dagger[1];
                    hitbox.X = (int)(c.X + c.Width / 2 - hitbox.Width / 2);
                    hitbox.Y = (int)(c.Y - hitbox.Height);
                    break;
                case ('d'):
                    attkTexture = Assets.dagger[4];
                    hitbox.X = (int)(c.X + c.Width / 2 - hitbox.Width / 2);
                    hitbox.Y = c.Y + c.Height;
                    break;
                case ('l'):
                    attkTexture = Assets.dagger[3];
                    hitbox.X = (int)(c.X - hitbox.Width);
                    hitbox.Y = (int)(c.Y + c.Height / 2 - hitbox.Height / 2);
                    break;
                case ('r'):
                    attkTexture = Assets.dagger[2];
                    hitbox.X = c.X + c.Width;
                    hitbox.Y = (int)(c.Y + c.Height / 2 - hitbox.Height / 2);
                    break;
                default:
                    attkTexture = Assets.dagger[1];
                    break;
            }

        }

        public override void OnCoolDown()
        {
            coolDown -= 5;
            if (coolDown <= 0)
            {
                coolDown = -1;
                attacking = false;
            }
        }
    }
}
