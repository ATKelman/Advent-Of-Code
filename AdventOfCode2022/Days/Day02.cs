using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2022.Days
{
    class Day02 : DayBase
    {
        public Day02(int day)
        : base(day)
        { }

        public override string SolvePart1()
        {
            int result = 0;
            File.ReadAllLines(_inputPath)
                .ToList()
                .ForEach(x =>
                {
                    var row = x.Split(' ');
                    var theirPick = row[0][0] - 64;
                    var myPick = row[1][0] - 87;
                    result += myPick;

                    if (theirPick == myPick)
                        result += 3;

                    if ((theirPick + 1) % 3 == (myPick + 3) % 3)
                        result += 6;
                });

            return result.ToString();
        }

        public override string SolvePart2()
        {
            return "":
        }
    }
}
