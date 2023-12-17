using Microsoft.Extensions.Configuration;

namespace AdventOfCode2023.Days;
public class Day10 : DayBase
{
    public Day10(IConfiguration config, bool isTest)
        : base(config, 2023, 10, isTest)
    {
        
    }

    public override string SolvePart1()
    {
        var input = File.ReadAllLines(_inputPath)
            .Select(x => x.ToCharArray())
            .ToList();

        (int x, int y) start = (0, 0);
        var grid = new char[input.Count(), input[0].Count()];
        for (int y = 0; y < input.Count(); y++)
            for (int x = 0; x < input[y].Count(); x++)
            {
                grid[x, y] = input[y][x];
                if (grid[x, y] == 'S')
                    start = (x, y);
            }

        // Find starting point pipe or just one that connects to the starting point 
        List<(int x, int y)> passed = new() { (start.x, start.y) };
        Queue<(int x, int y)> queued = new();
        for (int x = -1; x <= 1; x++)
            for (int y = -1; y <= 1; y++)
            {
                if (x == 0 && y == 0)
                    continue;
                var dx = x + start.x;
                var dy = y + start.y;

                if (dx < 0 || dx >= input.Count() || dy < 0 || dy >= input.Count()) continue;
                if (grid[dx, dy] == '.') continue;

                var connections = GetConnections(grid[dx, dy], dx, dy);
                if (connections.first == start || connections.second == start)
                {
                    queued.Enqueue((dx, dy));
                }
            }

        // find loop length 
        while (queued.Count() > 0)
        {
            var current = queued.Dequeue();
            if (passed.Contains(current)) continue;

            passed.Add(current);

            var connections = GetConnections(grid[current.x, current.y], current.x, current.y);
            if (!passed.Contains(connections.first))
                queued.Enqueue(connections.first);
            if (!passed.Contains(connections.second))
                queued.Enqueue(connections.second);
        }

        return (passed.Count() / 2).ToString();
    }

    public override string SolvePart2()
    {
        throw new NotImplementedException();
    }

    private ((int x, int y) first, (int x, int y) second) GetConnections(char pipe, int x, int y)
    {
        return pipe switch
        {
            '|' => ((x, y - 1), (x, y + 1)),
            '-' => ((x - 1, y), (x + 1, y)),
            'L' => ((x, y - 1), (x + 1, y)),
            'J' => ((x - 1, y), (x, y - 1)),
            '7' => ((x - 1, y), (x, y + 1)),
            'F' => ((x + 1, y), (x, y + 1)),
            _ => throw new Exception("Outside loop")
        };
    }
}
