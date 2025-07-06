using SwordEngine.Gfx;

namespace SwordEngine.Tiles
{
    /// <summary>
    /// Cave Mushroom tile.
    /// </summary>
    public class CaveMushTile : Tile
    {
        public CaveMushTile(int id) : base(Assets.cMushroom, id)
        {
        }

        public override bool IsSolid()
        {
            return true;
        }
    }
}
