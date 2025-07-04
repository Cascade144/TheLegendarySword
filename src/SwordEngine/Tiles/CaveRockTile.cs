namespace SwordEngine.Tiles
{
    public class CaveRockTile : Tile
    {
        public CaveRockTile(int id) : base(Assets.cRock, id)
        {
        }


        public override bool IsSolid()
        {
            return true;
        }
    }
}
