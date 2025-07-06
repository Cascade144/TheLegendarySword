using SwordEngine.Entities;
using SwordEngine.Gfx;
using System.Drawing;

namespace SwordEngine.Weapons
{
    public class LongSword : Weapon
    {
        public LongSword(Handler handler, int width, int height, float x, float y) : base(handler, width, height)
        {
            displayTexture = Assets.longSword[0];
            this.x = x;
            this.y = y;
            damage = 3;
        }

        public override void Update(Entity e)
        {
            Rectangle c = e.GetCollisionBounds(0, 0);
            if (pickedUp)
            {
                switch (e.GetLastDirection())
                {
                    case ('f'):
                        return;
                    case ('u'):
                        attkTexture = Assets.longSword[1];
                        hitbox.X = (int)(c.X + c.Width / 2 - hitbox.Width / 2);
                        hitbox.Y = (int)(c.Y - hitbox.Height);
                        break;
                    case ('d'):
                        attkTexture = Assets.longSword[4];
                        hitbox.X = (int)(c.X + c.Width / 2 - hitbox.Width / 2);
                        hitbox.Y = c.Y + c.Height;
                        break;
                    case ('l'):
                        attkTexture = Assets.longSword[3];
                        hitbox.X = (int)(c.X - hitbox.Width);
                        hitbox.Y = (int)(c.Y + c.Height / 2 - hitbox.Height / 2);
                        break;
                    case ('r'):
                        attkTexture = Assets.longSword[2];
                        hitbox.X = c.X + c.Width;
                        hitbox.Y = (int)(c.Y + c.Height / 2 - hitbox.Height / 2);
                        break;
                    default:
                        attkTexture = Assets.longSword[1];
                        break;
                }
            }
            else
            {
                hitbox.X = (int)x;
                hitbox.Y = (int)y;
                if (c.IntersectsWith(hitbox))
                {
                    e.SetNewWeapon(this);
                    pickedUp = true;
                }

            }

        }

        public override void OnCoolDown()
        {
            coolDown -= 3;
            if (coolDown <= 0)
            {
                coolDown = -1;
                attacking = false;
            }
        }
    }
}
