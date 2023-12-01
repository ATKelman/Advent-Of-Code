using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AdventOfCode2023.Days;
public class Day1 : DayBase
{
    public Day1(IConfiguration config, bool isTest)
        : base(config, 2023, 1, isTest)
    { }

    public override string SolvePart1()
    {
        var input = File
                .ReadAllLines(_inputPath);

        string pattern = @"\d";
        Regex rx = new Regex(pattern);

        List<int> sums = new();
        foreach (var line in input)
        {
            var matches = rx.Matches(line);
            sums.Add(int.Parse(matches.First().ToString() + matches.Last().ToString()));
        }

        return sums.Sum().ToString();
    }

    public override string SolvePart2()
    {
        var input = File
                .ReadAllLines(_inputPath);

        string pattern = @"\d|one|two|three|four|five|six|seven|eight|nine";
        Regex rx = new Regex(pattern);

        List<int> sums = new();
        foreach (var line in input)
        {
            var matches = rx.Matches(line);
            sums.Add(int.Parse(matches.First().ToString() + matches.Last().ToString()));
        }

        return sums.Sum().ToString();
    }
}
