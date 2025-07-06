using SkiaSharp;
using SwordEngine.Entities;
using SwordEngine.Gfx;
using SwordEngine.Tiles;
using System.Drawing;

namespace SwordEngine.Weapons
{
    public class Axe : Weapon
    {
        // hitbox is 3 perpendicular to the front of the player
        public Axe(Handler handler, int width, int height, float x, float y) : base(handler, width, height)
        {
            displayTexture = Assets.axe[0];
            this.x = x;
            this.y = y;
            damage = 10;
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
                        attkTexture = Assets.axe[1];
                        hitbox.Width = width * 3;
                        hitbox.Height = height;
                        hitbox.X = (int)(c.X + c.Width / 2 - hitbox.Width / 2);
                        hitbox.Y = (int)(c.Y - hitbox.Height);
                        break;
                    case ('d'):
                        attkTexture = Assets.axe[4];
                        hitbox.Width = width * 3;
                        hitbox.Height = height;
                        hitbox.X = (int)(c.X + c.Width / 2 - hitbox.Width / 2);
                        hitbox.Y = c.Y + c.Height;
                        break;
                    case ('l'):
                        attkTexture = Assets.axe[3];
                        hitbox.Width = width;
                        hitbox.Height = height * 3;
                        hitbox.X = (int)(c.X - hitbox.Width);
                        hitbox.Y = (int)(c.Y + c.Height / 2 - hitbox.Height / 2);
                        break;
                    case ('r'):
                        attkTexture = Assets.axe[2];
                        hitbox.Width = width;
                        hitbox.Height = height * 3;
                        hitbox.X = c.X + c.Width;
                        hitbox.Y = (int)(c.Y + c.Height / 2 - hitbox.Height / 2);
                        break;
                    default:
                        attkTexture = Assets.axe[1];
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


        public void Render(SKCanvas canvas, float x, float y)
        {
            if (!attacking)
            {
                switch (handler.GetWorld().GetEntityManager().GetPlayer().GetLastDirection())
                {
                    case ('f'):
                        return;
                    case ('u'):
                        canvas.DrawBitmap(attkTexture, (int)(x + Tile.TILEWIDTH), (int)y, null);
                        break;
                    case ('d'):
                        canvas.DrawBitmap(attkTexture, (int)(x + Tile.TILEWIDTH), (int)y, null);
                        break;
                    case ('l'):
                        canvas.DrawBitmap(attkTexture, (int)x, (int)(y + Tile.TILEHEIGHT), null);
                        break;
                    case ('r'):
                        canvas.DrawBitmap(attkTexture, (int)x, (int)(y + Tile.TILEHEIGHT), null);
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
                        canvas.DrawBitmap(attkTexture, (int)(x + Tile.TILEWIDTH), (int)(y - (Tile.TILEHEIGHT / 2)), null);
                        break;
                    case ('d'):
                        canvas.DrawBitmap(attkTexture, (int)(x + Tile.TILEWIDTH), (int)(y + (Tile.TILEHEIGHT / 2)), null);
                        break;
                    case ('l'):
                        canvas.DrawBitmap(attkTexture, (int)(x - (Tile.TILEWIDTH / 2)), (int)(y + Tile.TILEHEIGHT), null);
                        break;
                    case ('r'):
                        canvas.DrawBitmap(attkTexture, (int)(x + (Tile.TILEWIDTH / 2)), (int)(y + Tile.TILEHEIGHT), null);
                        break;
                    default:
                        return;
                }
            }
        }

        public override void Hurt()
        {
            durability -= 4;
            if (durability <= 0)
            {
                Die();
            }
        }

        public override void OnCoolDown()
        {
            coolDown -= 1;
            if (coolDown <= 0)
            {
                coolDown = -1;
                attacking = false;
            }
        }

    }
}
