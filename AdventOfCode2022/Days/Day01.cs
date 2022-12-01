using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq;

namespace AdventOfCode2022.Days
{
    public class Day01 : DayBase
    {
        public Day01(int day)
        : base(day)
        { }

        public override string SolvePart1()
        {
            var input = File
                .ReadAllText(_inputPath)
                .Split("\r\n\r\n")
                .Select(x => 
                    x.Split("\r\n").Select(int.Parse).Sum()
                 );
            return input.Max().ToString();
        }

        public override string SolvePart2()
        {
            var input = File
                .ReadAllText(_inputPath)
                .Split("\r\n\r\n")
                .Select(x =>
                    x.Split("\r\n").Select(int.Parse).Sum()
                 )
                .OrderByDescending(x => x)
                .Take(3)
                .Sum();
            return input.ToString();
        }
    }
}
