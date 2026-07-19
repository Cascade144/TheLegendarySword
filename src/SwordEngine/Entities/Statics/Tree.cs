using SkiaSharp;
using SwordEngine.Tiles;
using SwordEngine.Gfx;

namespace SwordEngine.Entities.Statics
{
    public class Tree : StaticEntity
    {
        /// <summary>
        /// Creates a static tree entity at the given x and y position in tixels.
        /// </summary>
        /// <param name="handler"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public Tree(Handler handler, float x, float y) : base(handler, x, y, Tile.TILEWIDTH, Tile.TILEHEIGHT)
        {
        }

        /**
         * Renders this entity on the screen based on where the camera
         * is currently
         * uses the tree asset
         */
        public override void Render(SKCanvas canvas)
        {
            canvas.DrawBitmap(Assets.tree, (int)(x - handler.GetGameCamera().GetXOffset()), (int)(y - handler.GetGameCamera().GetYOffset()), null);
        }


        /**
         * Updates the tree, currently the tree doesn't do anything
         */
        public override void Update()
        {
        }

        public override void Die()
        {
            // TODO Auto-generated method stub
        }
    }
}
