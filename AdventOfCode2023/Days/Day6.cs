using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AdventOfCode2023.Days;
public class Day6 : DayBase
{
    public Day6(IConfiguration config, bool isTest)
        : base(config, 2023, 6, isTest)
    {

    }

    public override string SolvePart1()
    {
        Regex regex = new Regex(@"\d+");
        var input = File.ReadAllLines(_inputPath)
            .Select(x =>
                regex.Matches(x)
                .Select(y => int.Parse(y.Value)).ToArray())
            .ToArray();

        long winningOptions = 1;

        for (int i = 0; i < input[0].Count(); i++)
        {
            var time = input[0][i];
            var record = input[1][i];

            var lower = (-time - Math.Sqrt(time * time - 4 * record)) / 2;
            var h1 = (long)Math.Ceiling(lower);
            if (h1 == lower)
                h1++;

            var upper = (-time + Math.Sqrt(time * time - 4 * record)) / 2;
            var h2 = (long)Math.Floor(upper);
            if (h2 == upper)
                h2--;

            winningOptions *= (h2 - h1 + 1);
        }

        return winningOptions.ToString();
    }

    public override string SolvePart2()
    {
        Regex regex = new Regex(@"\d+");
        var input = File.ReadAllLines(_inputPath)
            .Select(x =>
                regex.Matches(x)
                .Aggregate("", (tot, val) => $"{tot}{val.Value}"))
            .ToArray();

        var time = long.Parse(input[0]);
        var record = long.Parse(input[1]);

        var lower = (-time - Math.Sqrt(time * time - 4 * record)) / 2;
        var upper = (-time + Math.Sqrt(time * time - 4 * record)) / 2;

        var h1 = (long)Math.Ceiling(lower);
        if (h1 == lower)
            h1++;

            
        var h2 = (long)Math.Floor(upper);
        if (h2 == upper)
            h2--;

        long winningOptions = (h2 - h1 + 1);

        return winningOptions.ToString();
    }
}
