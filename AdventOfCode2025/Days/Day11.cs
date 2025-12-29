using Microsoft.Extensions.Configuration;

namespace AdventOfCode2025.Days;

public class Day11 (IConfiguration config)
    : DayBase(config)
{
    private Dictionary<string, string[]> _mappings = [];

    public override string SolvePart1()
    {
		_mappings = InputLines
			.Select(x => x.Split(' '))
			.ToDictionary(
				x => x[0][..^1],
				x => x[1..]);

		var result = FindPathsBetween("you", "out", []);

		return result.ToString();
    }

    public override string SolvePart2()
    {
		_mappings = InputLines
			.Select(x => x.Split(' '))
			.ToDictionary(
				x => x[0][..^1],
				x => x[1..]);

		var svrToFft = FindPathsBetween("svr", "fft", []);
		var svrToDac = FindPathsBetween("svr", "dac", []);
		var fftToDac = FindPathsBetween("fft", "dac", []);
		var dacToFft = FindPathsBetween("dac", "fft", []);
		var fftToOut = FindPathsBetween("fft", "out", []);
		var dacToOut = FindPathsBetween("dac", "out", []);

		var paths = (svrToFft * fftToDac * dacToOut) +
			(svrToDac * dacToFft * fftToOut);
		return paths.ToString();
	}

	private long FindPathsBetween(string key, string destination, Dictionary<string, long> cache)
	{
		long result = 0;
		if (!_mappings.TryGetValue(key, out string[] paths))
			return result;

		foreach (var path in paths)
		{
			if (path == destination)
			{
				result++;
				continue;
			}

			if (cache.TryGetValue(path, out long value))
				result += value;
			else
				result += FindPathsBetween(path, destination, cache);
		}
		cache[key] = result;
		return result;
	}

	private record Device(string Key, int PathsOut);
}
