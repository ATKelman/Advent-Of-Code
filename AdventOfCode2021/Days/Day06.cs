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
            long sum = 0;
            File.ReadAllText(_inputPath)
               .Split(",")
               .Select(int.Parse)
               .ToList()
               .GroupBy(x => x)
               .Select(x=> new { value = x.Key, count = x.Count() })
               .ToList()
               .ForEach(x => 
               {
                   sum += (x.count * Solve(x.value, 256));
               }); 

            return sum.ToString();
        }

        private long Solve(int current, int days)
        {
            if ((days - current) <= 0)
                return 0;

            return 1 + Solve(6, days - current - 1) + Solve(8, days - current - 1);
        }
    }
}
