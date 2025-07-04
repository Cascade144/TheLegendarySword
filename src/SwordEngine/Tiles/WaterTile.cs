namespace SwordEngine.Tiles
{
    /// <summary>
    /// 
    /// </summary>
    public class WaterTile : Tile
    {
        public WaterTile(int id) : base(Assets.water, id)
        {
        }

        public override bool IsSolid()
        {
            return true;
        }
    }
}
