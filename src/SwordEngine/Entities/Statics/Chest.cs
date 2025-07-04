using SkiaSharp;
using SwordEngine.Tiles;
using SwordEngine.Gfx;
using SwordEngine.Weapons;

namespace SwordEngine.Entities.Statics
{
    public class Chest : StaticEntity
    {
        private Random itemGen = new Random();

        public Chest(Handler handler, float x, float y) : base(handler, x, y, Tile.TILEWIDTH, Tile.TILEHEIGHT)
        {
            health = 1;
        }

        public override void Update()
        {
            // TODO Auto-generated method stub
        }

        public override void Render(SKCanvas canvas)
        {
            canvas.DrawBitmap(Assets.chest, (int)(x - handler.GetGameCamera().GetXOffset())
                    , (int)(y - handler.GetGameCamera().GetYOffset()), null);
        }

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
        
        public override void Hurt()
        {
            alive = false;
            Die();
        }
    }
}
