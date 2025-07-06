using SwordEngine.Gfx;

namespace SwordEngine.Tiles
{
    /// <summary>
    /// The brick tile.
    /// </summary>
    public class BrickTile : Tile
    {
        public BrickTile(int id) : base(Assets.stone, id)
        {
        }

        /// <summary>
        /// Whether the brick tile is solid.
        /// </summary>
        public override bool IsSolid()
        {
            return true;
        }
    }
}
