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
        public int getWidth()
        {
            return mainGame.getWidth();
        }

        /// <summary>
        /// Returns the window's height in pixels
        /// </summary>
        /// <returns></returns>
        public int getHeight()
        {
            return mainGame.getHeight();
        }

        /// <summary>
        /// Returns the mainGame keyManager.
        /// </summary>
        public KeyManager getKeyManager()
        {
            return mainGame.getKeyManager();
        }

        /// <summary>
        /// Returns the mainGame mouseManager
        /// </summary>
        public MouseManager getMouseManager()
        {
            return mainGame.getMouseManager();
        }

        /// <summary>
        /// Return the MainGame camera.
        /// </summary>
        public GameCamera getGameCamera()
        {
            return mainGame.getGameCamera();
        }

        /// <summary>
        /// Returns the MainGame object itself.
        /// </summary>
        public MainGame getGame()
        {
            return mainGame;
        }

        /// <summary>
        /// Sets the MainGame object to whatever is passed in
        /// </summary>
        /// <param name="game">The main game object.</param>
        public void setGame(MainGame game)
        {
            this.mainGame = game;
        }

        /// <summary>
        /// Returns the main world object
        /// </summary>
        public World.World getWorld()
        {
            return world;
        }

        /// <summary>
        /// Sets the main world object
        /// </summary>
        public void setWorld(World.World world)
        {
            this.world = world;
        }
    }
}
