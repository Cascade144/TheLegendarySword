using SkiaSharp;
using SwordEngine.Tiles;
using SwordEngine.Utilities;
using SwordEngine.Gfx;
using SwordEngine.Weapons;

namespace SwordEngine.Entities.Creatures
{
    /// <summary>
    /// The Mouse class represents a small creature in the game that can move around and interact with the player.
    /// It inherits from the Creature class and has its own unique behavior, including random movement and chasing the player when they are nearby.
    /// </summary>
    public class Mouse : Creature
    {
        private Random random;
        private int sleep = 0, direction = 5;

        /// <summary>
        /// Constructor for the mouse object.
        /// </summary>
        /// <param name="handler">The game handler.</param>
        /// <param name="x">The x spawn width.</param>
        /// <param name="y">The y spawn height.</param>
        public Mouse(Handler handler, float x, float y) : base(handler, x, y, DEFAULT_CREATURE_WIDTH, DEFAULT_CREATURE_HEIGHT)
        {
            bounds.X = 8;
            bounds.Y = 15;
            bounds.Width = 25;
            bounds.Height = 20;
            random = new Random();
            health = 15;
            speed = 3.0f; //20.0f;
        }

        /// <summary>
        /// Method to update the mouse's state, including getting input for movement and updating the game camera.
        /// </summary>
        public override void Update()
        {
            GetInput();
            Move();
            handler.GetGameCamera();
        }

        /// <summary>
        /// Method to get the input for mouse movement.
        /// AI logic for the mouse to follow the player.
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
                        {
                            xMove = -speed;
                        }
                        else if (direction == 1)
                        {
                            xMove = speed;
                        }
                        else if (direction == 2)
                        {
                            yMove = -speed;
                        }
                        else if (direction == 3)
                        {
                            yMove = speed;
                        }
                        else
                        {
                            direction = random.Next(4);
                        }
                    }
                    else
                    {
                        sleep = 0;
                    }
                }
            }
        }

        /// <summary>
        /// Method to render the mouse on canvas.
        /// </summary>
        /// <param name="canvas">The current game canvas.</param>
        public override void Render(SKCanvas canvas)
        {
            SKPoint mousePoint = new SKPoint((int)(x - handler.GetGameCamera().GetXOffset())
                    , (int)(y - handler.GetGameCamera().GetYOffset()));
            canvas.DrawBitmap(Assets.mouse, mousePoint, null);
        }

        /// <summary>
        /// Method to handle the mouse's death, printing a message to the console.
        /// </summary>
        public override void Die()
        {
            Console.WriteLine("Mouse Slain");
        }

        /// <summary>
        /// Method to set a new weapon for the mouse.
        /// This method is not implemented and will throw a NotImplementedException if called.
        /// </summary>
        /// <param name="w">Weapon to be given to the mouse.</param>
        /// <exception cref="NotImplementedException"></exception>
        public override void SetNewWeapon(Weapon w)
        {
            throw new NotImplementedException();
        }
    }
}
