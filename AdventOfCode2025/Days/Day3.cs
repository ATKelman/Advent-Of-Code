using Microsoft.Extensions.Configuration;

namespace AdventOfCode2025.Days;

public class Day3 (IConfiguration config)
    : DayBase(config)
{
    public override string SolvePart1()
    {
        var maxJoltage = InputLines
            .Select(bank =>
            {
                var firstIndex = Enumerable.Range(0, bank.Length - 1)
                    .OrderByDescending(x => bank[x])
                    .First();

                var second = bank.Skip(firstIndex + 1)
                    .Max();

                return int.Parse($"{bank[firstIndex]}{second}");
            })
            .Sum();
      
		return maxJoltage.ToString();
    }

    public override string SolvePart2()
    {
		var maxJoltage = InputLines
			.Select(bank =>
			{
                var selected = new List<char>();
                int remaining = 12;
                int startIndex = 0;

                while (remaining > 0)
                {
                    int searchEnd = bank.Length - remaining + 1;
                    var best = bank.Skip(startIndex)
                                    .Take(searchEnd - startIndex)
                                    .Select((c, i) => (value: c, index: i + startIndex))
                                    .MaxBy(x => x.value);
                    selected.Add(best.value);
                    startIndex = best.index + 1;
                    remaining--;
				}

				return long.Parse(string.Concat(selected));
			})
			.Sum();

		return maxJoltage.ToString();
	}
}
