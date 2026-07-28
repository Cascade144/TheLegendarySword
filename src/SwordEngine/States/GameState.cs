using SkiaSharp;
using SwordEngine.Utilities;
using SwordEngine.World;

namespace SwordEngine.States
{
    /// <summary>
    /// The game state class, which is responsible for managing the game world and its updates and rendering.
    /// </summary>
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
            var worldPath = ResourcePaths.ResolveResourcePath("res", "worlds", "world1.txt");
            world = new World.World(handler, worldPath);
            handler.SetWorld(world);
        }

        /// <summary>
        /// Calls the world object's update method.
        /// </summary>
        public override void Update()
        {
            world.Update();
        }

        /// <summary>
        /// Calls the world object's render method.
        /// </summary>
        /// <param name="canvas"></param>
        public override void Render(SKCanvas canvas)
        {
            world.Render(canvas);
        }
    }
}
