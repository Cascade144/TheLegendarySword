namespace SwordEngine.Tiles
{
    public class CaveWallTile : Tile
    {
        public CaveWallTile(int id) : base(Assets.cWall, id)
        {
        }

        public override bool IsSolid()
        {
            return true;
        }
    }
}
