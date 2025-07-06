using SkiaSharp;
using SwordEngine.Entities;
using SwordEngine.Tiles;
using System.Drawing;

namespace SwordEngine.Weapons
{
    public abstract class Weapon
    {
        /// <summary>
        /// The generic weapon handler.
        /// </summary>
        protected Handler handler;
        
        /// <summary>
        /// Display sprite.
        /// </summary>
        protected SKBitmap displayTexture;

        /// <summary>
        /// Attack sprite.
        /// </summary>
        protected SKBitmap attkTexture;

        /// <summary>
        /// Default durability of weapons.
        /// </summary>
        public static int DEFAULT_DURABILITY = 20;

        /// <summary>
        /// Weapon durability.
        /// </summary>
        protected int durability;

        /// <summary>
        /// Default cooldown.
        /// </summary>
        public static int DEFAULT_COOLDOWN = 20;

        /// <summary>
        /// Whether or not the weapon is attacking.
        /// </summary>
        protected bool attacking = false;

        /// <summary>
        /// The cool down value.
        /// </summary>
        protected int coolDown;

        /// <summary>
        /// Hit box.
        /// </summary>
        protected Rectangle hitbox;

        /// <summary>
        /// The location of the weapon.
        /// </summary>
        protected float x, y;

        /// <summary>
        /// The damage value of the weapon when attacking.
        /// </summary>
        protected int damage;

        /// <summary>
        /// Whether the weapon has been picked up.
        /// </summary>
        protected bool pickedUp = false;

        /// <summary>
        /// The width and height of the weapon.
        /// </summary>
        protected int width, height;

        /// <summary>
        /// The default weapon constructor.
        /// </summary>
        /// <param name="handler"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        public Weapon(Handler handler, int width, int height)
        {
            this.handler = handler;
            durability = DEFAULT_DURABILITY;
            coolDown = -1;
            this.width = width;
            this.height = height;

            hitbox = new Rectangle(0, 0, width, height);
        }

        /// <summary>
        /// The weapon update method.
        /// </summary>
        /// <param name="e"></param>
        public abstract void Update(Entity e);

        /// <summary>
        /// Method used to attack.
        /// </summary>
        /// <param name="e">The entity the weapon collides with.</param>
        public void Attk(Entity e)
        {
            if (coolDown == -1)
            {
                attacking = true;
                foreach (Entity ent in handler.GetWorld().GetEntityManager().getEntities())
                {
                    if (ent == e)
                    {
                        continue;
                    }

                    if (ent.GetCollisionBounds(0, 0).IntersectsWith(hitbox))
                    {
                        ent.Hurt();
                        this.Hurt();
                    }
                }

                coolDown = DEFAULT_COOLDOWN;
            }
        }

        /// <summary>
        /// The weapon destruction method.
        /// </summary>
        public virtual void Die()
        {
            handler.GetWorld().GetEntityManager().GetPlayer().resetCurrWeap();
        }

        /// <summary>
        /// The weapon durability damage method.
        /// </summary>
        public virtual void Hurt()
        {
            durability--;
            if (durability <= 0)
            {
                Die();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="canvas"></param>
        public virtual void Render(SKCanvas canvas)
        {

            if (pickedUp)
            {
                Render(canvas, (hitbox.X - handler.GetGameCamera().GetXOffset())
                        , (hitbox.Y - handler.GetGameCamera().GetYOffset()));

                // draws a border box and durability box
                SKPaint blackRect = new SKPaint
                {
                    Color = SKColors.Empty,
                    StrokeWidth = 1,
                    IsAntialias = true,
                    Style = SKPaintStyle.Stroke
                };
                canvas.DrawRect((int)(handler.GetWidth() - 455)
                        , (int)(handler.GetHeight() - 75), (5 * DEFAULT_DURABILITY) + 10, 25, blackRect);

                SKPaint greenRect = new SKPaint
                {
                    Color = SKColors.Green,
                    StrokeWidth = 1,
                    IsAntialias = true,
                    Style = SKPaintStyle.Stroke
                };
                canvas.DrawRect((int)(handler.GetWidth() - 450)
                        , (int)(handler.GetHeight() - 70), (5 * this.durability), 15, greenRect);

                // draws a border box and the weapon's cooldown if it's on cooldown
                canvas.DrawRect((int)(handler.GetWidth() - 455)
                        , (int)(handler.GetHeight() - 55), (5 * DEFAULT_COOLDOWN) + 10, 25, blackRect);
                if (coolDown != -1)
                {
                    SKPaint blueRect = new SKPaint
                    {
                        Color = SKColors.Blue,
                        StrokeWidth = 1,
                        IsAntialias = true,
                        Style = SKPaintStyle.Stroke
                    };
                    canvas.DrawRect((int)(handler.GetWidth() - 450)
                            , (int)(handler.GetHeight() - 50), (5 * this.coolDown), 15, blueRect);
                }

                canvas.DrawRect((int)(handler.GetWidth() - 495)
                        , (int)(handler.GetHeight() - 75), 40, 45, blackRect);
                canvas.DrawBitmap(displayTexture, (int)(handler.GetWidth() - 490)
                        , (int)(handler.GetHeight() - 72), null);
            }
            else
            {
                canvas.DrawBitmap(displayTexture, (int)(x - handler.GetGameCamera().GetXOffset())
                        , (int)(y - handler.GetGameCamera().GetYOffset()), null);
            }
        }

        public virtual void Render(SKCanvas canvas, float x, float y)
        {
            if (!attacking)
            {
                if (attkTexture != null)
                {
                    canvas.DrawBitmap(attkTexture, (int)x, (int)y, null);
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
                        canvas.DrawBitmap(attkTexture, (int)x, (int)(y + (Tile.TILEHEIGHT / 2)), null);
                        break;
                    case ('l'):
                        canvas.DrawBitmap(attkTexture, (int)(x - (Tile.TILEWIDTH / 2)), (int)y, null);
                        break;
                    case ('r'):
                        canvas.DrawBitmap(attkTexture, (int)(x + (Tile.TILEWIDTH / 2)), (int)y, null);
                        break;
                    default:
                        return;
                }
            }
        }

        /// <summary>
        /// Gets the cooldown value.
        /// </summary>
        public int GetCoolDown()
        {
            return coolDown;
        }

        /// <summary>
        /// Gets the damage value.
        /// </summary>
        public int GetDamage()
        {
            return damage;
        }

        /// <summary>
        /// Gets the picked up value.
        /// </summary>
        /// <returns></returns>
        public bool IsPickedUp()
        {
            return pickedUp;
        }

        /// <summary>
        /// Abstract method for how long a cool down should last.
        /// </summary>
        public abstract void OnCoolDown();
    }
}
