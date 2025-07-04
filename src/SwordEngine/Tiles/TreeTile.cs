namespace SwordEngine.Tiles
{
    public class TreeTile : Tile
    {
        public TreeTile(int id) : base(Assets.tree, id)
        {
        }

        public override bool IsSolid()
        {
            return true;
        }
    }
}
