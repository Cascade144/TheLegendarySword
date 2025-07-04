using SkiaSharp;
using System.Drawing;

namespace SwordEngine.States
{
    /// <summary>
    /// The generic state class.
    /// </summary>
    public abstract class State
    {
        /// <summary>
        /// The current state the game is in.
        /// </summary>
        private static State currentState = null;

        /// <summary>
        /// The Main handler.
        /// </summary>
        protected Handler handler;

        /// <summary>
        /// Constructs the main state object, setting its handler variable.
        /// </summary>
        public State(Handler handler)
        {
            this.handler = handler;
        }

        /// <summary>
        /// Sets the current state that the game is in.
        /// </summary>
        /// <param name="state">The state to set.</param>
        public static void setState(State state)
        {

            currentState = state;
        }

        /// <summary>
        /// Returns the current state the game is in.
        /// </summary>
        public static State getState()
        {

            return currentState;
        }

        /// <summary>
        /// The game has many different states it can be in,
        /// leaving this abstracted allows for each state to update the game as it needs to
        /// </summary>
        public abstract void Update();

        /// <summary>
        /// The game has many different states it can be in,
        /// leaving this abstracted allows for each state to render the game
        /// as it needs to
        /// </summary>
        /// <param name="canvas">The canvas to render.</param>
        public abstract void Render(SKCanvas canvas);
    }
}
