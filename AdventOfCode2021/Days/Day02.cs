using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2021.Days
{
    public class Day02 : DayBase
    {
        public Day02(int day)
            : base(day)
        { }

        public override string SolvePart1()
        {
            int depth = 0;
            int horizontal = 0;

            var instructions = File
                .ReadAllLines(_inputPath)
                .ToList();

            for (int i = 0; i < instructions.Count; i++)
            {
                var instance = instructions[i].Split(" ");

                if (instructions[i].ToUpper().StartsWith("F"))
                {
                    horizontal += int.Parse(instance[1]);
                }
                else if (instructions[i].ToUpper().StartsWith("U"))
                {
                    depth -= int.Parse(instance[1]);
                }
                else
                {
                    depth += int.Parse(instance[1]);
                }
            }

            return (depth * horizontal).ToString();
        }

        public override string SolvePart2()
        {
            int depth = 0;
            int horizontal = 0;
            int aim = 0;

            var instructions = File
                .ReadAllLines(_inputPath)
                .ToList();

            for (int i = 0; i < instructions.Count; i++)
            {
                var instance = instructions[i].Split(" ");

                if (instructions[i].ToUpper().StartsWith("F"))
                {
                    horizontal += int.Parse(instance[1]);
                    depth += int.Parse(instance[1]) * aim;
                }
                else if (instructions[i].ToUpper().StartsWith("U"))
                {
                    aim -= int.Parse(instance[1]);
                }
                else
                {
                    aim += int.Parse(instance[1]);
                }
            }

            return (depth * horizontal).ToString();
        }
    }
}
