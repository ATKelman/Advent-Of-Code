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
                    var theirPick = x[0] - 64;
                    var myPick = x[2] - 87;
                    result += myPick;

                    if (theirPick == myPick)
                        result += 3;
                    else if ((theirPick + 1) % 3 == (myPick + 3) % 3)
                        result += 6;
                });

            return result.ToString();
        }

        public override string SolvePart2()
        {
            int result = 0;
            File.ReadAllLines(_inputPath)
                .ToList()
                .ForEach(x =>
                {
                    var gameResult = x[2] - 87;
                    // X = 0 (Lose), Y = 3 (Draw), Z = 6 (Win)
                    result += ((gameResult + 2) % 3) * 3;
                    var theirPickValue = x[0] - 64;
                    // x[2] - 87 to get the correct decimal value
                    // + 1 then so that X % 3 = 2, Y % 3 = 0, Z % 3 = 1
                    var myPick = (theirPickValue + ((x[2] - 87 + 1) % 3));
                    result += (myPick > 3) ? myPick - 3 : myPick;
                });

            return result.ToString();
        }
    }
}
