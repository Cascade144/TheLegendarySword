namespace SwordEngine.Tiles
{
    public class CaveSlimeTile : Tile
    {
        public CaveSlimeTile(int id) : base(Assets.cSlime, id)
        {
        }

        public override bool IsSolid()
        {
            return true;
        }
    }
}
