using System.Drawing;
using SwordEngine.States;
using SwordEngine.Gfx;

namespace SwordEngine
{
    public class MainGame
    {

        #region Graphics

        /// <summary>
        /// Display of the game.
        /// </summary>
        private Display.Display display;

        /// <summary>
        /// The window's width/height in pixels.
        /// </summary>
        private int width, height;

        /// <summary>
        /// The game's camera.
        /// </summary>
        private GameCamera gameCamera;

        /// <summary>
        /// The set frames per second
        /// </summary>
        private int fps;

        /// <summary>
        /// The main graphics object.
        /// </summary>
        private Graphics g;

        /// <summary>
        /// The main buffer strategy.
        /// </summary>
        //private BufferStrategy bs;

        #endregion

        #region Game Logic

        /// <summary>
        /// The game's handler.
        /// </summary>
        private Handler handler;

        /// <summary>
        /// The main game thread.
        /// </summary>
        private Thread thread;

        /// <summary>
        /// Determines whether or not the game is currently running.
        /// </summary>
        private bool running = false;

        #endregion

        #region Gamestates

        /// <summary>
        /// Contains the game's state.
        /// </summary>
        private State gameState;

        /// <summary>
        /// Contains the setting's state.
        /// </summary>
        private State settingsState;

        /// <summary>
        /// Contains the title's state.
        /// </summary>
        private State titleState;

        /// <summary>
        /// Contains the game over state.
        /// </summary>
        private State gameOverState;

        /// <summary>
        /// Contains the win state.
        /// </summary>
        private State winState;

        #endregion

        /// <summary>
        /// Constructs the MainGame object, and sets the default values of the window
        /// </summary>
        /// <param name="width"></param>
        /// <param name="height"></param>
        public MainGame(int width, int height)
        {

            this.width = width;
            this.height = height;

            //keyManager = new KeyManager();
            //mouseManager = new MouseManager();
        }

        /// <summary>
        /// The Main update method.
        /// </summary>
        private void update()
        {
            keyManager.update();
            if (State.getState() == titleState)
            {
                State.setState(titleState);
            }
            else
            {
                if (State.getState() == winState)
                {
                    State.setState(winState);
                }
                else
                {
                    if (State.getState() == gameOverState)
                        State.setState(gameOverState);
                    else
                    {
                        if (keyManager.pause)
                            State.setState(settingsState);
                        else
                            State.setState(gameState);
                    }
                }
            }
            if (State.getState() != null)
            {
                State.getState().Update();
            }
        }

        /// <summary>
        /// The Main render method.
        /// This grabs the canvas and clears its screen,
        /// then it calls the current state's render method.
        /// Finally it draws whatever was rendered onto the screen.
        /// If there isn't a state in the variable, then it does nothing.
        /// </summary>
        private void render()
        {
            // Gets the amount of buffers the canvas is going to use.
            bs = display.GetCanvas().getBufferStrategy();

            // If there isn't any buffers get 3 buffers.
            if (bs == null)
            {
                display.GetCanvas().createBufferStrategy(3);
                return;
            }

            g = bs.getDrawGraphics();

            // Clear the current screen.
            g.clearRect(0, 0, width, height);

            // Render the game.
            if (State.getState() != null)
            {
                State.getState().render(g);
            }

            // Draws to the main screen.
            bs.show();
            g.dispose();
        }

        /// <summary>
        /// Initializes all the game/window variables and sets the defaults.
        /// Current default state is the Game state.
        /// </summary>
        private void init()
        {

            display = new Display.Display(width, height);
            //display.GetSurface().addKeyListener(keyManager);
            //display.GetSurface().addMouseMotionListener(mouseManager);
            //display.GetSurface().addMouseListener(mouseManager);
            //display.GetCanvas().addMouseMotionListener(mouseManager);
            //display.GetCanvas().addMouseListener(mouseManager);
            Assets.init();

            handler = new Handler(this);
            gameCamera = new GameCamera(handler, 0, 0);


            gameState = new GameState(handler);
            settingsState = new Settings(handler);
            titleState = new TitleState(handler);
            gameOverState = new GameOverState(handler);
            winState = new WinState(handler);
            State.setState(titleState);
        }

        /// <summary>
        /// Starts the main game loop.
        /// </summary>
        public void Start()
        {
            if (running)
                return;
            running = true;
            thread = new Thread();
            thread.start();
        }

        /// <summary>
        /// Stops the main game loop.
        /// </summary>
        public void Stop()
        {
            if (!running)
                return;

            // Stop main game thread here.
        }

        /**
         * The Run loop determines when to call the main update and render methods.
         * It calls the MainGame's initialization method and uses a frames per second
         * algorithm.
         * Also displays the frames per second on the console and window title.
         */
        public void Run()
        {
            init();

            // Frames Per Second, the amount of times we want to call this method.
            fps = 60;

            // The max time in nanoseconds that we have to execute the update and render methods.
            double timePerUpdate = 1000000000 / fps;
            // 1 second == 1 billion nanoseconds
            // We're using nanoseconds because it is much more accurate than normal seconds.

            // The amount of time we have until we have to call the update/renders methods again.
            double delta = 0;
            long now;   // the computer's current time (in nanoseconds)
            long lastTime = DateTime.Now.Ticks; // the last time we called this method

            long timer = 0; // times until we get to one seconds	
            int updates = 0; // how many times the update/render methods are called

            while (running)
            {
                now = DateTime.Now.Ticks; // set the current time

                // makes sure that delta is somewhere between 0 and 1
                delta += (now - lastTime) / timePerUpdate;

                // adds the amount of nanoseconds that have passed since this 
                // method has been called
                timer += now - lastTime;
                lastTime = now;

                // check if you need to render something
                if (delta >= 1)
                {
                    update();
                    render();
                    updates++;
                    delta--;
                }

                // checks if the timer has exceeded 1 second
                if (timer >= 1000000000)
                {
                    //System.out.println("Updates/Frames: " + updates);
                    display.getFrame().setTitle((" | FPS - " + updates));
                    updates = 0;
                    timer = 0;
                }
            }

            Stop();
        }

        #region Getters and Setters

        /// <summary>
        /// Returns the MainGame keyManager.
        /// </summary>
        //public KeyManager getKeyManager()
        //{
        //    return keyManager;
        //}

        /// <summary>
        /// Returns the MainGame mouse Manager.
        /// </summary>
        //public MouseManager getMouseManager()
        //{
        //    return mouseManager;
        //}

        /// <summary>
        /// Returns the MainGame Game Camera.
        /// </summary>
        public GameCamera getGameCamera()
        {
            return gameCamera;
        }

        /// <summary>
        ///  Returns the window's width in pixels.
        /// </summary>
        public int getWidth()
        {
            return width;
        }


        /// <summary>
        /// Returns the window's height in pixels.
        /// </summary>
        public int getHeight()
        {
            return height;
        }

        /// <summary>
        /// Returns the game state
        /// </summary>
        public State getGameState()
        {
            return gameState;
        }

        /// <summary>
        /// Returns the setting state
        /// </summary>
        public State getSettingsState()
        {
            return settingsState;
        }

        /// <summary>
        /// Returns the game over state.
        /// </summary>
        public State getGameOverState()
        {
            return gameOverState;
        }

        /// <summary>
        /// Returns the win state.
        /// </summary>
        public State getWinState()
        {
            return winState;
        }

        #endregion
    }
}
