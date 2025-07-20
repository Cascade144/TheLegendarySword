using SkiaSharp;
using SwordEngine.Entities;
using SwordEngine.Gfx;
using SwordEngine.Tiles;
using System.Drawing;

namespace SwordEngine.Weapons
{
    public class Spear : Weapon
    {
        public Spear(Handler handler, int width, int height, float x, float y) : base(handler, width, height)
        {
            displayTexture = Assets.spear[0];
            attkTexture = Assets.spear[1];
            this.x = x;
            this.y = y;
            damage = 3;
        }

        public override void Update(Entity e)
        {
            Rectangle c = e.GetCollisionBounds(0, 0);
            if (pickedUp && Assets.spear != null)
            {
                switch (e.GetLastDirection())
                {
                    case ('f'):
                        return;
                    case ('u'):
                        attkTexture = Assets.spear[1];
                        hitbox.Width = width;
                        hitbox.Height = height * 2;
                        hitbox.X = (int)(c.X + c.Width / 2 - hitbox.Width / 2);
                        hitbox.Y = (int)(c.Y - hitbox.Height);
                        break;
                    case ('d'):
                        attkTexture = Assets.spear[4];
                        hitbox.Width = width;
                        hitbox.Height = height * 2;
                        hitbox.X = (int)(c.X + c.Width / 2 - hitbox.Width / 2);
                        hitbox.Y = c.Y + c.Height;
                        break;
                    case ('l'):
                        attkTexture = Assets.spear[3];
                        hitbox.Width = width * 2;
                        hitbox.Height = height;
                        hitbox.X = (int)(c.X - hitbox.Width);
                        hitbox.Y = (int)(c.Y + c.Height / 2 - hitbox.Height / 2);
                        break;
                    case ('r'):
                        attkTexture = Assets.spear[2];
                        hitbox.Width = width * 2;
                        hitbox.Height = height;
                        hitbox.X = c.X + c.Width;
                        hitbox.Y = (int)(c.Y + c.Height / 2 - hitbox.Height / 2);
                        break;
                    default:
                        attkTexture = Assets.spear[1];
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

        public override void Render(SKCanvas canvas, float x, float y)
        {
            if (!attacking)
            {
                switch (handler.GetWorld().GetEntityManager().GetPlayer().GetLastDirection())
                {
                    case ('f'):
                        return;
                    case ('u'):
                        canvas.DrawBitmap(attkTexture, (int)x, (int)(y + (Tile.TILEHEIGHT / 2)), null);
                        break;
                    case ('d'):
                        canvas.DrawBitmap(attkTexture, (int)x, (int)y, null);
                        break;
                    case ('l'):
                        canvas.DrawBitmap(attkTexture, (int)(x + (Tile.TILEHEIGHT / 2)), (int)y, null);
                        break;
                    case ('r'):
                        canvas.DrawBitmap(attkTexture, (int)x, (int)y, null);
                        break;
                    default:
                        return;
                }
            }
            else
            {
                switch (handler.GetWorld().GetEntityManager().GetPlayer().GetLastDirection())
                {
                    case ('f'):
                        return;
                    case ('u'):
                        canvas.DrawBitmap(attkTexture, (int)x, (int)(y - (Tile.TILEHEIGHT / 2)), null);
                        break;
                    case ('d'):
                        canvas.DrawBitmap(attkTexture, (int)x, (int)(y + Tile.TILEHEIGHT), null);
                        break;
                    case ('l'):
                        canvas.DrawBitmap(attkTexture, (int)(x - (Tile.TILEWIDTH / 2)), (int)y, null);
                        break;
                    case ('r'):
                        canvas.DrawBitmap(attkTexture, (int)(x + Tile.TILEWIDTH), (int)y, null);
                        break;
                    default:
                        return;
                }
            }
        }

        public override void Hurt()
        {
            durability -= 2;
            if (durability <= 0)
            {
                Die();
            }
        }

        public override void OnCoolDown()
        {
            coolDown -= 2;
            if (coolDown <= 0)
            {
                coolDown = -1;
                attacking = false;
            }
        }
    }
}
