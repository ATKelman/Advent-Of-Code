using Microsoft.Extensions.Configuration;

namespace AdventOfCode2025.Days;

public class Day5 (IConfiguration config)
    : DayBase(config)
{
    public override string SolvePart1()
    {
        var input = InputText.Split("\n\n", StringSplitOptions.RemoveEmptyEntries);
        var ranges = input[0].Split('\n')
            .Select(line =>
            {
                var parts = line
                .Split("-")
                .Select(long.Parse)
                .ToArray();
                return (Start: parts[0], End: parts[1]);
            })
            .ToArray();

        var ids = input[1]
            .Split("\n", StringSplitOptions.RemoveEmptyEntries)
            .Select(long.Parse)
            .ToArray();

        int freshIngredients = 0;
        foreach (var id in ids)
        {
            if (ranges.Any(x => x.Start <= id && id <= x.End))
                freshIngredients++;
        }

        return freshIngredients.ToString();
    }

    public override string SolvePart2()
    {
        var input = InputText.Split("\n\n");
		var ranges = input[0].Split('\n')
			.Select(line =>
			{
				var parts = line
				.Split("-")
				.Select(long.Parse)
				.ToArray();
				return (Start: parts[0], End: parts[1]);
			})
			.ToArray();

        List<(long Start, long End)> freshIngredients = [];

        foreach (var range in ranges)
        {
            var overlap = freshIngredients.Where(x => RangesOverlap(range, x)).ToList();
            if (!overlap.Any())
            {
                freshIngredients.Add(range);
                continue;
            }

            var newRange = range;
            for (int i = 0; i < overlap.Count; i++)
            {
                var instance = overlap[i];
                freshIngredients.Remove(instance);

				newRange.Start = Math.Min(newRange.Start, instance.Start);
				newRange.End = Math.Max(newRange.End, instance.End);
			}
            freshIngredients.Add(newRange);
		}

        return freshIngredients.Sum(x => x.End - x.Start + 1).ToString();
    }

    private static bool RangesOverlap((long start, long end) first, (long start, long end) second)
    {
        return first.start <= second.end && second.start <= first.end;
    }
}
