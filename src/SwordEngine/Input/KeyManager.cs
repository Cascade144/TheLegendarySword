using Gma.System.MouseKeyHook;
using System.Windows.Forms;

namespace SwordEngine.Input
{
    /// <summary>
    /// Class that manages the keyboard input for the game. It subscribes to keyboard events and updates the state of control keys accordingly.
    /// </summary>
    public class KeyManager
    {
        private IKeyboardMouseEvents? mouseKeyHook;

        /// <summary>
        /// Available keys to the key manager.
        /// </summary>
        private bool[] keys;

        /// <summary>
        /// Basic control keys.
        /// </summary>
        public bool up, down, left, right, space, pause;

        /// <summary>
        /// Initializes a new instance of the <see cref="KeyManager"/> class.
        /// Constructs the KeyManager and initializes the keys array to track the state of each key.
        /// </summary>
        public KeyManager()
        {
            keys = new bool[256];
        }

        /// <summary>
        /// Method to subscribe to keyboard events. It sets up event handlers for key down and key up events, allowing the KeyManager to track the state of keys.
        /// </summary>
        /// <param name="events">The keyboard and mouse events to subscribe to.</param>
        public void Subscribe(IKeyboardMouseEvents events)
        {
            mouseKeyHook = events;

            mouseKeyHook.KeyUp += AppHookKeyup;
            mouseKeyHook.KeyDown += AppHookKeyDown;
        }

        /// <summary>
        /// The method to unsubscribe from keyboard events.
        /// </summary>
        public void Unsubscribe()
        {
            if (mouseKeyHook == null) return;
            mouseKeyHook.KeyUp -= AppHookKeyup;
            mouseKeyHook.KeyDown -= AppHookKeyDown;

            mouseKeyHook.Dispose();
        }

        /// <summary>
        /// Method to handle the KeyUp event.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="KeyEventArgs"/> instance containing the event data.</param>
        private void AppHookKeyup(object? sender, KeyEventArgs e)
        {
            Console.WriteLine("KeyPress: \t {0}", e.KeyValue);
            int releasedKey = e.KeyValue;

            if (releasedKey != (int)Keys.P)
            {
                keys[releasedKey] = false;
            }

            e.Handled = true;
        }

        /// <summary>
        /// Method to handle the KeyDown event.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="KeyEventArgs"/> instance containing the event data.</param>
        private void AppHookKeyDown(object? sender, KeyEventArgs e)
        {
            Console.WriteLine("KeyPress: \t {0}", e.KeyValue);

            int pressedKey = e.KeyValue;

            if ((pressedKey == (int)Keys.P) && keys[pressedKey])
            {
                keys[pressedKey] = false;
            }
            else
            {
                keys[pressedKey] = true;
            }

            e.Handled = true;
        }

        /// <summary>
        /// Method to update the state of control keys based on the current state of the keys array.
        /// </summary>
        public void Update()
        {
            up = keys[(int)Keys.W] || keys[(int)Keys.Up];
            down = keys[(int)Keys.S] || keys[(int)Keys.Down];
            left = keys[(int)Keys.A] || keys[(int)Keys.Left];
            right = keys[(int)Keys.D] || keys[(int)Keys.Right];
            space = keys[(int)Keys.Space];
            pause = keys[(int)Keys.P];
        }

        /// <summary>
        /// Method to clear the state of all keys. It resets the keys array and sets all control keys to false.
        /// </summary>
        public void ClearAllKeys()
        {
            Array.Clear(keys, 0, keys.Length);
            up = down = left = right = space = pause = false;
        }
    }
}
