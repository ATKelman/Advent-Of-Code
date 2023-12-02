using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AdventOfCode2023.Days;
public class Day2 : DayBase
{
    private string[] input;

    public Day2(IConfiguration config, bool isTest)
        : base(config, 2023, 2, isTest)
    {
        //input = File
        //    .ReadAllLines(_inputPath);
    }

    public override string SolvePart1()
    {
        Dictionary<string, int> allowedPulls = new()
        {
            { "red", 12 },
            { "green", 13 },
            { "blue", 14 }
        };
        List<int> legalGames = [];

        input = File.ReadAllLines(_inputPath);
        foreach (string line in input)
        {
            Regex regex = new("(?<amount>\\d{1,2}) (?<colour>red|green|blue)");
            var cubes = regex.Matches(line);
            if (!cubes.Any(x => int.Parse(x.Groups["amount"].Value) > allowedPulls[x.Groups["colour"].Value]))
            {
                regex = new("Game (?<id>\\d+)");
                int id = int.Parse(regex.Match(line).Groups["id"].Value);
                legalGames.Add(id);
            }
        }
        return legalGames.Sum().ToString();
    }

    public override string SolvePart2()
    {
        int result = 0;

        input = File.ReadAllLines(_inputPath);
        foreach (string line in input)
        {
            Regex regex = new("(?<amount>\\d{1,2}) (?<colour>red|green|blue)");
            var cubes = regex.Matches(line);
            var red = cubes.Where(x => string.Equals(x.Groups["colour"].Value, "red")).Max(y => int.Parse(y.Groups["amount"].Value));
            var green = cubes.Where(x => string.Equals(x.Groups["colour"].Value, "green")).Max(y => int.Parse(y.Groups["amount"].Value));
            var blue = cubes.Where(x => string.Equals(x.Groups["colour"].Value, "blue")).Max(y => int.Parse(y.Groups["amount"].Value));

            result += (red * green * blue);
        }

        return result.ToString();
    }
}
