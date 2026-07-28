using SkiaSharp;
using SwordEngine.Gfx;
using SwordEngine.Tiles;
using SwordEngine.Utilities;
using SwordEngine.Weapons;
using System.Drawing;

namespace SwordEngine.Entities.Creatures
{
    /// <summary>
    /// Method to create a slime creature that moves around the map and attacks the player when in range.
    /// </summary>
    public class Slime : Creature
    {
        /// <summary>
        /// The Random variable that determines if the slime will move around.
        /// </summary>
        private Random random;

        private int sleep = 0, direction = 5;

        /// <summary>
        /// This constructor creates a slime creature at the given 
        /// x and y position in tixels, and sets its hitbox and speed
        /// </summary>
        /// <param name="handler"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public Slime(Handler handler, float x, float y) : base(handler, x, y, DEFAULT_CREATURE_WIDTH, DEFAULT_CREATURE_HEIGHT)
        {
            bounds.X = 8;
            bounds.Y = 15;
            bounds.Width = 25;
            bounds.Height = 20;
            random = new Random();
            health = 10;
            this.speed = 2.0f; //20.0f;
        }

        /// <summary>
        /// Updates the slime by calling the getInput() function
        /// and moves it around the map accordingly.
        /// </summary>
        public override void Update()
        {
            GetInput();
            Move();

            handler.GetGameCamera();
        }

        /// <summary>
        /// Sets the random variable and determines what direction the slime will move.
        /// </summary>
        public void GetInput()
        {

            xMove = 0;
            yMove = 0;

            float playerX = handler.GetWorld().GetEntityManager().GetPlayer().GetX();
            float playerY = handler.GetWorld().GetEntityManager().GetPlayer().GetY();

            double distance = MathUtils.CalculateHypotenuse(x - playerX, y - playerY);

            if (distance <= 4 * Tile.TILEWIDTH)
            {
                if (sleep < 15)
                {
                    sleep++;
                }
                else
                {
                    if (sleep < 30)
                    {
                        sleep++;
                        if (x < playerX)
                        {
                            xMove = speed;
                        }
                        else if (x > playerX)
                        {
                            xMove = -speed;
                        }

                        if (y < playerY)
                        {
                            yMove = speed;
                        }
                        else if (y > playerY)
                        {
                            yMove = -speed;
                        }
                    }
                    else
                    {
                        sleep = 0;
                    }
                }
            }
            else
            {
                if (sleep < 15)
                {
                    sleep++;
                    direction = random.Next(4);
                }
                else
                {
                    if (sleep < 30)
                    {
                        sleep++;
                        if (direction == 0)
                            xMove = -speed;
                        else if (direction == 1)
                            xMove = speed;
                        else if (direction == 2)
                            yMove = -speed;
                        else if (direction == 3)
                            yMove = speed;
                        else
                            direction = random.Next(4);
                    }
                    else
                    {
                        sleep = 0;
                    }
                }
            }
        }

        /// <summary>
        ///  Renders the slime onto the screen uses the gameCamera's position to 
        ///  determine where to draw the creature.
        /// </summary>
        /// <param name="g"></param>
        public override void Render(SKCanvas canvas)
        {

            int posX = (int)(x - handler.GetGameCamera().GetXOffset());
            int posY = (int)(y - handler.GetGameCamera().GetYOffset());

            SKPoint sKPoint = new SKPoint(posX, posY);
            canvas.DrawBitmap(Assets.slime, sKPoint, null);

            // draws a bounding box
            //g.setColor(Color.red);
            //g.fillRect((int) (x + bounds.x - handler.getGameCamera().getxOffset())
            //		, (int) (y + bounds.y - handler.getGameCamera().getyOffset()), bounds.width, bounds.height);
        }

        /// <summary>
        /// Method to handle the death of the slime creature.
        /// </summary>
        public override void Die()
        {
            Console.WriteLine("Slime Slain");
        }

        /// <summary>
        /// Method to give the slime a new weapon. Not implemented yet.
        /// </summary>
        /// <param name="w">The weapon to give.</param>
        /// <exception cref="NotImplementedException"></exception>
        public override void SetNewWeapon(Weapon w)
        {
            throw new NotImplementedException();
        }
    }
}
