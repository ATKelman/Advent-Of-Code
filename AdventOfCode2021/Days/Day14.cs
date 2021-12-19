using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2021.Days
{
    public class Day14 : DayBase
    {
        public Day14(int day)
            : base(day)
        { }

        public override string SolvePart1()
        {
            var input = File
                .ReadAllLines(_inputPath)
                .ToList();

            var insertions = input
                .Where(x => x.Contains("->"))
                .Select(x => x.Split(" -> ", StringSplitOptions.RemoveEmptyEntries))
                .Select(x => new
                {
                    Adjacent = x[0],
                    Insert = x[1]
                })
                .ToList();

            var template = input.First();

            for (int steps = 0; steps < 10; steps++)
            {
                var newTemplate = "";

                template
                    .Aggregate((current, next) =>
                    {
                        newTemplate += current;
                        if (insertions.Any(x => x.Adjacent == $"{current}{next}"))
                            newTemplate += insertions
                                .Where(x => x.Adjacent == $"{current}{next}")
                                .Single().Insert;

                        return next;
                    });
                newTemplate += template.Last();
                template = newTemplate;
            }

            var result = template
                .Select(x => x)
                .GroupBy(x => x)
                .Select(x => new
                {
                    Character = x.Key,
                    Count = x.Count()
                })
                .ToList();

            var max = result.Max(x => x.Count);
            var min = result.Min(x => x.Count);

            return (result.Max(x => x.Count) - result.Min(x => x.Count)).ToString();
        }

        public override string SolvePart2()
        {
            return "";
        }
    }
}
