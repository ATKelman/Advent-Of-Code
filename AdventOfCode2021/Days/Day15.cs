using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2021.Days
{
    public class Day15 : DayBase
    {
        public Day15(int day)
            : base(day)
        { }

        

        public override string SolvePart1()
        {
            var input = File
                .ReadAllLines(_inputPath);

            return Solve(input).ToString();
        }

        private int Solve(string[] input, bool enlarge = false)
        {
            var dim = enlarge ? 5 : 1;
            var yMax = input.Length * dim - 1;
            var xMax = input[0].Length *  dim - 1;

            var shortestPath = new List<Path>() { new Path((0, 0)) };
            var visited = new Dictionary<(int x, int y), int> { { (0, 0), 0 } };

            do
            {
                var newPaths = new List<Path>();
                foreach (var path in shortestPath)
                {
                    var (x, y) = path.Last;
                    if ((x, y) == (xMax, yMax))
                    {
                        newPaths.Add(path);
                        continue;
                    }

                    for (int i = 0; i < 4; i++)
                    {
                        var dx = x + i switch { 0 => 1, 2 => -1, _ => 0 };
                        var dy = y + i switch { 1 => -1, 3 => 1, _ => 0 };
                        if (dx < 0 || dx > xMax || dy < 0 || dy > yMax) continue;
                        var pos = (dx, dy);
                        var riskLevel = RiskLevel(dx, dy);

                        if (!visited.TryGetValue(pos, out var visitedRiskLevel))
                        {
                            var newPath = new Path(path, pos, riskLevel);
                            newPaths.Add(newPath);
                            visited[pos] = newPath.Risk;
                        }
                        else if ((path.Risk + riskLevel) < visitedRiskLevel)
                        {
                            var newPath = new Path(path, pos, riskLevel);
                            newPaths.Add(newPath);
                            visited[pos] = newPath.Risk;
                        }
                    }
                }
                shortestPath = newPaths.GroupBy(x => x.Last, (key, g) => g.OrderBy(x => x.Risk).First()).ToList();
            } while (shortestPath.Count > 1);

            return shortestPath[0].Risk;

            int RiskLevel(int dx, int dy)
            {
                var xdim = dx / input[0].Length;
                var ydim = dy / input.Length;
                var vx = input[dy % input.Length][dx % input[0].Length] - '0';

                return ((vx - 1 + ydim + xdim) % 9) + 1;
            }
        }

        public override string SolvePart2()
        {
            var input = File
                .ReadAllLines(_inputPath);

            return Solve(input, true).ToString();
        }

    public class Path
    {
        public (int x, int y) Last { get; set; }
        public int Risk { get; set; }

        public Path((int x, int y) last)
        {
            Last = last;
            Risk = 0;
        }

        public Path(Path source, (int x, int y) last, int addRisk)
        {
            Last = last;
            Risk = source.Risk + addRisk;
        }
    }
}
