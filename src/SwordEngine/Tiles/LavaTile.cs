using SwordEngine.Gfx;

namespace SwordEngine.Tiles
{
    public class LavaTile : Tile
    {
        public LavaTile(int id) : base(Assets.lava, id)
        {

        }

        /// <inheritdoc>
        public override bool IsSolid()
        {
            return true;
        }
    }
}
