using SkiaSharp;
using SwordEngine.Weapons;
using System.Drawing;

namespace SwordEngine.Entities
{
    /// <summary>
    /// The abstract entity class.
    /// </summary>
    public abstract class Entity
    {
        /// <summary>
        /// Handler for Entity class.
        /// </summary>
        protected Handler handler;

        /// <summary>
        /// The entity's x and y position in pixels.
        /// </summary>
        protected float x, y;

        /// <summary>
        /// The entity's width and height in pixels.
        /// </summary>
        protected int width, height;

        /// <summary>
        /// The entity's hitbox, for entities other than the player, this will also be the attackbox.
        /// </summary>
        protected Rectangle bounds;

        /// <summary>
        /// Default health.
        /// </summary>
        public static int DEFAULT_HEALTH = 25;

        /// <summary>
        /// The health of any respective creature.
        /// </summary>
        protected int health;

        /// <summary>
        ///  Determines whether or not the entity is alive. (if not it won't be rendered)
        /// </summary>
        private char lastDirection = 'f';

        /// <summary>
        /// 
        /// </summary>
        protected bool alive = true;

        /// <summary>
        /// 
        /// </summary>
        protected bool isPlayer = false;

        /// <summary>
        /// Constructs an entity object setting its variables and 
        /// creating its bounding box(using some simple defaults)
        /// </summary>
        /// <param name="handler">The main handler object.</param>
        /// <param name="x">The main handler object.</param>
        /// <param name="y">The entity's y position in tixels (tile pixels).</param>
        /// <param name="width">The entity's width in pixels.</param>
        /// <param name="height">The entity's height in pixels.</param>
        public Entity(Handler handler, float x, float y, int width, int height) 
        {
            this.handler = handler;
            this.x = x;
            this.y = y;
            this.width = width;
            this.height = height;

            health = DEFAULT_HEALTH;

            bounds = new Rectangle(0, 0, width, height);
        }

        /// <summary>
        /// The entity's update method, differs between entities.
        /// </summary>
        public abstract void Update();

        /// <summary>
        /// The entity's render method, differs between entities
        /// </summary>
        /// <param name="g">The graphics object needed to be able to draw to the screen</param>
        public abstract void Render(SKCanvas canvas);

        /// <summary>
        ///  Goes through every entity and checks if there is an 
        ///  intersection between an entity and this entity's next x/y move location
        /// </summary>
        /// <param name="xOffset"></param>
        /// <param name="yOffset"></param>
        /// <returns></returns>
        public bool checkEntityCollisions(float xOffset, float yOffset)
        {
            foreach (Entity e in handler.GetWorld().GetEntityManager().GetEntities())
            {
                // The entity is obviously going to collide with itself, ignore it
                if (e == this)
                {
                    continue;
                }

                // Check if the current entity e's hitbox intersects with this entity's next x/y move location
                if (e.GetCollisionBounds(0f, 0f).IntersectsWith(GetCollisionBounds(xOffset, yOffset)))
                {
                    if (e.isPlayer)
                    {
                        e.Hurt();
                    }

                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Takes the entity that called it, and returns a 
        /// rectangle object that represents its hitbox
        /// </summary>
        /// <param name="xOffset"></param>
        /// <param name="yOffset"></param>
        /// <returns></returns>
        public Rectangle GetCollisionBounds(float xOffset, float yOffset)
        {
            return new Rectangle((int)(x + bounds.X + xOffset), (int)(y + bounds.Y + yOffset), bounds.Width, bounds.Height);
        }

        /// <summary>
        /// Entity is destroyed via this method.
        /// </summary>
        public abstract void Die();

        /// <summary>
        /// Hurts the current entity.
        /// </summary>
        public virtual void Hurt()
        {
            health -= handler.GetWorld().GetEntityManager().GetPlayer().GetCurrWeapon().GetDamage();
            if (health <= 0)
            {
                alive = false;
                Die();
            }
        }

        #region Getters and Setters

        /// <summary>
        /// Returns the entity's x position in tixels.
        /// </summary>
        public float GetX()
        {
            return x;
        }

        /// <summary>
        /// Sets the entity's x position to the given tixel amount.
        /// </summary>
        /// <param name="x">The x position to set.</param>
        public void SetX(float x)
        {
            this.x = x;
        }


        /// <summary>
        /// Returns the entity's y position in tixels.
        /// </summary>
        public float GetY()
        {
            return y;
        }

        /// <summary>
        ///  Sets the entity's y position to the given tixel amount.
        /// </summary>
        /// <param name="y">The y position to set.</param>
        public void SetY(float y)
        {
            this.y = y;
        }

        /// <summary>
        /// Returns the width of the entity in pixels.
        /// </summary>
        public int GetWidth()
        {
            return width;
        }

        /// <summary>
        /// Sets the entity's width value to the given pixel amount
        /// </summary>
        /// <param name="width">The width to set.</param>
        public void SetWidth(int width)
        {
            this.width = width;
        }

        /// <summary>
        /// Returns the height of the entity in pixels
        /// </summary>
        public int SetHeight()
        {
            return height;
        }

        /// <summary>
        /// Sets the entity's height value to the given pixel amount.
        /// </summary>
        /// <param name="height">The height to set.</param>
        public void SetHeight(int height)
        {
            this.height = height;
        }

        /// <summary>
        /// The health to get.
        /// </summary>
        public virtual int GetHealth()
        {
            return health;
        }

        /// <summary>
        /// The health to set.
        /// </summary>
        public virtual void SetHealth(int health)
        {
            this.health = health;
        }

        /// <summary>
        /// Whether the entity is alive or dead.
        /// </summary>
        public bool IsAlive()
        {
            return alive;
        }

        /// <summary>
        /// Brings the entity to life.
        /// </summary>
        /// <param name="alive"></param>
        public void SetAlive(bool alive)
        {
            this.alive = alive;
        }

        /// <summary>
        /// Gets the entities last facing direction.
        /// </summary>
        /// <returns></returns>
        public char GetLastDirection()
        {
            return lastDirection;
        }

        /// <summary>
        /// Sets the last direction.
        /// </summary>
        /// <param name="c">The direction to face.</param>
        public void SetLastDirection(char c)
        {
            lastDirection = c;
        }

        /// <summary>
        /// Sets a new weap for the entity.
        /// </summary>
        /// <param name="w">The weap to set.</param>
        public abstract void SetNewWeapon(Weapon w);

        #endregion
    }
}
