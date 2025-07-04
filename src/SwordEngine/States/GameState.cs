using SkiaSharp;
using SwordEngine.World;
using System.Drawing;

namespace SwordEngine.States
{
    public class GameState : State
    {
        /// <summary>
        /// The world that the game will use.
        /// </summary>
        private World.World world;

        /// <summary>
        /// Constructs the game object, creates a new world object using
        /// a predetermined file path, and sets it in the handler
        /// </summary>
        /// <param name="handler">The game handler.</param>
        public GameState(Handler handler) : base(handler)
        {
            world = new World.World(handler, "res/worlds/world1.txt");
            handler.setWorld(world);
        }

        /**
         * Calls the world object's update method.
         */
        public override void Update()
        {
            world.Update();
        }

        /**
         * Calls the world object's render method.
         */
        public override void Render(SKCanvas canvas)
        {
            world.Render(canvas);
        }
    }
}
