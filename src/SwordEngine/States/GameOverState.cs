using SkiaSharp;
using SwordEngine.Utilities;

namespace SwordEngine.States
{
    /// <summary>
    /// The game over state of the main game.
    /// </summary>
    public class GameOverState: State
    {
        /// <summary>
        /// The game over bitmap.
        /// </summary>
        public SKBitmap bitmap;

        /// <summary>
        /// The instantiation method to render a game over state screen.
        /// </summary>
        /// <param name="handler"></param>
        public GameOverState(Handler handler) : base(handler)
        {
            var losePath = ResourcePaths.ResolveResourcePath("res", "textures", "loseScreen.png");
            SKImage img = SKImage.FromEncodedData(losePath);
            bitmap = SKBitmap.FromImage(img);
        }

        /// <summary>
        /// Method to update the game over state.
        /// Currently, it does not perform any actions.
        /// </summary>
        public override void Update()
        {
            // TODO Auto-generated method stub
        }

        /// <summary>
        /// Render the game over state on the provided canvas.
        /// </summary>
        /// <param name="canvas">The canvas the game is currently using.</param>
        public override void Render(SKCanvas canvas)
        {
            canvas.Clear(SKColors.Red);

            SKPaint RectPaint = new SKPaint
            {
                Color = SKColors.Red,
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
