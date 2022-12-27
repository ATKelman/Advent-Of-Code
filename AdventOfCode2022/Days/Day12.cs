using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2022.Days
{
    internal class Day12 : DayBase
    {
        private Dictionary<(int x, int y), int> map;

        public Day12(int day)
        : base(day)
        {
            var input = File.ReadAllLines(_inputPath);

            int mapHeight = input.Length;
            int mapWidth = input[0].Length;

            // position - height
            map = new Dictionary<(int x, int y), int>();

            for (int x = 0; x < mapHeight; x++)
            {
                for (int y = 0; y < mapWidth; y++)
                {
                    char c = input[x][y];

                    int v = c switch
                    {
                        'E' => 27,
                        'S' => 0,
                        _ => c - 'a' + 1
                    };

                    map.Add((x, y), v);
                }
            }
        }

        (int x, int y)[] AdjacentDirections = new (int, int)[]
        {
            (-1, 0),
            (1, 0),
            (0, -1),
            (0, 1)
        };

        private int BFS((int x, int y) start, (int x, int y) end)
        {
            // position - depth
            var visited = new Dictionary<(int x, int y), int>();
            visited.Add(start, 0);

            var queue = new Queue<(int x, int y)>();
            queue.Enqueue(start);

            while (queue.Count > 0)
            {
                var instance = queue.Dequeue();
                if (instance == end)
                    break;

                var value = visited[instance];

                foreach (var (x, y) in AdjacentDirections)
                {
                    var neighbour = (instance.x + x, instance.y + y);

                    // Out of bounds or already visited
                    if (!map.ContainsKey(neighbour) || visited.ContainsKey(neighbour))
                        continue;

                    var moveCost = map[neighbour] - map[instance];

                    if (moveCost <= 1)
                    {
                        visited[neighbour] = value + 1;
                        queue.Enqueue(neighbour);
                    }
                }
            }

            visited.TryGetValue(end, out int steps);
            return steps;
        }

        public override string SolvePart1()
        {
            var start = map.OrderBy(x => x.Value).First();
            var end = map.OrderByDescending(x => x.Value).First();
         
            return "Steps taken: " + BFS(start.Key, end.Key);
        }

        public override string SolvePart2()
        {
            // S & a
            var starts = map.Where(x => x.Value <= 1).Select(x => x.Key);
            var end = map.OrderByDescending(x => x.Value).First();

            int least = int.MaxValue;
            foreach (var start in starts)
            {
                var instance = BFS(start, end.Key);
                if (instance < least && instance > 0)
                    least = instance;
            }

            return "Minimum steps: " + least;
        }
    }
}
