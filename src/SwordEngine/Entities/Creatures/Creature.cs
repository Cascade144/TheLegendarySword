using SwordEngine.Tiles;
using SwordEngine.Weapons;

namespace SwordEngine.Entities.Creatures
{
    /// <summary>
    /// The creature class for NPC enemies for the player.
    /// It sets each creature's health, speed, width and height. It also prevents 
    /// each creature from moving out of bounds by checking the solidity
    /// </summary>
    public abstract class Creature : Entity
    {
        /// <summary>
        /// Default speed.
        /// </summary>
        public static float DEFAULT_SPEED = 3.0f; //6.0f;

        /// <summary>
        /// Default size in width and height in tixels.
        /// </summary>
        public static int DEFAULT_CREATURE_WIDTH = 40,
                          DEFAULT_CREATURE_HEIGHT = 40;

        /// <summary>
        /// The speed at which the creature move on the overworld.
        /// </summary>
        protected float speed;

        /// <summary>
        /// The current size in tixels a creature is moving.
        /// </summary>
        protected float xMove, yMove;

        /// <summary>
        /// Constructs a creature object setting its health 
        /// speed, direction of movement, size and is extended by
        /// any other class to create a unique creature.
        /// </summary>
        /// <param name="handler">The main handler object.</param>
        /// <param name="x">This creature's x position in tixels.</param>
        /// <param name="y">This creature's y position in tixels.</param>
        /// <param name="width">The width of the creature.</param>
        /// <param name="height">The height of the creature.</param>
        public Creature(Handler handler, float x, float y, int width, int height) : base(handler, x, y, width, height)
        {
            speed = DEFAULT_SPEED;
            xMove = 0;
            yMove = 0;
        }

        /// <summary>
        /// Checks for collision before allowing the creature to move
        /// left, right, up, down.If there are none, it calls for moveX 
        /// or moveY.
        /// </summary>
        public void Move()
        {
            if (!checkEntityCollisions(xMove, 0f))
                MoveX();
            if (!checkEntityCollisions(0f, yMove))
                MoveY();
        }

        /// <summary>
        /// Allows the creature to move left or right depending on the xMove value.
        /// </summary>
        public void MoveX()
        {
            if (xMove > 0)
            { // moving right
                int tx = (int)(x + xMove + bounds.X + bounds.Width) / Tile.TILEWIDTH;

                if (!CollisionWithTile(tx, (int)(y + bounds.Y) / Tile.TILEHEIGHT) &&
                        !CollisionWithTile(tx, (int)(y + bounds.Y + bounds.Height) / Tile.TILEHEIGHT))
                {
                    x += xMove;
                }
                else
                {
                    x = tx * Tile.TILEWIDTH - bounds.X - bounds.Width - 1;
                }

            }
            else if (xMove < 0)
            { // moving left
                int tx = (int)(x + xMove + bounds.X) / Tile.TILEWIDTH;

                if (!CollisionWithTile(tx, (int)(y + bounds.Y) / Tile.TILEHEIGHT) &&
                        !CollisionWithTile(tx, (int)(y + bounds.Y + bounds.Height) / Tile.TILEHEIGHT))
                {
                    x += xMove;
                }
                else
                {
                    x = tx * Tile.TILEWIDTH + Tile.TILEWIDTH - bounds.X;
                }
            }

        }

        /// <summary>
        /// Allows the creature to move up or down depending on the yMove value.
        /// </summary>
        public void MoveY()
        {
            if (yMove < 0)
            { // moving up
                int ty = (int)(y + yMove + bounds.Y) / Tile.TILEHEIGHT;

                if (!CollisionWithTile((int)(x + bounds.X) / Tile.TILEWIDTH, ty) &&
                        !CollisionWithTile((int)(x + bounds.X + bounds.Width) / Tile.TILEWIDTH, ty))
                {
                    y += yMove;
                }
                else
                {
                    y = ty * Tile.TILEHEIGHT + Tile.TILEHEIGHT - bounds.Y;
                }
            }
            else if (yMove > 0)
            { // moving down
                int ty = (int)(y + yMove + bounds.Y + bounds.Height) / Tile.TILEHEIGHT;

                if (!CollisionWithTile((int)(x + bounds.X) / Tile.TILEWIDTH, ty) &&
                        !CollisionWithTile((int)(x + bounds.X + bounds.Width) / Tile.TILEWIDTH, ty))
                {
                    y += yMove;
                }
                else
                {
                    y = ty * Tile.TILEHEIGHT - bounds.Y - bounds.Height - 1;
                }
            }
        }

        /// <summary>
        /// Checks for collision with solid tiles by finding the tile at the passed in coordinates.
        /// Passed in x and y are coordinates with 1 unit of space being the width of a tile.
        /// </summary>
        /// <param name="x">The x coordinate in tixels.</param>
        /// <param name="y">The y coordinate in tixels.</param>
        /// <returns>Returns true if the tixel coordinates are in a solid tile or false.</returns>
        protected bool CollisionWithTile(int x, int y)
        {
            return handler.GetWorld().GetTile(x, y).IsSolid();
        }

        #region Getters and Setters

        /// <summary>
        /// Returns the x tixel count for the creatures movement.
        /// </summary>
        public float GetXMove()
        {
            return xMove;
        }

        /// <summary>
        /// Sets the tixel value for xMove.
        /// </summary>
        /// <param name="xMove">New XTixel value.</param>
        public void SetXMove(float xMove)
        {
            this.xMove = xMove;
        }

        /// <summary>
        /// Returns the y tixel count for the creatures movement.
        /// </summary>
        public float GetYMove()
        {
            return yMove;
        }


        /// <summary>
        /// Sets the tixel value for yMove.
        /// </summary>
        /// <param name="yMove">New YTixel value.</param>
        public void SetYMove(float yMove)
        {
            this.yMove = yMove;
        }

        /// <summary>
        /// Returns the current health of the creature.
        /// </summary>
        public override int GetHealth()
        {
            return health;
        }
        
        /// <summary>
        /// Sets the creature's health.
        /// </summary>
        /// <param name="health">The new health to set.</param>
        public override void SetHealth(int health)
        {
            this.health = health;
        }

        /// <summary>
        /// Returns the current speed of the creature.
        /// </summary>
        public float GetSpeed()
        {
            return speed;
        }

        /// <summary>
        /// Sets the speed of the creature 
        /// </summary>
        /// <param name="speed">The creats speed.</param>
        public void SetSpeed(float speed)
        {
            this.speed = speed;
        }

        public abstract void SetNewWeapon(Weapon w);

        #endregion
    }
}
