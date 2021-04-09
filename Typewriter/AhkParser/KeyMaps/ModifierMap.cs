namespace Typewriter.AhkParser.KeyMaps
{
    public static class ModifierMap
    {
        public static bool TryGet(char c, out K key)
        {
            key = c switch
            {
                '^' => K.Ctrl,
                '+' => K.Shift,
                '!' => K.Alt,
                '#' => K.LeftWin,
                _ => default,
            };
            return key != default;
        }
    }
}