using SwordEngine.Gfx;

namespace SwordEngine.Tiles
{
    /// <summary>
    /// 
    /// </summary>
    public class RockTile : Tile
    {
        public RockTile(int id) : base(Assets.rock, id)
        {
        }

        public override bool IsSolid()
        {
            return true;
        }
    }
}
