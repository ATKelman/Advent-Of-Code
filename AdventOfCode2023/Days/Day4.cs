using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2023.Days;
public class Day4 : DayBase
{
    public Day4(IConfiguration config, bool isTest)
        : base(config, 2023, 4, isTest)
    {
            
    }

    public override string SolvePart1()
    {
        var input = File.ReadAllLines(_inputPath);
        int points = 0;
        foreach (var line in input)
        {
            var i = line.Split(":", StringSplitOptions.RemoveEmptyEntries)[1];
            var j = i.Split("|").Select(x => x.Split(" ", StringSplitOptions.RemoveEmptyEntries).ToList().Select(int.Parse));

            var result = j.First().Intersect(j.Last());
            if (result.Any())
                points += (int)Math.Pow(2, result.Count() - 1);
        }

        return points.ToString();
    }

    public override string SolvePart2()
    {
        var input = File.ReadAllLines(_inputPath);
        var cards = Enumerable.Range(0, input.Count())
            .Select(x => new
            {
                Key = x,
                Value = 1
            })
            .ToDictionary(x => x.Key, x => x.Value);
        for (int i = 0; i < input.Length; i++)
        {
            var j = input[i].Split(":", StringSplitOptions.RemoveEmptyEntries)[1];
            var k = j.Split("|").Select(x => x.Split(" ", StringSplitOptions.RemoveEmptyEntries).ToList().Select(int.Parse));

            var result = k.First().Intersect(k.Last());
            if (result.Any())
            {
                var currentCardCount = cards[i];
                for (int copies = 1; copies <= result.Count(); copies++)
                {
                    var amount = cards[i + copies];
                    cards[i + copies] = amount + currentCardCount;
                }

            }
        }

        return cards.Values.Sum().ToString();
    }
}
