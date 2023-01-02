using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode2022.Days
{
    public class Day15 : DayBase
    {
        public Day15(int day)
            : base(day)
        { }

        public override string SolvePart1()
        {
            Dictionary<(int x, int y), string> grid = new();

            Regex rx = new(@"\d+");

            var input = File
                .ReadAllLines(_inputPath)
                .Select(x => rx.Matches(x))
                .ToList();

            foreach (var pair in input)
            {              
                (int x, int y) sensorPos = (int.Parse(pair[0].Value), int.Parse(pair[1].Value));
                grid[sensorPos] = "S";

                (int x, int y) beaconPos = (int.Parse(pair[2].Value), int.Parse(pair[3].Value));
                grid[beaconPos] = "B";

                int length = Math.Abs(sensorPos.x - beaconPos.x) + Math.Abs(sensorPos.y - beaconPos.y);

                foreach (var yAdjustments in Enumerable.Range(0, length + 1))
                {
                    var xRange = length - Math.Abs(yAdjustments);
                    foreach (var xAdjustments in Enumerable.Range(0, xRange + 1))
                    {
                        var posUpRight = (x: sensorPos.x + xAdjustments, y: sensorPos.y + yAdjustments);
                        if (!grid.ContainsKey(posUpRight))
                            grid[posUpRight] = "#";

                        var posUpLeft = (x: sensorPos.x - xAdjustments, y: sensorPos.y + yAdjustments);
                        if (!grid.ContainsKey(posUpLeft))
                            grid[posUpLeft] = "#";

                        var posDownRight = (x: sensorPos.x + xAdjustments, y: sensorPos.y - yAdjustments);
                        if (!grid.ContainsKey(posDownRight))
                            grid[posDownRight] = "#";

                        var posDownLeft = (x: sensorPos.x - xAdjustments, y: sensorPos.y - yAdjustments);
                        if (!grid.ContainsKey(posDownLeft))
                            grid[posDownLeft] = "#";
                    }
                } 
            }

            var test = grid.Where(x => x.Key.y == 10 && x.Value == "#").Count();

            return test.ToString();
        }

        public override string SolvePart2()
        {
            return "";
        }
    }
}
