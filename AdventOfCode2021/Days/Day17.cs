using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2021.Days
{
    public class Day17 : DayBase
    {
        public Day17(int day)
            : base(day)
        { }

        public override string SolvePart1()
        {
            var input = File
                .ReadAllText(_inputPath)
                .Replace("target area: x=", "")
                .Replace(", y=", "..")
                .Split("..", StringSplitOptions.RemoveEmptyEntries)
                .Select(x => int.Parse(x))
                .ToArray();

            var n = input[2] * -1 - 1;
            return (n * (n+1) / 2).ToString();
        }

        public override string SolvePart2()
        {
            var input = File
                .ReadAllText(_inputPath)
                .Replace("target area: x=", "")
                .Replace(", y=", "..")
                .Split("..", StringSplitOptions.RemoveEmptyEntries)
                .Select(x => int.Parse(x))
                .ToArray();

            List<(int xvel, int yvel)> validVelocities = new();

            for (int x = 0; x <= input[1]; x++)
            {
                for (int y = Math.Abs(input[2]); y >= input[2]; y--)
                {
                    (int velX, int velY) velocity = (x, y);
                    (int posX, int posY) position = (0, 0);
                    
                    while (position.posX <= input[1] && position.posY >= input[2])
                    {
                        position.posX += velocity.velX;
                        position.posY += velocity.velY;

                        if (velocity.velX > 0)
                            velocity.velX--;

                        velocity.velY--;

                        if (position.posX >= input[0] && position.posX <= input[1]
                            && position.posY >= input[2] && position.posY <= input[3])
                        {
                            validVelocities.Add((x, y));
                            break;
                        }

                    }
                }
            }

            return validVelocities.Count.ToString();
        }
    }
}
