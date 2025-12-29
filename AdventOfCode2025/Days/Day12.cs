using Microsoft.Extensions.Configuration;

namespace AdventOfCode2025.Days;

public class Day12 (IConfiguration config)
    : DayBase(config)
{
    public override string SolvePart1()
    {
        var input = InputText.Split("\n\n", StringSplitOptions.RemoveEmptyEntries);
        var shapes = input[..^1].Select(x =>
            {
                var lines = x.Split('\n', StringSplitOptions.RemoveEmptyEntries)[1..];
                return new Shape(lines);
		    })
            .ToArray();
        var regions = input[^1]
            .Split('\n', StringSplitOptions.RemoveEmptyEntries)
            .Select(x =>
            {
                var parts = x.Split(' ', StringSplitOptions.RemoveEmptyEntries);
                var size = parts[0][..^1].Split('x').Select(int.Parse).ToArray();
				var shapes = parts[1..].Select(int.Parse).ToArray();

                return new Region(size[0], size[1], size[0] * size[1], shapes);
			});

        int validRegions = 0;
        foreach (var region in regions)
        {
            long weightedSum = 0;
            for (int i = 0; i < region.Shapes.Length; i++)
                weightedSum += region.Shapes[i] * shapes[i].Area;

            if (weightedSum > region.Area)
                continue;

            var shapesCount = region.Shapes.Sum();
			if ((region.Width / 3) * (region.Height / 3) >= shapesCount)
            {
                validRegions++;
                continue;
            }
        }

		return validRegions.ToString();
    }

    public override string SolvePart2()
    {
        return "There was no part 2";
    }

    private class Shape(string[] lines)
	{
		public string[] Lines { get; set; } = lines;
		public int Area { get; set; } = lines.Sum(line => line.Count(c => c == '#'));
	}

    private record Region(int Width, int Height, long Area, int[] Shapes);
}
