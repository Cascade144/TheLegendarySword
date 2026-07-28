using SkiaSharp;
using SwordEngine.Gfx;
using SwordEngine.Tiles;
using SwordEngine.Utilities;
using SwordEngine.Weapons;

namespace SwordEngine.Entities.Creatures
{
    /// <summary>
    /// Class for the snake creature, which extends the Creature class.
    /// This class handles the snake's behavior, including movement, rendering, and interactions with the player.
    /// </summary>
    public class Snake : Creature
    {
        private Random random;
        private int sleep = 0, direction = 5;

        /// <summary>
        /// Constructs the snake creature with the specified handler and position.
        /// </summary>
        /// <param name="handler">The main game handler.</param>
        /// <param name="x">The x spawn width.</param>
        /// <param name="y">The y spawn height.</param>
        public Snake(Handler handler, float x, float y) : base(handler, x, y, DEFAULT_CREATURE_WIDTH, DEFAULT_CREATURE_HEIGHT)
        {
            bounds.X = 8;
            bounds.Y = 15;
            bounds.Width = 25;
            bounds.Height = 20;
            random = new Random();
            health = 15;
            this.speed = 3.0f; //20.0f;
        }

        /// <summary>
        /// Method to update the snake's state, including getting input, moving, and updating the game camera.
        /// </summary>
        public override void Update()
        {
            GetInput();
            Move();

            handler.GetGameCamera();
        }

        /// <summary>
        /// Method to determine snake movement.
        /// </summary>
        public void GetInput()
        {

            xMove = 0;
            yMove = 0;

            float playerX = handler.GetWorld().GetEntityManager().GetPlayer().GetX();
            float playerY = handler.GetWorld().GetEntityManager().GetPlayer().GetY();

            double distance = MathUtils.CalculateHypotenuse(x - playerX, y - playerY);

            if (distance <= 2 * Tile.TILEWIDTH)
            {
                //System.out.println("noticed");
                if (sleep < 8)
                {
                    sleep++;
                }
                else
                {
                    if (sleep < 16)
                    {
                        sleep++;
                        if (x < playerX)
                            xMove = speed;
                        else if (x > playerX)
                            xMove = -speed;

                        if (y < playerY)
                            yMove = speed;
                        else if (y > playerY)
                            yMove = -speed;
                    }
                    else
                    {
                        sleep = 0;
                    }
                }
            }
            else
            {
                if (sleep < 8)
                {
                    sleep++;
                    direction = random.Next(4);
                }
                else
                {
                    if (sleep < 16)
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
        /// Method to render the snake.
        /// </summary>
        /// <param name="canvas">The main game canvas.</param>
        public override void Render(SKCanvas canvas)
        {
            int posX = (int)(x - handler.GetGameCamera().GetXOffset());
            int posY = (int)(y - handler.GetGameCamera().GetYOffset());

            SKPoint sKPoint = new SKPoint(posX, posY);

            canvas.DrawBitmap(Assets.snake, sKPoint, null);
        }

        /// <summary>
        /// Method to handle the snake's death, printing a message to the console.
        /// </summary>
        public override void Die()
        {
            Console.WriteLine("Snake Slain");
        }

        /// <summary>
        /// Method to set a new weapon for the danger noodle.
        /// This method is not implemented and will throw a NotImplementedException if called.
        /// </summary>
        /// <param name="w">Weapon</param>
        /// <exception cref="NotImplementedException"></exception>
        public override void SetNewWeapon(Weapon w)
        {
            throw new NotImplementedException();
        }
    }
}
