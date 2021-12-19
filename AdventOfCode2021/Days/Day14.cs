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
                    Count = (long)x.Count()
                })
                .ToList();

            var max = result.Max(x => x.Count);
            var min = result.Min(x => x.Count);

            return (result.Max(x => x.Count) - result.Min(x => x.Count)).ToString();
        }

        public override string SolvePart2()
        {
            var input = File
                .ReadAllLines(_inputPath)
                .ToList();

            Dictionary<string, string> insertions = new();

            input
                .Where(x => x.Contains("->"))
                .Select(x => x.Split(" -> ", StringSplitOptions.RemoveEmptyEntries))
                .ToList()
                .ForEach(x =>
                {
                    insertions.Add(x[0], x[1]);
                });

            var template = input.First();

            Dictionary<string, long> pairs = new();
            _ = template.Aggregate((current, next) =>
            {
                if (!pairs.ContainsKey($"{current}{next}"))
                    pairs.Add($"{current}{next}", 0);
                pairs[$"{current}{next}"]++;
                return next;
            });

            for (int i = 0; i < 40; i++)
            {
                Dictionary<string, long> temp = new();
                foreach (var pair in pairs)
                {
                    if (!temp.ContainsKey($"{pair.Key[0]}{insertions[pair.Key]}"))
                        temp.Add($"{pair.Key[0]}{insertions[pair.Key]}", 0);
                    if (!temp.ContainsKey($"{insertions[pair.Key]}{pair.Key[1]}"))
                        temp.Add($"{insertions[pair.Key]}{pair.Key[1]}", 0);

                    temp[$"{pair.Key[0]}{insertions[pair.Key]}"] += pair.Value;
                    temp[$"{insertions[pair.Key]}{pair.Key[1]}"] += pair.Value;
                }

                pairs = temp;
            }

            Dictionary<char, long> count = new();
            foreach (var pair in pairs)
            {
                if (!count.ContainsKey(pair.Key[0]))
                    count.Add(pair.Key[0], 0);
                if (!count.ContainsKey(pair.Key[1]))
                    count.Add(pair.Key[1], 0);

                count[pair.Key[0]] += pair.Value;
                count[pair.Key[1]] += pair.Value;
            }

            foreach (var item in count.Keys.ToList())
            {
                if (item == template.First() || item == template.Last())
                    count[item] = (count[item] + 1) / 2; // Last and first value are in 1 less pair 
                else
                    count[item] = count[item] / 2; // Divide by 2 as every character is doubled || counted twice as in both of its pairs
            }


            return (count.Max(x => x.Value) - count.Min(x => x.Value)).ToString();
        }
    }
}
