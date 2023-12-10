using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2023.Days;
public class Day9 : DayBase
{
    public Day9(IConfiguration config, bool isTest)
        : base(config, 2023, 9, isTest)
    {

    }

    public override string SolvePart1()
    {
        var input = File.ReadAllLines(_inputPath)
            .Select(x => x.Split(" ", StringSplitOptions.RemoveEmptyEntries).Select(long.Parse).ToList())
            .Select(GetHistory)
            .ToList()
            .Select(x => x.Last())
            .Sum();

        return input.ToString();
    }

    private List<long> GetHistory(List<long> sequence)
    {
        if (sequence.All(x => x == 0))
            return sequence;
        List<long> newPairs = new();
        for (var i = 0; i < sequence.Count() - 1; i++)
            newPairs.Add(sequence[i + 1] - sequence[i]);

        var nextSequence = GetHistory(newPairs);
        sequence.Add(nextSequence.Last() + sequence.Last());
        return sequence;
    }

    public override string SolvePart2()
    {
        var input = File.ReadAllLines(_inputPath)
            .Select(x => x.Split(" ", StringSplitOptions.RemoveEmptyEntries).Select(long.Parse).Reverse().ToList())
            .Select(GetHistory)
            .ToList()
            .Select(x => x.Last())
            .Sum();

        return input.ToString();
    }
}
