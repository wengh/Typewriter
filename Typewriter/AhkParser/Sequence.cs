using System.Collections.Generic;
using System.Linq;

namespace Typewriter.AhkParser
{
    public class Sequence : List<KeyAction>
    {
        public Sequence()
        {
        }

        public Sequence(params K[] stack)
        {
            AddRange(stack
               .Select(x => new KeyAction(x, UpDown.Down)));

            AddRange(stack.Reverse()
               .Select(x => new KeyAction(x, UpDown.Up)));
        }

        public Sequence Clone()
        {
            var clone = new Sequence();
            clone.AddRange(this);
            return clone;
        }

        public static Sequence Shift(K key)
        {
            return new Sequence(K.Shift, key);
        }

        public static implicit operator Sequence(K key)
        {
            return new() {new(key, UpDown.Down), new(key, UpDown.Up)};
        }

        public static implicit operator Sequence(KeyAction action)
        {
            return new() {action};
        }
    }
}