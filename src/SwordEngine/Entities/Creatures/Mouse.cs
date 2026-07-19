using SkiaSharp;
using SwordEngine.Tiles;
using SwordEngine.Utilities;
using SwordEngine.Gfx;
using SwordEngine.Weapons;

namespace SwordEngine.Entities.Creatures
{
    public class Mouse : Creature
    {
        private Random random;
        private int sleep = 0, direction = 5;

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

        public override void Update()
        {
            GetInput();
            Move();
            handler.GetGameCamera();
        }

        public void GetInput()
        {
            xMove = 0;
            yMove = 0;

            float playerX = handler.GetWorld().GetEntityManager().GetPlayer().GetX();
            float playerY = handler.GetWorld().GetEntityManager().GetPlayer().GetY();

            double distance = MathUtils.CalculateHypotenuse(x - playerX, y - playerY);

            if (distance <= 4 * Tile.TILEWIDTH)
            {
                //System.out.println("noticed");
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

        public override void Render(SKCanvas canvas)
        {
            SKPoint mousePoint = new SKPoint((int)(x - handler.GetGameCamera().GetXOffset())
                    , (int)(y - handler.GetGameCamera().GetYOffset()));
            canvas.DrawBitmap(Assets.mouse, mousePoint, null);
        }


        public override void Die()
        {
            Console.WriteLine("Mouse Slain");
        }

        public override void SetNewWeapon(Weapon w)
        {
            throw new NotImplementedException();
        }
    }
}
