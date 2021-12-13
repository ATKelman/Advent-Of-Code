using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2021.Days
{
    public class Day07 : DayBase
    {
        public Day07(int day)
            : base(day)
        { }

        public override string SolvePart1()
        {
            var input = File
                .ReadAllText(_inputPath)
                .Split(",")
                .Select(int.Parse)
                .ToArray();

            var fuelCosts = new List<(int pos, int cost)>();
            for (int i = 0; i < input.Length; i++)
            {
                int fuel = 0;
                for (int j = 0; j < input.Length; j++)
                {
                    if (i == j) continue;

                    fuel += Math.Abs(input[i] - input[j]);
                }
                fuelCosts.Add((input[i], fuel));
            }

            return fuelCosts.OrderBy(x => x.cost).First().cost.ToString();
        }

        public override string SolvePart2()
        {
            var input = File
                .ReadAllText(_inputPath)
                .Split(",")
                .Select(int.Parse)
                .ToArray();

            var fuelCosts = new List<(int pos, int cost)>();
            for (int i = 0; i < input.Length; i++)
            {
                int fuel = 0;
                for (int j = 0; j < input.Length; j++)
                {
                    if (i == j) continue;

                    fuel += AddLower(Math.Abs(input[i] - input[j]));
                }
                fuelCosts.Add((input[i], fuel));
            }

            return fuelCosts.OrderBy(x => x.cost).First().cost.ToString();
        }

        private int AddLower(int value)
        {
            if (value <= 1) return 1;
            return (value) + AddLower(value - 1);
        }
    }
}
