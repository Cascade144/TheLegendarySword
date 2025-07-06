using SkiaSharp;
using SwordEngine.Input;
using System.Runtime.InteropServices;
using Gma.System.MouseKeyHook;

namespace SwordEngine
{
    public partial class DisplayForm : Form
    {
        private IKeyboardMouseEvents keyMouseEvents;

        public DisplayForm()
        {
            InitializeComponent();
            MainGame game = new MainGame(600, 600, this);
            game.Start();
        }

        public void RenderExternalCall(SKSurface surface)
        {
            // Draws to the main screen.
            using (SKImage image = surface.Snapshot())
            using (SKData data = image.Encode())
            using (System.IO.MemoryStream mStream = new System.IO.MemoryStream(data.ToArray()))
            {
                pictureBox1.Image?.Dispose();
                pictureBox1.Image = new Bitmap(mStream, false);
            }
        }

        public void LoadKeyMouseListeners(KeyManager keyManager, MouseManager mouseManager)
        {
            keyManager.Subscribe(Hook.AppEvents());
            mouseManager.Subscribe(Hook.AppEvents());
        }

        private void Subscribe()
        {
            keyMouseEvents = Hook.AppEvents();
            keyMouseEvents.KeyDown += OnKeyDown;
            keyMouseEvents.KeyUp += OnKeyUp;

            keyMouseEvents.MouseUp += OnMouseUp;
            keyMouseEvents.MouseDown += OnMouseDown;

            keyMouseEvents.MouseMove += HookManager_MouseMove;
        }

        private void OnKeyDown(object? sender, KeyEventArgs e)
        {
            Console.WriteLine(string.Format("KeyDown  \t\t {0}\n", e.KeyCode));
        }

        private void OnKeyUp(object? sender, KeyEventArgs e)
        {
            Console.WriteLine(string.Format("KeyUp  \t\t\t {0}\n", e.KeyCode));
        }

        private void HookManager_MouseMove(object? sender, MouseEventArgs e)
        {
        }

        private void OnMouseDown(object? sender, MouseEventArgs e)
        {
            Console.WriteLine(string.Format("MouseDown \t\t {0}\n", e.Button));
        }

        private void OnMouseUp(object? sender, MouseEventArgs e)
        {
            Console.WriteLine(string.Format("MouseUp \t\t {0}\n", e.Button));
        }

        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool AllocConsole();
    }
}
