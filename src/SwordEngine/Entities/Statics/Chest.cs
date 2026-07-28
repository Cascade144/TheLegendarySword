using SkiaSharp;
using SwordEngine.Tiles;
using SwordEngine.Gfx;
using SwordEngine.Weapons;

namespace SwordEngine.Entities.Statics
{
    /// <summary>
    /// The class representing a chest entity in the game. A chest is a static entity that can be destroyed to potentially drop a weapon item.
    /// </summary>
    public class Chest : StaticEntity
    {
        /// <summary>
        /// The random number generator used to determine the item dropped by the chest upon destruction.
        /// </summary>
        private Random itemGen = new Random();

        /// <summary>
        /// Constructor for the Chest class. Initializes a new instance of the Chest entity with specified position and size.
        /// </summary>
        /// <param name="handler">The main game handler.</param>
        /// <param name="x">The x position of the chest.</param>
        /// <param name="y">The y position of the chest.</param>
        public Chest(Handler handler, float x, float y) : base(handler, x, y, Tile.TILEWIDTH, Tile.TILEHEIGHT)
        {
            health = 1;
        }

        /// <summary>
        /// Method to update the chest's state. Currently, it does not perform any actions.
        /// </summary>
        public override void Update()
        {
            // TODO Auto-generated method stub
        }

        /// <summary>
        /// Method to render the chest.
        /// </summary>
        /// <param name="canvas">The current game canvas.</param>
        public override void Render(SKCanvas canvas)
        {
            canvas.DrawBitmap(Assets.chest, (int)(x - handler.GetGameCamera().GetXOffset())
                    , (int)(y - handler.GetGameCamera().GetYOffset()), null);
        }

        /// <summary>
        /// Method to handle the death of the chest.
        /// </summary>
        public override void Die()
        {
            int random = itemGen.Next(101);
            if (random <= 45) 
            {
                handler.GetWorld().GetEntityManager().addWeapon(new LongSword(handler, Tile.TILEWIDTH, Tile.TILEHEIGHT, x, y));
            }
            else if (random <= 76 && random >= 46)
                handler.GetWorld().GetEntityManager().addWeapon(new Spear(handler, Tile.TILEWIDTH, Tile.TILEHEIGHT, x, y));
            else if (random <= 89 && random >= 77)
                handler.GetWorld().GetEntityManager().addWeapon(new BallNChain(handler, Tile.TILEWIDTH, Tile.TILEHEIGHT, x, y));
            else if (random <= 99 && random >= 90)
                handler.GetWorld().GetEntityManager().addWeapon(new Axe(handler, Tile.TILEWIDTH, Tile.TILEHEIGHT, x, y));
            else
            {
                Console.WriteLine("The chest was empty!");
            }
        }

        /// <summary>
        /// Method that is called when the chest is hurt.
        /// It sets the alive status to false and calls the Die method to handle the chest's death.
        /// Used for destroying the chest and potentially dropping an item.
        /// </summary>
        public override void Hurt()
        {
            alive = false;
            Die();
        }
    }
}
