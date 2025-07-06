using SwordEngine.States;
using SwordEngine.Gfx;
using SkiaSharp;
using SwordEngine.Input;
using System.Drawing;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Diagnostics;

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
        private SKCanvas canvas;

        /// <summary>
        /// The main surface.
        /// </summary>
        private SKSurface surface;
        
        /// <summary>
        /// The game engine Key Manager.
        /// </summary>
        private KeyManager keyManager;

        /// <summary>
        /// The game engine Mouse Manager.
        /// </summary>
        private MouseManager mouseManager;

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

        private DisplayForm windowForm;

        #endregion

        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool AllocConsole();

        /// <summary>
        /// Constructs the MainGame object, and sets the default values of the window
        /// </summary>
        /// <param name="width"></param>
        /// <param name="height"></param>
        public MainGame(int width, int height, DisplayForm windowForm)
        {
            // Render Console
            AllocConsole();
            this.width = width;
            this.height = height;
            this.windowForm = windowForm;

            keyManager = new KeyManager();
            mouseManager = new MouseManager();
            windowForm.LoadKeyMouseListeners(keyManager, mouseManager);
        }

        /// <summary>
        /// The Main update method.
        /// </summary>
        private void update()
        {
            keyManager.Update();
            if (State.GetState() == titleState)
            {
                State.SetState(titleState);
            }
            else
            {
                if (State.GetState() == winState)
                {
                    State.SetState(winState);
                }
                else
                {
                    if (State.GetState() == gameOverState)
                    {
                        State.SetState(gameOverState);
                    }
                    else
                    {
                        if (keyManager.pause)
                        {
                            State.SetState(settingsState);
                        }
                        else
                        {
                            State.SetState(gameState);
                        }
                    }
                }
            }

            if (State.GetState() != null)
            {
                State.GetState().Update();
            }
        }

        /// <summary>
        /// The Main render method.
        /// This grabs the canvas and clears its screen,
        /// then it calls the current state's render method.
        /// Finally it draws whatever was rendered onto the screen.
        /// If there isn't a state in the variable, then it does nothing.
        /// </summary>
        private void Render()
        {
            // Gets the canvas
            canvas = display.GetCanvas();
            surface = display.GetSurface();

            // Clear the current screen.
            canvas.Clear();

            // Render the game.
            if (State.GetState() != null)
            {
                State.GetState().Render(canvas);
            }

            // Draws to the main screen.
            windowForm.RenderExternalCall(surface);
        }

        /// <summary>
        /// Initializes all the game/window variables and sets the defaults.
        /// Current default state is the Game state.
        /// </summary>
        private void init()
        {

            display = new Display.Display(width, height);
            Assets.init();

            handler = new Handler(this);
            gameCamera = new GameCamera(handler, 0, 0);


            gameState = new GameState(handler);
            settingsState = new Settings(handler);
            titleState = new TitleState(handler);
            gameOverState = new GameOverState(handler);
            winState = new WinState(handler);
            State.SetState(titleState);
        }

        /// <summary>
        /// Starts the main game loop.
        /// </summary>
        public void Start()
        {
            if (running)
            {
                return;
            }

            running = true;
            thread = new Thread(() => Run());
            thread.Start();
        }

        /// <summary>
        /// Stops the main game loop.
        /// </summary>
        public void Stop()
        {
            if (!running)
            {
                return;
            } 
            else
            {
                running = false;
                thread.Join();
            }
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

            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();
            int updates = 0; // how many times the update/render methods are called

            while (running)
            {
                stopWatch.Stop();
                TimeSpan ts = stopWatch.Elapsed;

                // makes sure that delta is somewhere between 0 and 1
                delta += ts.Nanoseconds / timePerUpdate;

                // check if you need to render something
                if (delta >= 1)
                {
                    update();
                    Render();
                    updates++;
                    delta--;
                }

                // checks if the timer has exceeded 1 second
                if (ts.Nanoseconds >= 1000000000)
                {
                    Console.WriteLine("Updates/Frames: " + updates);
                    //display.getFrame().setTitle((" | FPS - " + updates));
                    updates = 0;
                }
            }

            Stop();
        }

        #region Getters and Setters

        /// <summary>
        /// Returns the MainGame keyManager.
        /// </summary>
        public KeyManager GetKeyManager()
        {
            return keyManager;
        }

        /// <summary>
        /// Returns the MainGame mouse Manager.
        /// </summary>
        public MouseManager GetMouseManager()
        {
            return mouseManager;
        }

        /// <summary>
        /// Returns the MainGame Game Camera.
        /// </summary>
        public GameCamera GetGameCamera()
        {
            return gameCamera;
        }

        /// <summary>
        ///  Returns the window's width in pixels.
        /// </summary>
        public int GetWidth()
        {
            return width;
        }

        /// <summary>
        /// Returns the window's height in pixels.
        /// </summary>
        public int GetHeight()
        {
            return height;
        }

        /// <summary>
        /// Returns the game state
        /// </summary>
        public State GetGameState()
        {
            return gameState;
        }

        /// <summary>
        /// Returns the setting state
        /// </summary>
        public State GetSettingsState()
        {
            return settingsState;
        }

        /// <summary>
        /// Returns the game over state.
        /// </summary>
        public State GetGameOverState()
        {
            return gameOverState;
        }

        /// <summary>
        /// Returns the win state.
        /// </summary>
        public State GetWinState()
        {
            return winState;
        }

        #endregion
    }
}
