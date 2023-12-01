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
    private string[] input;

    public Day1(IConfiguration config, bool isTest)
        : base(config, 2023, 1, isTest)
    {
        input = File
            .ReadAllLines(_inputPath);
    }

    public override string SolvePart1()
    {
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
        string pattern = @"(?=(\d|one|two|three|four|five|six|seven|eight|nine))";
        Regex rx = new Regex(pattern);

        List<int> sums = new();
        foreach (var line in input)
        {
            var matches = rx.Matches(line);
            sums.Add(int.Parse(GetNumber(matches.First().Groups[1].ToString()) + GetNumber(matches.Last().Groups[1].ToString())));
        }

        return sums.Sum().ToString();
    }

    private static string GetNumber(string number)
    {
        return number switch
        {
            "one" => "1",
            "two" => "2",
            "three" => "3",
            "four" => "4",
            "five" => "5",
            "six" => "6",
            "seven" => "7",
            "eight" => "8",
            "nine" => "9",
            _ => number
        };
    }
}
