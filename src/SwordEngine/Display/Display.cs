using SkiaSharp;

namespace SwordEngine.Display
{
    public class Display
    {
        /// <summary>
        /// The frame of the window to render.
        /// </summary>
        private SKSurface frame { get; set; }

        /// <summary>
        /// The canvas of the displayed window, inside the borders.
        /// </summary>
        private SKCanvas canvas { get; set; }

        /// <summary>
        /// The hidden height.
        /// </summary>
        private int height;

        /// <summary>
        /// The hidden width.
        /// </summary>
        private int width;

        /// <summary>
        /// The Window's Title Bar.
        /// </summary>
        private String title;

        /// <summary>
        /// The generic Display instantiation method.
        /// </summary>
        /// <param name="width">The desired game width.</param>
        /// <param name="height">The desired game height.</param>
        public Display(int width, int height) 
        {
            this.width = width;
            this.height = height;

            createDisplay();
        }

        /// <summary>
        /// Creates a Window Display in the Operating System utilizing SkiaSharp.
        /// </summary>
        private void createDisplay()
        {
            // First determine Frame size.
            SKImageInfo imageInfo = new SKImageInfo();
            imageInfo.Width = width;
            imageInfo.Height = height;

            frame = SKSurface.Create(imageInfo);

            // Create an empty bitmap to display
            SKBitmap bitmap = new SKBitmap(width, height);

            // Get the canvas now from the newly created frame.
            SKCanvas canvas = frame.Canvas;
            canvas.Clear(SKColors.White);

            // Draw a point and render the bitmap.
            SKPoint point = new SKPoint(0, 0);
            canvas.DrawBitmap(bitmap, point);

            this.canvas = canvas;
        }

        /// <summary>
        /// Returns the SKSurface's Canvas.
        /// </summary>
        public SKCanvas getCanvas()
        {
            return canvas;
        }

        /// <summary>
        /// Returns the SKSurface.
        /// </summary>
        public SKSurface getSurface()
        {
            return frame;
        }
    }
}
