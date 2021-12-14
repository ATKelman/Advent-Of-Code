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
            var input = File
                .ReadAllLines(_inputPath)
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
            int result = 0;

            var input = File
                .ReadAllLines(_inputPath)
                .ToArray();

            foreach (var line in input)
            {
                var instance = line
                    .Replace("|", " ")
                    .Split(" ", StringSplitOptions.RemoveEmptyEntries)
                    .Select(x => x.OrderBy(y => y))
                    .Select(x => string.Concat(x))
                    .OrderBy(x => x.Length)
                    .GroupBy(x => x)
                    .Select(x => x.Key)
                    .ToArray();

                var values = GetValueMappings(instance);

                var output = line
                    .Split("|", StringSplitOptions.RemoveEmptyEntries)
                    .Last()
                    .Split(" ", StringSplitOptions.RemoveEmptyEntries)
                    .Select(x => x.OrderBy(y => y))
                    .Select(x => string.Concat(x))
                    .Select(x => x.Replace(x, values.Where(y => y.Value == x).Single().Key.ToString()))
                    .Aggregate((x, y) => x + y);

                result += int.Parse(output);
            }

            return result.ToString();
        }

        private Dictionary<int, string> GetValueMappings(string[] instance)
        {
            var values = new Dictionary<int, string>();
            var one = instance.Where(x => x.Length == 2).Single();
            values.Add(1, one);

            var seven = instance.Where(x => x.Length == 3).Single();
            values.Add(7, seven);

            var four = instance.Where(x => x.Length == 4).Single();
            values.Add(4, four);

            var eight = instance.Where(x => x.Length == 7).Single();
            values.Add(8, eight);

            // 0 6 9
            var sixChars = instance.Where(x => x.Length == 6).ToList();
            if (sixChars.Count != 3) throw new Exception("There should only be 3 values with 6 characters.");

            // 6 = the one that doesnt match all of 7 or 1
            var six = sixChars.Where(x => x.Intersect(values[1]).Count() != 2).Single();
            sixChars.Remove(six);
            values.Add(6, six);

            // 0 = the one that doesnt match all of 4 
            var zero = sixChars.Where(x => x.Intersect(values[4]).Count() != 4).Single();
            sixChars.Remove(zero);
            values.Add(0, zero);
            values.Add(9, sixChars.First());

            // 2 3 5
            var fiveChars = instance.Where(x => x.Length == 5).ToList();
            if (fiveChars.Count != 3) throw new Exception("There should only be 3 values with 5 characters.");

            // 3 = the one that matches all of 1
            var three = fiveChars.Where(x => x.Intersect(values[1]).Count() == 2).Single();
            fiveChars.Remove(three);
            values.Add(3, three);

            // 5 = the one that matches 3 when compared to 4
            var five = fiveChars.Where(x => x.Intersect(values[4]).Count() == 3).Single();
            fiveChars.Remove(five);
            values.Add(5, five);
            values.Add(2, fiveChars.First());

            return values;
        }
    }
}
