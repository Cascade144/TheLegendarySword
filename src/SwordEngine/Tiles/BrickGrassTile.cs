
namespace SwordEngine.Tiles
{
    public class BrickGrassTile : Tile
    {
        public BrickGrassTile(int id) : base(Assets.stonegrass, id)
        {
        }

        public override bool IsSolid()
        {
            return true;
        }
    }
}
