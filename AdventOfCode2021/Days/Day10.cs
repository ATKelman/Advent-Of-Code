using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2021.Days
{
    public class Day10 : DayBase
    {
        public Day10(int day)
            : base(day)
        { }

        Dictionary<char, int> _scoreTable = new()
        {
            { ')', 3 },
            { ']', 57 },
            { '}', 1197 },
            { '>', 25137 }
        };

        List<(char open, char close)> _pairs = new()
        {
            ('(', ')'),
            ('[', ']'),
            ('{', '}'),
            ('<', '>')
        };

        public override string SolvePart1()
        {
            var input = File
                .ReadAllLines(_inputPath);

            var illegalCharacters = new List<char>();
            foreach (var line in input)
            {
                var stack = new Stack<char>();
                foreach (var item in line)
                {
                    if (_pairs.Any(x => x.open == item))
                        stack.Push(item);
                    else
                    {
                        var opening = stack.Pop();
                        if (_pairs.Any(x => x.open == opening && x.close == item))
                            continue;
                        else
                            illegalCharacters.Add(item);
                    }
                }
            }

            return illegalCharacters
                .GroupBy(x => x)
                .Select(x => x.Count() * _scoreTable[x.Key])
                .Sum()
                .ToString();
        }

        public override string SolvePart2()
        {
            return "";
        }
    }
}
