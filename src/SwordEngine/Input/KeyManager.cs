using Gma.System.MouseKeyHook;
using System.Windows.Forms;

namespace SwordEngine.Input
{
    public class KeyManager
    {
        /// <summary>
        /// Available keys to the key manager.
        /// </summary>
        private bool[] keys;

        /// <summary>
        /// Basic control keys.
        /// </summary>
        public bool up, down, left, right, space, pause;

        public KeyManager()
        {
            keys = new bool[256];
        }

        public void Subscribe(IKeyboardMouseEvents events)
        {
            events.KeyUp += AppHookKeyup;
            events.KeyDown += AppHookKeyDown;
        }

        private void AppHookKeyup(object? sender, KeyEventArgs e)
        {
            Console.WriteLine("KeyPress: \t {0}", e.KeyValue);
            int releasedKey = e.KeyValue;

            if (releasedKey != (int)Keys.P)
            {
                keys[releasedKey] = false;
            }
        }

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
        }

        public void Update()
        {
            up = keys[(int)Keys.W];
            down = keys[(int)Keys.S];
            left = keys[(int)Keys.A];
            right = keys[(int)Keys.D];
            space = keys[(int)Keys.Space];
            pause = keys[(int)Keys.P];
        }
    }
}
