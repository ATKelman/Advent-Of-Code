using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode2023.Days;
public class Day3 : DayBase
{
    private string[] input;

    public Day3(IConfiguration config, bool isTest)
        : base(config, 2023, 3, isTest)
    {

    }

    public override string SolvePart1()
    {
        Regex symbolRegex = new(@"[^.0-9]");
        Regex valueRegex = new(@"\d+");

        input = File.ReadAllLines(_inputPath);
        List<(int x, int y)> symbols = new();
        List<Number> numbers = new();
        for (int i = 0; i < input.Length; i++)
        {
            var matches = symbolRegex.Matches(input[i]);
            foreach (Match match in matches)
                symbols.Add((match.Index, i));

            var nums = valueRegex.Matches(input[i]);
            foreach (Match match in nums)
            {
                Number instance = new();
                instance.Row = i;
                instance.Columns = Enumerable.Range(match.Index, match.Length).ToList();
                instance.Value = int.Parse(match.Value);

                numbers.Add(instance);
            }
        }

        foreach (var symbol in symbols)
        {
            for (int x = -1; x < 2; x++)
            {
                for (int y = -1; y < 2; y++)
                {
                    var dx = symbol.x + x;
                    var dy = symbol.y + y;

                    var num = numbers.Where(n => n.Row == dy && n.Columns.Contains(dx) && !n.IsAdjacent).FirstOrDefault();
                    if (num is not null)
                        num.IsAdjacent = true;
                }
            }
        }

        return numbers.Where(x => x.IsAdjacent).Sum(x => x.Value).ToString();
    }

    private class Number
    {
        public int Row { get; set; }
        public List<int> Columns { get; set; } = new();
        public int Value { get; set; }
        public bool IsAdjacent { get; set; }
    }

    public override string SolvePart2()
    {
        Regex gearRegex = new(@"\*");
        Regex valueRegex = new(@"\d+");

        input = File.ReadAllLines(_inputPath);
        List<GearRatios> gears = new();
        List<GearRatios> numbers = new();
        for (int i = 0; i < input.Length; i++)
        {
            var matches = gearRegex.Matches(input[i]);
            foreach (Match match in matches)
            {
                GearRatios instance = new();
                for (int x = -1; x < 2; x++)
                {
                    for (int y = -1; y < 2; y++)
                        instance.Positions.Add((match.Index + x, i + y));
                }
                gears.Add(instance);
            }

            var nums = valueRegex.Matches(input[i]);
            foreach (Match match in nums)
            {
                GearRatios instance = new();
                instance.Value = int.Parse(match.Value);
                for (int x = 0; x < match.Length; x++)
                    instance.Positions.Add((match.Index + x, i));

                numbers.Add(instance);
            }
        }

        int r = 0;

        foreach (GearRatios gear in gears)
        {
            var adjacent = numbers.Where(n => n.Positions.Intersect(gear.Positions).Any());
            if (adjacent.Count() == 2)
                r += adjacent.First().Value * adjacent.Last().Value;
        }

        return r.ToString();
    }

    private class GearRatios
    {
        public HashSet<(int x, int y)> Positions { get; set; } = new();
        public int Value { get; set; } = 0;
    }
}
