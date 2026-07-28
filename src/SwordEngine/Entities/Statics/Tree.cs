using SkiaSharp;
using SwordEngine.Tiles;
using SwordEngine.Gfx;

namespace SwordEngine.Entities.Statics
{
    /// <summary>
    /// Method for creating a static tree entity in the game world.
    /// This class inherits from StaticEntity and represents a tree that does not move or interact with other entities.
    /// It is rendered using the tree asset and has a fixed position in the game world.
    /// </summary>
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

        /// <summary>
        /// Renders this entity on the screen based on where the camera is currently.
        /// </summary>
        /// <param name="canvas">The current game canvas.</param>
        public override void Render(SKCanvas canvas)
        {
            canvas.DrawBitmap(Assets.tree, (int)(x - handler.GetGameCamera().GetXOffset()), (int)(y - handler.GetGameCamera().GetYOffset()), null);
        }

        /// <summary>
        /// Updates the tree, currently the tree doesn't do anything.
        /// </summary>
        public override void Update()
        {
        }

        /// <summary>
        /// Method to handle the death of the tree entity. Currently, this method is not implemented as trees do not die in the game.
        /// </summary>
        public override void Die()
        {
            // TODO Auto-generated method stub
        }
    }
}
