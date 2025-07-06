using SkiaSharp;

namespace SwordEngine.Gfx
{
    public class SpriteSheet
    {
        /// <summary>
        /// The sprite sheet being cropped.
        /// </summary>
        private SKBitmap sheet;

        /// <summary>
        /// Takes in a BufferedImage and sets this class's sheet variable to it.
        /// </summary>
        /// <param name="sheet">Image being cropped.</param>
        public SpriteSheet(SKBitmap sheet)
        {
            this.sheet = sheet;
        }

        /// <summary>
        /// Takes in various coordinates and crops out the image 
        /// at that location and returns it.
        /// </summary>
        /// <param name="x">The x position of the sprite in pixels.</param>
        /// <param name="y">The y position of the sprite in pixels.</param>
        /// <param name="width">The width of the sprite in pixels.</param>
        /// <param name="height">The height of the sprite in pixels.</param>
        /// <returns>The cropped out sprite</returns>
        public SKBitmap Crop(int x, int y, int width, int height)
        {
            SKRectI cropArea = SKRectI.Create(x, y, width, height);
            SKBitmap croppedBitMap = new SKBitmap();
            bool success = sheet.ExtractSubset(croppedBitMap, cropArea);
            if (success)
            {
                return croppedBitMap;
            }
            else
            {
                throw new InvalidOperationException("Unable to crop image.");
            }
        }
    }
}
