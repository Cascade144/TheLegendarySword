using SwordEngine.Entities;

namespace SwordEngine.Gfx
{
    /// <summary>
    /// The main game engine camera.
    /// </summary>
    public class GameCamera
    {
        /// <summary>
        /// The camera's x/y displacement in tixels.
        /// </summary>
        private float xOffset, yOffset;

        /// <summary>
        /// The main handler
        /// </summary>
        private Handler handler;

        /// <summary>
        /// Constructs a GameCamera object initializing its offsets (initial position) 
        /// to the given variables.
        /// </summary>
        /// <param name="handler">The main handler object.</param>
        /// <param name="xOffset">The starting x position in tixels.</param>
        /// <param name="yOffset">The starting y position in tixels.</param>
        public GameCamera(Handler handler, float xOffset, float yOffset)
        {
            this.handler = handler;
            this.xOffset = xOffset;
            this.yOffset = yOffset;
        }

        /// <summary>
        /// Checks for empty map space (beyond bounds) and prevents camera from moving past that 
        /// by resetting its offset variables to 0
        /// </summary>
        public void checkBlankSpace()
        {
            if (xOffset < 0)
            {
                xOffset = 0;
            }
            else if (xOffset > handler.getWorld().getWidth() * Tile.Tile.TILEWIDTH - handler.getWidth())
            {
                xOffset = handler.getWorld().getWidth() * Tile.Tile.TILEWIDTH - handler.getWidth();
            }

            if (yOffset < 0)
            {
                yOffset = 0;
            }
            else if (yOffset > handler.getWorld().getHeight() * Tile.Tile.TILEHEIGHT - handler.getHeight())
            {
                yOffset = handler.getWorld().getHeight() * Tile.Tile.TILEHEIGHT - handler.getHeight();
            }
        }

        /// <summary>
        /// Moves the camera so that the window is always centered on the given entity e
        /// </summary>
        /// <param name="e">The entity to center on.</param>
        public void centerOnEntity(Entity e)
        {
            xOffset = e.getX() - handler.getWidth() / 2 + e.getWidth();
            yOffset = e.getY() - handler.getHeight() / 2 + e.getHeight();
            checkBlankSpace();
        }


        /// <summary>
        /// Moves camera by x and y parameters in pixels 
        /// </summary>
        /// <param name="xAmt">The x amount of pixels.</param>
        /// <param name="yAmt">The y amount of pixels.</param>
        public void Move(float xAmt, float yAmt)
        {
            yOffset += yAmt;
            xOffset += xAmt;
            checkBlankSpace();
        }

        #region Getters and Setters

        /// <summary>
        /// Returns the xOffset in tixels
        /// </summary>
        public float getxOffset()
        {
            return xOffset;
        }
        

        /// <summary>
        /// Sets the xOffset value to the given tixel amount
        /// and checks for blank space.
        /// </summary>
        /// <param name="xOffset">The new offset in tixels.</param>
        public void setxOffset(float xOffset)
        {
            this.xOffset = xOffset;
            checkBlankSpace();
        }

        /// <summary>
        /// Returns the yOffset in tixels.
        /// </summary>
        /// <returns>The new offset in tixels.</returns>
        public float getyOffset()
        {
            return yOffset;
        }

        /// <summary>
        /// Sets the yOffset value to the given tixel amount
        /// </summary>
        /// <param name="yOffset">The offset in tixels to set.</param>
        public void setyOffset(float yOffset)
        {
            this.yOffset = yOffset;
            checkBlankSpace();
        }
    }
}
