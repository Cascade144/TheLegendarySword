using SkiaSharp;
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
        /// Constructs a title state object and passes the handler to the main state object.
        /// </summary>
        /// <param name="handler"></param>
        public TitleState(Handler handler) : base(handler)
        {
            img = SKImage.FromEncodedData("/textures/titleScreen.png");

            // Create hitboxes for options.
            start.width = 150;
            start.height = 100;
            start.x = 205;
            start.y = 420;
        }

        /**
         * The update method, currently does nothing
         */
        public override void Update()
        {
            if (CheckClick(start))
            {
                State.setState(handler.getGame().getGameState());
            }
        }

        /// <summary>
        /// Checks if the user clicked.
        /// </summary>
        /// <param name="check">The rectangle to check within.</param>
        public bool CheckClick(Rectangle check)
        {
            Rectangle cursor = new Rectangle();
            cursor.x = handler.getMouseManager().getX();
            cursor.y = handler.getMouseManager().getY();
            cursor.width = 1;
            cursor.height = 1;
            if (cursor.intersects(check) && handler.getMouseManager().left)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /**
         * The render method, currently does nothing
         */
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

            canvas.DrawRect(0, 0, handler.getWidth(), handler.getHeight(), RectPaint);

            SKPoint sKPoint = new SKPoint();
            canvas.DrawImage(img, sKPoint);
        }
    }
}
