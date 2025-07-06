using Gma.System.MouseKeyHook;
using System.Windows.Forms;

namespace SwordEngine.Input
{
    public class MouseManager
    {
        private IKeyboardMouseEvents? mouseKeyHook;
        public bool left, right;
        private int x, y;

        public MouseManager()
        {
        }

        public void Subscribe(IKeyboardMouseEvents events)
        {
            events.MouseDown += HookMouseDown;
            events.MouseUp += HookMouseUp;
            events.MouseMove += MouseMoved;
        }

        public void Unsubscribe()
        {
            if (mouseKeyHook == null) return;
            mouseKeyHook.MouseDown -= HookMouseDown;
            mouseKeyHook.MouseUp -= HookMouseUp;
            mouseKeyHook.MouseMove -= MouseMoved;

            mouseKeyHook.Dispose();
        }

        private void HookMouseUp(object? sender, MouseEventArgs e)
        {
            Console.WriteLine($"MouseUp: {e.Button}");
            if (e.Button == MouseButtons.Left)
            {
                left = false;
            }
            else if (e.Button == MouseButtons.Right)
            {
                right = false;
            }
        }

        private void HookMouseDown(object? sender, MouseEventArgs e)
        {
            Console.WriteLine($"MouseDown: {e.Button}");
            if (e.Button == MouseButtons.Left)
            {
                left = true;
            }
            else if (e.Button == MouseButtons.Right)
            {
                right = true;
            }
        }

        public void MouseMoved(object? sender, MouseEventArgs e)
        {
            x = e.X;
            y = e.Y;
        }

        #region Getters and Setters

        public int GetX()
        {
            return x;
        }

        public int GetY()
        {
            return y;
        }

        #endregion
    }
}
