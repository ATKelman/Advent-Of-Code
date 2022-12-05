using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2022.Days
{
    internal class Day04 : DayBase
    {
        public Day04(int day)
            : base(day)
        { }

        public override string SolvePart1()
        {
            int totalOverlaps = 0;
            File.ReadAllLines(_inputPath)
                .ToList()
                .ForEach(x =>
                {
                    var elves = x.Split(',')
                        .Select(x =>
                        {
                            var values = x.Split('-').Select(int.Parse).ToArray();
                            return Enumerable.Range(values[0], values[1] - values[0] + 1);
                        })
                        .ToList();

                    var intersects = elves[0].Intersect(elves[1]).Count();

                    if (intersects == elves[0].Count() || intersects == elves[1].Count())
                        totalOverlaps++;
                });
            return totalOverlaps.ToString();
        }

        public override string SolvePart2()
        {
            int overlaps = 0;
            File.ReadAllLines(_inputPath)
                .ToList()
                .ForEach(x =>
                {
                    var elves = x.Split(',')
                        .Select(x =>
                        {
                            var values = x.Split('-').Select(int.Parse).ToArray();
                            return Enumerable.Range(values[0], values[1] - values[0] + 1);
                        })
                        .ToList();

                    if (elves[1].Intersect(elves[0]).Count() >= 1)
                        overlaps++;
                });
            return overlaps.ToString();
        }
    }
}
