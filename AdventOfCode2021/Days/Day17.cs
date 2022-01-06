using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2021.Days
{
    public class Day17 : DayBase
    {
        public Day17(int day)
            : base(day)
        { }

        public override string SolvePart1()
        {
            var input = File
                .ReadAllText(_inputPath)
                .Replace("target area: x=", "")
                .Replace(", y=", "..")
                .Split("..", StringSplitOptions.RemoveEmptyEntries)
                .Select(x => int.Parse(x))
                .ToArray();

            var n = input[2] * -1 - 1;
            return (n * (n+1) / 2).ToString();
        }

        public override string SolvePart2()
        {
            var input = File
                .ReadAllText(_inputPath)
                .Replace("target area: x=", "")
                .Replace(", y=", "..")
                .Split("..", StringSplitOptions.RemoveEmptyEntries)
                .Select(x => int.Parse(x))
                .ToArray();

            return "";
        }
    }
}
