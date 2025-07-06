using SkiaSharp;
using SwordEngine.World;

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
            world = new World.World(handler, "F:/source/TheLegendarySword/res/worlds/world1.txt");
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
