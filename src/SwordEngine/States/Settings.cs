using SkiaSharp;
using SwordEngine.Utilities;

namespace SwordEngine.States
{
    /// <summary>
    /// The settings state.
    /// </summary>
    public class Settings : State
    {
        /// <summary>
        /// The image to be displayed.
        /// </summary>
        private SKBitmap bitmap;

        /// <summary>
        /// The constructor for the settings object, uses a predetermined file
        /// path to load an image to the img variable
        /// </summary>
        /// <param name="handler">The generic game handler.</param>
        public Settings(Handler handler): base(handler)
        {
            var settingsPath = ResourcePaths.ResolveResourcePath("res", "textures", "testSettings.png");
            SKImage img = SKImage.FromEncodedData(settingsPath);
            bitmap = SKBitmap.FromImage(img);
        }

        /// <summary>
        /// Right now it only checks to see if the pause key has been pressed 
        /// again to switch back to the game state. 
        /// Future plans include putting the mouse controls here
        /// </summary>
        public override void Update()
        {
            if (!(handler.GetKeyManager().pause))
            {
                SetState(handler.GetGame().GetGameState());
            }
        }

        /// <summary>
        /// Renders the image onto the window, since the image has a white
        /// background the window is also colored white to avoid having an
        /// image that doesn't look like it failed to cover the entire window.
        /// </summary>
        /// <param name="canvas">The main display canvas.</param>
        public override void Render(SKCanvas canvas)
        {
            // Draw title screen background.
            canvas.Clear(SKColors.White);

            SKPaint RectPaint = new SKPaint
            {
                Color = SKColors.Blue,
                StrokeWidth = 1,
                IsAntialias = true,
                Style = SKPaintStyle.Stroke
            };

            canvas.DrawRect(0, 0, handler.GetWidth(), handler.GetHeight(), RectPaint);
            SKPoint sKPoint = new SKPoint();
            canvas.DrawBitmap(bitmap, sKPoint);
        }
    }
}
