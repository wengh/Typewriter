using System;

namespace Typewriter.AhkParser.KeyMaps
{
    public static class SingleCharMap
    {
        public static Sequence Get(char c)
        {
            if (c >= 128 || k[c] == null)
                throw new ArgumentException($"Character '{c}' has no corresponding sequence");

            return k[c].Clone();
        }

        private static readonly Sequence[] k = new Sequence[128];

        static SingleCharMap()
        {
            k['0'] = K.Alpha0;
            k['1'] = K.Alpha1;
            k['2'] = K.Alpha2;
            k['3'] = K.Alpha3;
            k['4'] = K.Alpha4;
            k['5'] = K.Alpha5;
            k['6'] = K.Alpha6;
            k['7'] = K.Alpha7;
            k['8'] = K.Alpha8;
            k['9'] = K.Alpha9;

            k['a'] = K.A;
            k['b'] = K.B;
            k['c'] = K.C;
            k['d'] = K.D;
            k['e'] = K.E;
            k['f'] = K.F;
            k['g'] = K.G;
            k['h'] = K.H;
            k['i'] = K.I;
            k['j'] = K.J;
            k['k'] = K.K;
            k['l'] = K.L;
            k['m'] = K.M;
            k['n'] = K.N;
            k['o'] = K.O;
            k['p'] = K.P;
            k['q'] = K.Q;
            k['r'] = K.R;
            k['s'] = K.S;
            k['t'] = K.T;
            k['u'] = K.U;
            k['v'] = K.V;
            k['w'] = K.W;
            k['x'] = K.X;
            k['y'] = K.Y;
            k['z'] = K.Z;

            k['A'] = Sequence.Shift(K.A);
            k['B'] = Sequence.Shift(K.B);
            k['C'] = Sequence.Shift(K.C);
            k['D'] = Sequence.Shift(K.D);
            k['E'] = Sequence.Shift(K.E);
            k['F'] = Sequence.Shift(K.F);
            k['G'] = Sequence.Shift(K.G);
            k['H'] = Sequence.Shift(K.H);
            k['I'] = Sequence.Shift(K.I);
            k['J'] = Sequence.Shift(K.J);
            k['K'] = Sequence.Shift(K.K);
            k['L'] = Sequence.Shift(K.L);
            k['M'] = Sequence.Shift(K.M);
            k['N'] = Sequence.Shift(K.N);
            k['O'] = Sequence.Shift(K.O);
            k['P'] = Sequence.Shift(K.P);
            k['Q'] = Sequence.Shift(K.Q);
            k['R'] = Sequence.Shift(K.R);
            k['S'] = Sequence.Shift(K.S);
            k['T'] = Sequence.Shift(K.T);
            k['U'] = Sequence.Shift(K.U);
            k['V'] = Sequence.Shift(K.V);
            k['W'] = Sequence.Shift(K.W);
            k['X'] = Sequence.Shift(K.X);
            k['Y'] = Sequence.Shift(K.Y);
            k['Z'] = Sequence.Shift(K.Z);

            k['`'] = K.Backtick;
            k[' '] = K.Space;
            k['-'] = K.Minus;
            k['='] = K.Equal;
            k['['] = K.LeftBracket;
            k[']'] = K.RightBracket;
            k[';'] = K.Semicolon;
            k['\''] = K.Apostrophe;
            k['\\'] = K.Backslash;
            k[','] = K.Comma;
            k['.'] = K.Period;
            k['/'] = K.Slash;

            k['~'] = Sequence.Shift(K.Backtick);
            k['!'] = Sequence.Shift(K.Alpha1);
            k['@'] = Sequence.Shift(K.Alpha2);
            k['#'] = Sequence.Shift(K.Alpha3);
            k['$'] = Sequence.Shift(K.Alpha4);
            k['%'] = Sequence.Shift(K.Alpha5);
            k['^'] = Sequence.Shift(K.Alpha6);
            k['&'] = Sequence.Shift(K.Alpha7);
            k['*'] = Sequence.Shift(K.Alpha8);
            k['('] = Sequence.Shift(K.Alpha9);
            k[')'] = Sequence.Shift(K.Alpha0);
            k['_'] = Sequence.Shift(K.Minus);
            k['+'] = Sequence.Shift(K.Equal);
            k['{'] = Sequence.Shift(K.LeftBracket);
            k['}'] = Sequence.Shift(K.RightBracket);
            k[':'] = Sequence.Shift(K.Semicolon);
            k['"'] = Sequence.Shift(K.Apostrophe);
            k['|'] = Sequence.Shift(K.Backslash);
            k['<'] = Sequence.Shift(K.Comma);
            k['>'] = Sequence.Shift(K.Period);
            k['?'] = Sequence.Shift(K.Slash);
        }
    }
}