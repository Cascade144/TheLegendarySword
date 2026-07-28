using SkiaSharp;
using SwordEngine.States;
using SwordEngine.Weapons;
using SwordEngine.Gfx;
using System.Drawing;

namespace SwordEngine.Entities.Creatures
{
    /// <summary>
    /// Class for the main player character. Inherits from Creature class.
    /// </summary>
    public class Player : Creature
    {
        private int iFrames = 0;
        private Weapon defaultDagger;
        private Weapon currWeap;

        /// <summary>
        ///  Creates the player creature and sets its position
        ///  based on the passed parameters.
        ///  Also sets up the character's hitbox.
        /// </summary>
        /// <param name="handler">The main handler.</param>
        /// <param name="x">This entity's x position in tixels.</param>
        /// <param name="y">This entity's y position in tixels.</param>
        public Player(Handler handler, float x, float y) : base(handler, x, y, Creature.DEFAULT_CREATURE_WIDTH, DEFAULT_CREATURE_HEIGHT)
        {
            bounds.X = 10;
            bounds.X = 10;
            bounds.Width = 20;
            bounds.Height = 20;

            isPlayer = true;

            currWeap = new Dagger(handler, 20, 20);
            defaultDagger = currWeap;
        }

        /// <summary>
        /// Updates this entity by calling the getInput and move functions.
        /// Also tells the camera object to center itself on this entity.
        /// </summary>
        public override void Update()
        {
            GetInput();
            Move();

            handler.GetGameCamera().CenterOnEntity(this);

            if (currWeap.GetCoolDown() == -1)
            {
                if (handler.GetKeyManager().space)
                {
                    Attk();
                }
            }
            else
            {
                currWeap.OnCoolDown();
            }

            currWeap.Update(this);
        }

        /// <summary>
        /// Uses the keyManager to determine where the user wants this 
        /// entity to move and sets the corresponding variables.
        /// </summary>
        public void GetInput()
        {
            xMove = 0;
            yMove = 0;

            if (handler.GetKeyManager().up)
            {
                yMove = -speed;
                SetLastDirection('u');
            }
            if (handler.GetKeyManager().down)
            {
                yMove = speed;
                SetLastDirection('d');
            }
            if (handler.GetKeyManager().left)
            {
                xMove = -speed;
                SetLastDirection('l');
            }
            if (handler.GetKeyManager().right)
            {
                xMove = speed;
                SetLastDirection('r');
            }

        }

        /// <summary>
        /// Player Attack method.
        /// </summary>
        public void Attk()
        {
            currWeap.Attk(this);
        }

        /// <summary>
        /// Render method.
        /// </summary>
        /// <param name="canvas">The main window canvas.</param>
        public override void Render(SKCanvas canvas)
        {

            if ((iFrames % 2) == 0)
            {
                int xPos = (int)(x - handler.GetGameCamera().GetXOffset());
                int yPos = (int)(y - handler.GetGameCamera().GetYOffset());

                SKRect playerBoundingRect = SKRect.Create(xPos, yPos, bounds.Width, bounds.Height);
                canvas.DrawBitmap(Assets.player, playerBoundingRect, null);
            }
            else
            {
                iFrames++;
            }

            currWeap.Render(canvas);

            // Draws a border box and health box
            SKPaint blackBackground = new SKPaint
            {
                Color = SKColors.Black,
            };
            canvas.DrawRect((handler.GetWidth() - 255), (handler.GetHeight() - 75), (5 * DEFAULT_HEALTH) + 10, 40, blackBackground);
            SKPaint redBackground = new SKPaint
            {
                Color = SKColors.Red,
            };
            canvas.DrawRect((int)(handler.GetWidth() - 250), (int)(handler.GetHeight() - 70), (5 * this.health), 30, redBackground);
        }

        /// <summary>
        /// Method called when the player dies.
        /// </summary>
        public override void Die()
        {
            State.SetState(handler.GetGame().GetGameOverState());
        }

        /// <summary>
        /// Method called when the player is hurt.
        /// </summary>
        public override void Hurt()
        {
            if (iFrames < 15)
            {
                iFrames++;
            }
            else
            {
                // System.out.println("You took 1 damage.");
                health--;
                if (health <= 0)
                {
                    alive = false;
                    Console.WriteLine("You Died.");
                    Die();
                }
                iFrames = 0;
            }
        }

        /// <summary>
        /// Gets the current weapon.
        /// </summary>
        public Weapon GetCurrWeapon()
        {
            return currWeap;
        }

        /// <summary>
        /// Overrides the basic SetWeapon.
        /// </summary>
        /// <param name="w"></param>
        public override void SetNewWeapon(Weapon w)
        {
            currWeap = w;
        }

        public void ResetCurrWeap()
        {
            currWeap = defaultDagger;
        }
    }
}
