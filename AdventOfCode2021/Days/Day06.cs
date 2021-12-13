using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2021.Days
{
    public class Day06 : DayBase
    {
        public Day06(int day)
            : base(day)
        { }

        public override string SolvePart1()
        {
            var input = File.ReadAllText(_inputPath)
                .Split(",")
                .Select(int.Parse)
                .ToList();

            for (int i = 0; i < 80; i++)
            {
                var lanternfish = input.ToList();
                for (int j = 0; j < input.Count; j++)
                {
                    if (lanternfish[j] == 0)
                    {
                        lanternfish[j] = 6;
                        lanternfish.Add(8);
                    }
                    else
                        lanternfish[j]--;
                }
                input = lanternfish;
            }

            return input.Count.ToString();
        }

        public override string SolvePart2()
        {
            return "";
        }
    }
}
