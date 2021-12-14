using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2021.Days
{
    public class Day08 : DayBase
    {
        public Day08(int day)
            : base(day)
        { }

        public override string SolvePart1()
        {
            var input = File.ReadAllLines(_inputPath)
                .Select(x =>
                    x.Split('|', StringSplitOptions.RemoveEmptyEntries)
                    .Last()
                    .Split(" ", StringSplitOptions.RemoveEmptyEntries)
                    .Select(x => x.Length)
                    .Where(x => x == 2 || x == 3 || x == 4 || x == 7)
                    .Count())
                .Sum();

            return input.ToString();
        }

        public override string SolvePart2()
        {
            return "";
        }
    }
}
