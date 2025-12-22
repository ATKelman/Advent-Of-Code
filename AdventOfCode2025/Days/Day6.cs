using Microsoft.Extensions.Configuration;
using System.Reflection.Metadata.Ecma335;

namespace AdventOfCode2025.Days;

public class Day6 (IConfiguration config)
    : DayBase(config)
{
    public override string SolvePart1()
    {
        var input = InputText.Split("\n", StringSplitOptions.RemoveEmptyEntries)
            .Select(line => line.Split(" ", StringSplitOptions.RemoveEmptyEntries))
            .ToArray();

        var values = input[..^1].Select(x => x.Select(long.Parse).ToList()).ToList();
		var operators = input[^1];

        var transposed = Enumerable.Range(0, values[0].Count)
            .Select(col => values.Select(row => row[col]).ToList())
            .ToList();

        long total = 0;
        for (int i = 0; i <= operators.Length - 1; i++)
        {
            total += transposed[i].Aggregate((x, y) =>
            {
                return operators[i] switch
                {
                    "+" => x + y,
                    "*" => x * y,
                    _ => throw new AggregateException()
                };
            });
        }

		return total.ToString();
    }

    public override string SolvePart2()
    {
		var input = InputText.Split("\n", StringSplitOptions.RemoveEmptyEntries)		
			.ToArray();

		var values = input[..^1];
		var operators = input[^1].Split(" ", StringSplitOptions.RemoveEmptyEntries);

        var transposed = Enumerable.Range(0, values[0].Length)
            .Select(col => new string(values.Select(line => line[col]).ToArray()))
            .ToList()
            .Aggregate(new List<List<long>> { new() },
            (acc, item) =>
            {
                if (string.IsNullOrWhiteSpace(item))
                    acc.Add([]);
                else
                    acc[^1].Add(long.Parse(item));
                return acc;
            });

		long total = 0;
		for (int i = 0; i <= operators.Length - 1; i++)
		{
			total += transposed[i].Aggregate((x, y) =>
			{
				return operators[i] switch
				{
					"+" => x + y,
					"*" => x * y,
					_ => throw new AggregateException()
				};
			});
		}

		return total.ToString();
	}
}
