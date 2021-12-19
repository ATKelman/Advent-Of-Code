using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AdventOfCode2021.Days
{
    public class Day13 : DayBase
    {
        public Day13(int day)
            : base(day)
        { }

        public override string SolvePart1()
        {
            var input = File
                .ReadAllLines(_inputPath);
            var dots = input
                .Where(x => x.Contains(","))
                .Select(x =>
                    x.Split(",")
                    .ToArray())
                .Select(x => new
                {
                    X = int.Parse(x[0]),
                    Y = int.Parse(x[1])
                })
                .ToList();

            var instructions = input
                .Where(x => x.Contains("fold"))
                .Select(x => x.Replace("fold along ", ""))
                .Select(x => x.Split('=', StringSplitOptions.RemoveEmptyEntries))
                .Select(x => new
                {
                    X = x[0] == "x" ? int.Parse(x[1]) : 0,
                    Y = x[0] == "y" ? int.Parse(x[1]) : 0
                })
                .ToList();
  
            return instructions
                .Take(1)
                .SelectMany(x =>
                    dots.Select(d => new
                    {
                        X = Transform(x.X, d.X),
                        Y = Transform(x.Y, d.Y)
                    }))
                .Distinct()
                .Count()
                .ToString();
        }

        public override string SolvePart2()
        {
            var input = File
                .ReadAllLines(_inputPath);
            var dots = input
                .Where(x => x.Contains(","))
                .Select(x =>
                    x.Split(",")
                    .ToArray())
                .Select(x => new
                {
                    X = int.Parse(x[0]),
                    Y = int.Parse(x[1])
                })
                .ToList();

            var instructions = input
                .Where(x => x.Contains("fold"))
                .Select(x => x.Replace("fold along ", ""))
                .Select(x => x.Split('=', StringSplitOptions.RemoveEmptyEntries))
                .Select(x => new
                {
                    X = x[0] == "x" ? int.Parse(x[1]) : 0,
                    Y = x[0] == "y" ? int.Parse(x[1]) : 0
                })
                .ToList();

            var code = instructions
                .Aggregate(dots, (d, f) =>
                    d.ToList()
                    .Select(x => new
                    {
                        X = Transform(f.X, x.X),
                        Y = Transform(f.Y, x.Y)
                    })
                    .ToList())
                .Distinct()
                .OrderBy(x => x.X)
                .ThenBy(y => y.Y)
                .ToList();

            var rows =
                Enumerable.Range(0, code.Max(p => p.Y) + 1)
                    .Select(y =>
                        Enumerable.Range(0, code.Max(p => p.X) + 1)
                            .Select(x => code.Any(z => z.X == x && z.Y == y) ? "#" : " ")
                            .Aggregate(string.Empty, (concat, x) => $"{concat}{x}"))
                    .ToList();
            return string.Join("\r\n", rows);
        }

        private int Transform(int fold, int pos) => fold > 0 && pos >= fold ? fold - (pos - fold) : pos;
    }
}
