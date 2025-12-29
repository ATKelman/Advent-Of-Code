using Microsoft.Extensions.Configuration;
using System.Drawing;

namespace AdventOfCode2025.Days;

public class Day9(IConfiguration config)
	: DayBase(config)
{
	public override string SolvePart1()
	{
		var points = InputLines
			.Select(x => x.Split(",").Select(long.Parse).ToArray())
			.Select(x => new Point(x[0], x[1]))
			.ToArray();

		long largestArea = 0;
		for (int i = 0; i < points.Length; i++)
		{
			for (int j = i + 1; j < points.Length; j++)
			{
				var area = CalculateArea(points[i], points[j]);
				if (area > largestArea)
					largestArea = area;
			}
		}
		return largestArea.ToString();
	}

	public override string SolvePart2()
	{
		var redTiles = InputLines
			.Select(x => x.Split(",").Select(long.Parse).ToArray())
			.Select(x => new Point(x[0], x[1]))
			.ToArray();

		HashSet<Point> redSet = [.. redTiles];
		HashSet<Point> greenTiles = [.. redSet];

		for (int i = 0; i < redTiles.Length; i++)
		{
			var start = redTiles[i];
			var end = redTiles[(i + 1) % redTiles.Length];
			foreach (var point in GetPointsBetween(start, end))
				greenTiles.Add(point);
		}

		var yRanges = greenTiles
			.GroupBy(p => p.Y)
			.ToDictionary(
				g => g.Key,
				g => (min: g.Min(p => p.X), max: g.Max(p => p.X))
			);

		long maxArea = 0;

		for (int i = 0; i < redTiles.Length; i++)
		{
			for (int j = i + 1; j < redTiles.Length; j++)
			{
				var corner1 = redTiles[i];
				var corner2 = redTiles[j];

				long minX = Math.Min(corner1.X, corner2.X);
				long maxX = Math.Max(corner1.X, corner2.X);
				long minY = Math.Min(corner1.Y, corner2.Y);
				long maxY = Math.Max(corner1.Y, corner2.Y);

				bool valid = true;

				foreach (var (y, (xMin, xMax)) in yRanges)
				{
					if (y < minY || y > maxY) continue;

					if (minX < xMin || maxX > xMax)
					{
						valid = false;
						break;
					}
				}

				if (valid)
				{
					long area = CalculateArea(corner1, corner2);
					maxArea = Math.Max(maxArea, area);
				}
			}
		}

		return maxArea.ToString();
	}

	private static long CalculateArea(Point a, Point b)
	{
		return (Math.Abs(a.X - b.X) + 1) * (Math.Abs(a.Y - b.Y) + 1);
	}

	private static IEnumerable<Point> GetPointsBetween(Point a, Point b)
	{
		if (a.X == b.X)
		{
			long startY = Math.Min(a.Y, b.Y);
			long endY = Math.Max(a.Y, b.Y);
			for (long y = startY; y <= endY; y++)
				yield return new Point(a.X, y);
		}
		else if (a.Y == b.Y)
		{
			long startX = Math.Min(a.X, b.X);
			long endX = Math.Max(a.X, b.X);
			for (long x = startX; x <= endX; x++)
				yield return new Point(x, a.Y);
		}
		else
		{
			throw new InvalidOperationException("Red tiles must be on same row or column");
		}
	}

	private record Point(long X, long Y);
}
