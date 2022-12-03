using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2022.Days
{
    class Day03 : DayBase
    {
        public Day03(int day)
        : base(day)
        { }

        public override string SolvePart1()
        {
            var prioritySum = 0;
            File.ReadAllLines(_inputPath)
                .ToList()
                .ForEach(x =>
                {
                    var compartmentOne = x[..(x.Length / 2)];
                    var compartmentTwo = x[(x.Length / 2)..];
                    var commonItem = compartmentOne.Intersect(compartmentTwo).First();
                    // A has value 65, for it to start at 27 substract 38
                    // a has value 97, for it to start at 1 subtract 96
                    prioritySum += commonItem - (Char.IsUpper(commonItem) ? 38 : 96);
                });

            return prioritySum.ToString();
        }

        public override string SolvePart2()
        {
            var prioritySum = 0;

            File.ReadAllLines(_inputPath)
                .Select((x, i) => new { Index = i, Value = x })
                .GroupBy(x => x.Index / 3)
                .Select(x => x.Select(v => v.Value).ToList())
                .ToList()
                .ForEach(x =>
                {
                    var commonItem = x[0].Intersect(x[1]).Intersect(x[2]).First();
                    prioritySum += commonItem - (Char.IsUpper(commonItem) ? 38 : 96);
                });

            return prioritySum.ToString();
        }
    }
}
