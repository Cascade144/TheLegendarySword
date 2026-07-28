using SwordEngine.Gfx;
using SwordEngine.Input;

namespace SwordEngine
{
    /// <summary>
    /// The generic handler for the game engine.
    /// </summary>
    public class Handler
    {
        /// <summary>
        /// The main game object.
        /// </summary>
        private MainGame mainGame;

        /// <summary>
        /// The main world object.
        /// </summary>
        private World.World world;

        /// <summary>
        /// Constructs the handler object and sets its mainGame 
        /// variable to whatever is passed in.
        /// </summary>
        /// <param name="game">The main game.</param>
        public Handler(MainGame game)
        {
            mainGame = game;
        }

        /// <summary>
        /// Returns the window's width in pixels
        /// </summary>
        public int GetWidth()
        {
            return mainGame.GetWidth();
        }

        /// <summary>
        /// Returns the window's height in pixels
        /// </summary>
        /// <returns></returns>
        public int GetHeight()
        {
            return mainGame.GetHeight();
        }

        /// <summary>
        /// Returns the mainGame keyManager.
        /// </summary>
        public KeyManager GetKeyManager()
        {
            return mainGame.GetKeyManager();
        }

        /// <summary>
        /// Returns the mainGame mouseManager.
        /// </summary>
        public MouseManager GetMouseManager()
        {
            return mainGame.GetMouseManager();
        }

        /// <summary>
        /// Return the MainGame camera.
        /// </summary>
        public GameCamera GetGameCamera()
        {
            return mainGame.GetGameCamera();
        }

        /// <summary>
        /// Returns the MainGame object itself.
        /// </summary>
        public MainGame GetGame()
        {
            return mainGame;
        }

        /// <summary>
        /// Sets the MainGame object to whatever is passed in.
        /// </summary>
        /// <param name="game">The main game object.</param>
        public void SetGame(MainGame game)
        {
            mainGame = game;
        }

        /// <summary>
        /// Returns the main world object.
        /// </summary>
        public World.World GetWorld()
        {
            return world;
        }

        /// <summary>
        /// Sets the main world object.
        /// </summary>
        public void SetWorld(World.World world)
        {
            this.world = world;
        }
    }
}
