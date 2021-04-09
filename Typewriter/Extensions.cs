using Core_Interception.Lib;
using Typewriter.AhkParser;

namespace Typewriter
{
    public static class Extensions
    {
        public static ManagedWrapper.Stroke ToStroke(this K key)
        {
            var stroke = new ManagedWrapper.Stroke();
            ushort keyCode = (ushort) key;
            if (keyCode >= 256)
            {
                keyCode -= 256;
                stroke.key.state = 2;
            }

            stroke.key.code = keyCode;
            return stroke;
        }

        public static ManagedWrapper.Stroke ToStroke(this KeyAction action)
        {
            var stroke = action.Key.ToStroke();
            if (action.UpDown == UpDown.Up)
                stroke.key.state |= 1;

            return stroke;
        }

        public static ManagedWrapper.Stroke Down(this ManagedWrapper.Stroke stroke)
        {
            stroke.key.state |= 1;
            return stroke;
        }
    }
}