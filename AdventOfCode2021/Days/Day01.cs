using System.IO;
using System.Linq;

namespace AdventOfCode2021.Days
{
    public class Day01 : DayBase
    {
        public Day01(int day)
            : base(day)
        { }

        public override string SolvePart1()
        {
            var count = 0;
            var input = File
                .ReadAllLines(_inputPath)
                .Select(x => int.Parse(x))
                .ToList()
                .Aggregate((x, y) =>
                {
                    if (y > x) count++;
                    return y;
                });

            return count.ToString();
        }

        public override string SolvePart2()
        {
            var count = 0;
            var input = File
                .ReadAllLines(_inputPath)
                .Select((x, i) => new { value = int.Parse(x), index = i })
                .ToList();
            _ = input
                .Aggregate((current, next) =>
                {
                    if (next.index + 2 < input.Count)
                    {
                        var firstSum = current.value + next.value + input[next.index + 1].value;
                        var secondSum = next.value + input[next.index + 1].value + input[next.index + 2].value;

                        if (secondSum > firstSum) count++;
                    }

                    return next;
                });

            return count.ToString();
        }
    }
}
