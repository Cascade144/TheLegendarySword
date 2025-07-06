using SkiaSharp;
using SwordEngine.Entities;
using SwordEngine.Gfx;
using SwordEngine.Tiles;
using System.Drawing;

namespace SwordEngine.Weapons
{
    public class BallNChain : Weapon
    {
        private SKBitmap ballTexture;

        public BallNChain(Handler handler, int width, int height, float x, float y) : base(handler, width, height)
        {
            displayTexture = Assets.ballNChain[0];
            attkTexture = Assets.ballNChain[0];
            ballTexture = Assets.ballNChain[1];
            this.x = x;
            this.y = y;
            damage = 6;
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
                        hitbox.X = (int)(c.X + c.Width / 2 - hitbox.Width/ 2);
                        hitbox.Y = (int)(c.Y - hitbox.Height - Tile.TILEHEIGHT);
                        break;
                    case ('d'):
                        hitbox.X = (int)(c.X + c.Width / 2 - hitbox.Width / 2);
                        hitbox.Y = c.Y + c.Height + Tile.TILEHEIGHT;
                        break;
                    case ('l'):
                        hitbox.X = (int)(c.X - hitbox.Width - Tile.TILEWIDTH);
                        hitbox.Y = (int)(c.Y + c.Height / 2 - hitbox.Height / 2);
                        break;
                    case ('r'):
                        hitbox.X = c.X + c.Width + Tile.TILEWIDTH;
                        hitbox.Y = (int)(c.Y + c.Height / 2 - hitbox.Height / 2);
                        break;
                    default:
                        return;
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
                        canvas.DrawBitmap(attkTexture, (int)x, (int)(y + Tile.TILEHEIGHT), null);
                        break;
                    case ('d'):
                        canvas.DrawBitmap(attkTexture, (int)x, (int)(y - Tile.TILEHEIGHT), null);
                        break;
                    case ('l'):
                        canvas.DrawBitmap(attkTexture, (int)(x + Tile.TILEWIDTH), (int)y, null);
                        break;
                    case ('r'):
                        canvas.DrawBitmap(attkTexture, (int)(x - Tile.TILEWIDTH), (int)y, null);
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
                        canvas.DrawBitmap(ballTexture, (int)x, (int)y, null);
                        break;
                    case ('d'):
                        canvas.DrawBitmap(ballTexture, (int)x, (int)y, null);
                        break;
                    case ('l'):
                        canvas.DrawBitmap(ballTexture, (int)x, (int)y, null);
                        break;
                    case ('r'):
                        canvas.DrawBitmap(ballTexture, (int)x, (int)y, null);
                        break;
                    default:
                        return;
                }
            }
        }

        public override void Hurt()
        {
            durability -= 3;
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
