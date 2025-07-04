using SkiaSharp;
using System.Drawing;
using System.Numerics;
using System.Runtime.Versioning;

namespace SwordEngine.Display
{
    public class Display
    {
        private SKSurface frame { get; set; }

        private SKCanvas canvas { get; set; }

        private int height;

        private int width;

        /// <summary>
        /// The Window's Title Bar.
        /// </summary>
        private String title;

        public Display(string title, int width, int height) 
        {
            this.title = title;
            this.width = width;
            this.height = height;

            createDisplay();
        }

        private void createDisplay()
        {
            // First determine Frame size.
            SKImageInfo imageInfo = new SKImageInfo();
            imageInfo.Width = width;
            imageInfo.Height = height;

            this.frame = SKSurface.Create(imageInfo);

            // Create an empty bitmap to display
            SKBitmap bitmap = new SKBitmap(width, height);

            // Get the canvas now from the newly created frame.
            SKCanvas canvas = this.frame.Canvas;
            canvas.Clear(SKColors.White);

            this.canvas = canvas;
        }

        /// <summary>
        /// Returns the SKSurface's Canvas.
        /// </summary>
        public SKCanvas getCanvas()
        {
            return this.canvas;
        }


        /// <summary>
        /// Returns the SKSurface.
        /// </summary>
        public SKSurface getSurface()
        {
            return this.frame;
        }
    }
}
