namespace Typewriter.AhkParser
{
    public record KeyAction
    {
        public K Key { get; }
        public UpDown UpDown { get; }

        public KeyAction(K key, UpDown upDown) => (Key, UpDown) = (key, upDown);
    }

    public enum UpDown
    {
        Up, Down
    }
}