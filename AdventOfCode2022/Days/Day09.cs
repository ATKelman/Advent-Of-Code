using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2022.Days
{
    internal class Day09 : DayBase
    {
        public Day09(int day)
        : base(day)
        { }

        public override string SolvePart1()
        {
            var input = File.ReadAllLines(_inputPath);

            (int x, int y) posTail = (0, 0);
            (int x, int y) posHead = (0, 0);
            HashSet<(int x, int y)> visitedPositions = new() { posTail };

            foreach (var instruction in input)
            {
                var values = instruction.Split(' ');
                int steps = int.Parse(values[1]);

                for (int i = 0; i < steps; i++)
                {
                    switch (values[0])
                    {
                        case "R": posHead.x++; break;
                        case "L": posHead.x--; break;
                        case "U": posHead.y++; break;
                        case "D": posHead.y--; break;
                    };

                    int dx = posHead.x - posTail.x;
                    int dy = posHead.y - posTail.y;

                    if (Math.Abs(dx) > 1 || Math.Abs(dy) > 1)
                    {
                        posTail.x += Math.Sign(dx);
                        posTail.y += Math.Sign(dy);
                    }

                    visitedPositions.Add(posTail);
                }
            }

            return visitedPositions.Count.ToString();
        }

        public override string SolvePart2()
        {
            var input = File.ReadAllLines(_inputPath);

            (int x, int y)[] knots = new(int x, int y)[10];
            HashSet<(int x, int y)> visitedPositions = new();

            foreach (var instruction in input)
            {
                var values = instruction.Split(' ');
                int steps = int.Parse(values[1]);

                for (int i = 0; i < steps; i++)
                {
                    switch (values[0])
                    {
                        case "R": knots[0].x++; break;
                        case "L": knots[0].x--; break;
                        case "U": knots[0].y++; break;
                        case "D": knots[0].y--; break;
                    };

                    for (int j = 1; j < 10; j++)
                    {
                        int dx = knots[j - 1].x - knots[j].x;
                        int dy = knots[j - 1].y - knots[j].y;

                        if (Math.Abs(dx) > 1 || Math.Abs(dy) > 1)
                        {
                            knots[j].x += Math.Sign(dx);
                            knots[j].y += Math.Sign(dy);
                        }
                    }

                    visitedPositions.Add(knots[9]);
                }
            }

            return visitedPositions.Count.ToString();
        }
    }
}
