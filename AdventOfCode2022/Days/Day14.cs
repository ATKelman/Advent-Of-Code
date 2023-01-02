using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2022.Days
{
    public class Day14 : DayBase
    {
        private HashSet<(int x, int y)> walls;

        public Day14(int day)
            : base(day)
        {
            var rocks = File
                .ReadAllLines(_inputPath)
                .Select(x => x.Split(" -> ")
                    .Select(y => y.Split(',')
                        .Select(int.Parse)
                        .ToArray())
                    .Select(p => (x: p[0], y: p[1]))
                    .ToArray())
                .ToArray();

            walls = new();

            foreach (var rock in rocks)
            {
                for (int i = 1; i < rock.Length; i++)
                {
                    var wall = from X in Enumerable.Range(
                                    Math.Min(rock[i].x, rock[i - 1].x),
                                    Math.Abs(rock[i].x - rock[i - 1].x) + 1)
                               from Y in Enumerable.Range(
                                    Math.Min(rock[i].y, rock[i - 1].y),
                                    Math.Abs(rock[i].y - rock[i - 1].y) + 1)
                               select (X, Y);

                    foreach (var brick in wall)
                        walls.Add(brick);
                }
            }
        }

        public override string SolvePart1()
        {                
            HashSet<(int x, int y)> sand = new();
         
            var instance = (x: 500, y: 0); // Source

            var lowestHeight = walls.OrderByDescending(x => x.y).First().y;

            while (!sand.Contains((500, 0)) && instance.y <= lowestHeight)
            {
                if (!walls.Contains((instance.x, instance.y + 1)) && !sand.Contains((instance.x, instance.y + 1))) // Fall down
                    instance = (instance.x, instance.y + 1);
                else if (!walls.Contains((instance.x - 1, instance.y + 1)) && !sand.Contains((instance.x - 1, instance.y + 1))) // Fall Diagonally-LEFT down 
                    instance = (instance.x - 1, instance.y + 1);
                else if (!walls.Contains((instance.x + 1, instance.y + 1)) && !sand.Contains((instance.x + 1, instance.y + 1))) // Fall Diagonally-RIGHT down 
                    instance = (instance.x + 1, instance.y + 1);
                else
                {
                    sand.Add(instance);
                    instance = (x: 500, y: 0); // Next sand
                }
            }

            return sand.Count().ToString();
        }

        public override string SolvePart2()
        {
            HashSet<(int x, int y)> sand = new();

            var instance = (x: 500, y: 0); // Source

            var lowestHeight = walls.OrderByDescending(x => x.y).First().y + 1;

            while (!sand.Contains((500, 0)))
            {
                if (instance.y == lowestHeight)
                {
                    sand.Add(instance);
                    instance = (x: 500, y: 0); // Next sand
                }
                else if (!walls.Contains((instance.x, instance.y + 1)) && !sand.Contains((instance.x, instance.y + 1))) // Fall down
                    instance = (instance.x, instance.y + 1);
                else if (!walls.Contains((instance.x - 1, instance.y + 1)) && !sand.Contains((instance.x - 1, instance.y + 1))) // Fall Diagonally-LEFT down 
                    instance = (instance.x - 1, instance.y + 1);
                else if (!walls.Contains((instance.x + 1, instance.y + 1)) && !sand.Contains((instance.x + 1, instance.y + 1))) // Fall Diagonally-RIGHT down 
                    instance = (instance.x + 1, instance.y + 1);
                else
                {
                    sand.Add(instance);
                    instance = (x: 500, y: 0); // Next sand
                }
            }

            return sand.Count().ToString();
        }
    }
}
