using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2021.Days
{
    public class Day09 : DayBase
    {
        public Day09(int day)
            : base(day)
        { }

        public override string SolvePart1()
        {
            var input = File
                .ReadAllLines(_inputPath)
                .Select(x => x
                    .ToArray()
                    .Select(y => int.Parse(y.ToString()))
                    .ToArray())
                .ToArray();

            var width = input[0].Length;
            var height = input.Length;

            var points = from x in Enumerable.Range(0, width)
                         from y in Enumerable.Range(0, height)
                         let instance = (x, y)
                         where GetNeighbours(instance, width, height).All(n => input[y][x] < input[n.y][n.x])
                         select instance;

            return points
                .Sum(p => input[p.y][p.x] + 1)
                .ToString();
        }

        public override string SolvePart2()
        {
            var input = File
                .ReadAllLines(_inputPath)
                .Select(x => x
                    .ToArray()
                    .Select(y => int.Parse(y.ToString()))
                    .ToArray())
                .ToArray();

            var width = input[0].Length;
            var height = input.Length;

            var visited = new bool[width, height];

            var points = from x in Enumerable.Range(0, width)
                         from y in Enumerable.Range(0, height)
                         let instance = (x, y)
                         where GetNeighbours(instance, width, height).All(n => input[y][x] < input[n.y][n.x])
                         select instance;

            var largest = points.Select(x => GetSize(x, visited, input, width, height))
                .OrderByDescending(x => x)
                .Take(3);

            return largest
                .Aggregate(1, (size, acc) => acc * size)
                .ToString();
        }

        private IEnumerable<(int x, int y)> GetNeighbours((int x, int y) i, int width, int height)
        {
            return from z in new (int x, int y)[] { (i.x - 1, i.y), (i.x + 1, i.y), (i.x, i.y - 1), (i.x, i.y + 1) }
                   where z.x >= 0 && z.x < width && z.y >= 0 && z.y < height
                   select z;
        }

        private int GetSize((int x, int y) i, bool[,] visited, int[][] input, int width, int height)
        {
            visited[i.x, i.y] = true;
            var depth = input[i.y][i.x];
            return 1 + GetNeighbours(i, width, height)
                .Where(n => !visited[n.x, n.y] && input[n.y][n.x] < 9)
                .Sum(n => GetSize(n, visited, input, width, height));
        }
    }
}
