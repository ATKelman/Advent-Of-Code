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

        readonly Dictionary<char, int> _scoreTable = new()
        {
            { ')', 3 },
            { ']', 57 },
            { '}', 1197 },
            { '>', 25137 }
        };
        readonly Dictionary<char, int> _incompleteScore = new()
        {
            { '(', 1 },
            { '[', 2 },
            { '{', 3 },
            { '<', 4 }
        };

        readonly List<(char open, char close)> _pairs = new()
        {
            ('(', ')'),
            ('[', ']'),
            ('{', '}'),
            ('<', '>')
        };

        public override string SolvePart1()
        {
            var illegalCharacters = new List<char>();

            File
                .ReadAllLines(_inputPath)
                .ToList()
                .ForEach(line =>
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
                });

            return illegalCharacters
                .GroupBy(x => x)
                .Select(x => x.Count() * _scoreTable[x.Key])
                .Sum()
                .ToString();
        }

        public override string SolvePart2()
        {
            List<long> results = new();

            File
                .ReadAllLines(_inputPath)
                .ToList()
                .ForEach(line =>
                {
                    var isIllegal = false;
                    var stack = new Stack<char>();
                    foreach (var item in line)
                    {
                        if (_pairs.Any(x => x.open == item))
                            stack.Push(item);
                        else
                        {
                            var opening = stack.Pop();
                            if (!_pairs.Any(x => x.open == opening && x.close == item))
                            {
                                isIllegal = true;
                                stack.Push(opening);
                            }
                        }
                    }

                    if (!isIllegal)
                    {
                        long score = 0;
                        while (stack.Count > 0)
                        {
                            var opening = stack.Pop();
                            score = (score * 5) + _incompleteScore[opening];
                        }
                        results.Add(score);
                    }
                });
            var sorted = results.OrderBy(x => x);
            return sorted.ElementAt((results.Count) / 2).ToString();
        }
    }
}
