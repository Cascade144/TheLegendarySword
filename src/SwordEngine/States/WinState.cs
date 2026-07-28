using SkiaSharp;
using SwordEngine.Utilities;

namespace SwordEngine.States
{
    /// <summary>
    /// The WinState of the game and main display.
    /// </summary>
    public class WinState : State
    {
        /// <summary>
        /// The win state bitmap.
        /// </summary>
        public SKBitmap bitmap;

        /// <summary>
        /// The instantiation method to render a Win state screen.
        /// </summary>
        /// <param name="handler"></param>
        public WinState(Handler handler) : base(handler)
        {
            var winPath = ResourcePaths.ResolveResourcePath("res", "textures", "winScreen.png");
            SKImage? img = SKImage.FromEncodedData(winPath);

            if (img == null)
            {
                throw new FileNotFoundException($"Could not load win screen image from: {winPath}");
            }

            bitmap = SKBitmap.FromImage(img);
        }

        /// <summary>
        /// Updates the state.
        /// </summary>
        public override void Update()
        {
            // TODO Auto-generated method stub
        }

        /// <summary>
        /// Overrides the main render method.
        /// </summary>
        public override void Render(SKCanvas canvas)
        {
            // Draw win screen background.
            canvas.Clear(SKColors.Black);

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
