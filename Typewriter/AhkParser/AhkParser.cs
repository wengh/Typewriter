using System;
using System.Collections.Generic;
using Typewriter.AhkParser.KeyMaps;

namespace Typewriter.AhkParser
{
    public class AhkParser
    {
        private string _A;
        private int _ii;
        private Sequence _sequence = new();
        private Stack<K> _modifiers = new();

        public static Sequence Parse(string str)
        {
            var parser = new AhkParser(str);
            parser.ParseAll();
            return parser._sequence;
        }

        private AhkParser(string str)
        {
            _A = str;
        }

        private void ParseAll()
        {
            try
            {
                while (_ii < _A.Length)
                {
                    ParseUnit();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(_A);
                Console.Write(new String(' ', _ii));
                Console.Write('^');
                throw new ArgumentException($"Parsing exception at position {_ii}", e);
            }
        }

        private void Add(Sequence sequence)
        {
            _sequence.AddRange(sequence);
        }

        private void ParseUnit()
        {
            while (_ii < _A.Length)
            {
                if (ModifierMap.TryGet(_A[_ii], out var modifier))
                {
                    _modifiers.Push(modifier);
                    Add(new KeyAction(modifier, UpDown.Down));
                    _ii++;
                }
                else
                {
                    if (_A[_ii] == '{')
                        ParseEscaped();
                    else
                        Add(SingleCharMap.Get(_A[_ii]));
                    _ii++;
                    break;
                }
            }

            while (_modifiers.Count != 0)
            {
                var modifier = _modifiers.Pop();
                Add(new KeyAction(modifier, UpDown.Up));
            }
        }

        private void ParseEscaped()
        {
            int end = _ii + 2;
            while (end < _A.Length && _A[end] != '}')
                end++;

            if (end == _A.Length)
                throw new FormatException($"Escaped action has no closing bracket");

            string[] args = _A.Substring(_ii + 1, end - _ii - 1).Split(' ');
            ParseEscapedArgs(args);

            _ii = end;
        }

        private void ParseEscapedArgs(string[] args)
        {
            if (args.Length == 0)
                throw new FormatException($"Escaped action is empty");
            if (args.Length > 2)
                throw new FormatException($"Escaped action has too many arguments");

            if (args.Length == 1)
            {
                Add(EscapedMap.Get(args[0]));
            }
            else
            {
                if (int.TryParse(args[1], out int reps))
                {
                    var press = EscapedMap.Get(args[0]);
                    for (int i = 0; i < reps; i++)
                    {
                        Add(press);
                    }
                }
                else if (args[1] == "down")
                {
                    Add(EscapedMap.GetDown(args[0]));
                }
                else if (args[1] == "up")
                {
                    Add(EscapedMap.GetUp(args[0]));
                }
            }
        }
    }
}