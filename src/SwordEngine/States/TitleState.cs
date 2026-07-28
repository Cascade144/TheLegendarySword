using SkiaSharp;
using SwordEngine.Utilities;
using System.Drawing;

namespace SwordEngine.States
{
    /// <summary>
    /// The title of the game.
    /// </summary>
    public class TitleState : State
    {
        /// <summary>
        /// The buffered image.
        /// </summary>
        public SKImage img;

        /// <summary>
        /// Start rectangle.
        /// </summary>
        Rectangle start = new Rectangle();

        /// <summary>
        /// Cursor rectangle.
        /// </summary>
        Rectangle cursor = new Rectangle();

        /// <summary>
        /// Initializes a new instance of the <see cref="TitleState"/> class.
        /// Constructs a title state object and passes the handler to the main state object.
        /// </summary>
        /// <param name="handler">The game handler.</param>
        public TitleState(Handler handler) : base(handler)
        {
            var titlePath = ResourcePaths.ResolveResourcePath("res", "textures", "titleScreen.png");
            img = SKImage.FromEncodedData(titlePath);

            // Create hitboxes for options.
            start.Width = 150;
            start.Height = 100;
            start.X = 205;
            start.Y = 420;
        }

        /// <summary>
        /// The update method for the title state.
        /// </summary>
        public override void Update()
        {
            if (CheckClick(start))
            {
                SetState(handler.GetGame().GetGameState());
            }
        }

        /// <summary>
        /// Checks if the user clicked.
        /// </summary>
        /// <param name="check">The rectangle to check within.</param>
        public bool CheckClick(Rectangle check)
        {
            Rectangle cursor = new Rectangle();
            cursor.X = handler.GetMouseManager().GetX();
            cursor.Y = handler.GetMouseManager().GetY();
            cursor.Width = 1;
            cursor.Height = 1;
            Console.WriteLine($"Check Click: X: {cursor.X}, Y: {cursor.Y}");
            if (handler.GetMouseManager().left)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// The render method for the title state.
        /// </summary>
        /// <param name="canvas">The current game canvas.</param>
        public override void Render(SKCanvas canvas)
        {
            //Draw title screen background WOW
            canvas.Clear(SKColors.Blue);

            var RectPaint = new SKPaint
            {
                Color = SKColors.Blue,
                StrokeWidth = 1,
                IsAntialias = true,
                Style = SKPaintStyle.Stroke
            };

            canvas.DrawRect(0, 0, handler.GetWidth(), handler.GetHeight(), RectPaint);

            SKPoint sKPoint = new SKPoint();
            canvas.DrawImage(img, sKPoint);
        }
    }
}
