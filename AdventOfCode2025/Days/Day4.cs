using Microsoft.Extensions.Configuration;

namespace AdventOfCode2025.Days;

public class Day4 (IConfiguration config)
    : DayBase(config)
{
    public override string SolvePart1()
    {
        Dictionary<(int X, int Y), int> maps = [];

        for (int y = 0; y < InputLines.Length; y++)
        {
            string line = InputLines[y];
            for (int x = 0; x < line.Length; x++)
            {
                if (line[x] == '@')
                {
					maps.TryAdd((x, y), 0);
					for (int i = -1; i <= 1; i++)
                    {
                        for (int j = -1; j <= 1; j++)
                        {
                            if (i == j && i == 0) 
                                continue;

                            var dx = x + i;
                            var dy = y + j;

                            if (dx < 0 || dx >= line.Length
                                || dy < 0 || dy >= InputLines.Count())
                                continue;

                            if (InputLines[dy][dx] != '@')
                                continue;

                            maps[(dx, dy)] = maps.GetValueOrDefault((dx, dy)) + 1;
                        }
                    }
                }
            }
		}

        return maps.Where(x => x.Value < 4).Count().ToString();
    }

    public override string SolvePart2()
    {
        // TODO: Implement Part 2
        return "Not implemented";
    }

    private record Map(int X, int Y, char Value, int AdjacentPaper);
}
