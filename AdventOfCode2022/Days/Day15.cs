using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode2022.Days
{
    public class Day15 : DayBase
    {
        private int row = 2000000;

        private List<Sensor> sensors;

        public Day15(int day)
            : base(day)
        { 
            sensors = File.ReadAllLines(_inputPath)
                .Select(Sensor.Parse)
                .ToList();
        }

        public override string SolvePart1()
        {
            var taken = from s in sensors
                        let p = s.Beacon
                        where p.Y == row
                        select p.X;

            return sensors.SelectMany(x => x.GetValuesOnRow(row).Values)
                .Except(taken)
                .Count()
                .ToString();
        }

        public override string SolvePart2()
        {
            long size = row * 2;
            var limit = new Range(0, (int)size);

            for (var y = 0; y <= size; ++y)
            {
                var covered = sensors.Select(s => s.GetValuesOnRow(y));
                var x = FindEmpty(covered, limit);
                if (x is int)
                    return ((x * size) + y).ToString();
            }

            return "";
        }

        private int? FindEmpty(IEnumerable<Range> ranges, Range limit)
        {
            var ordered = ranges.Select(r => r.Intersect(limit))
                                .Where(r => !r.IsEmpty)
                                .OrderBy(r => r.Min)
                                .ThenBy(r => r.Max);

            int max = limit.Min - 1;
            foreach (var r in ordered)
            {
                if (max + 1 < r.Min)
                    return max + 1;

                max = Math.Max(max, r.Max);
            }

            return max < limit.Max
              ? max + 1
              : null;
        }
    }

    record Range(int Min, int Max)
    {
        public static Range Empty = new(0, -1);

        public bool IsEmpty => Min > Max;

        public IEnumerable<int> Values
            => IsEmpty ? Enumerable.Empty<int>() : Enumerable.Range(Min, Max - Min + 1);

        public Range Intersect(Range other)
            => Overlaps(other) ? new(Math.Max(Min, other.Min), Math.Min(Max, other.Max)) : Empty;

        public bool Overlaps(Range other)
            => !IsEmpty
            && !other.IsEmpty
            && Min <= other.Max
            && Max >= other.Min;
    }

    record Point(int X, int Y)
    {
        public int ManhattanDistance(Point other)
            => Math.Abs(other.X - X) + Math.Abs(other.Y - Y);
    }

    record Sensor(Point Position, Point Beacon)
    {
        public int BeaconDistance = Position.ManhattanDistance(Beacon);

        public Range GetValuesOnRow(int y)
        {
            var dy = Math.Abs(y - Position.Y);
            if (dy > BeaconDistance)
                return Range.Empty;

            var dx = BeaconDistance - dy;
            return new(Position.X - dx, Position.X + dx);
        }

        public static Sensor Parse(string line)
        {
            var parts = line.Split(' ');

            var sensorPos = new Point(
              int.Parse(parts[2][2..^1]),
              int.Parse(parts[3][2..^1]));

            var beaconPos = new Point(
              int.Parse(parts[8][2..^1]),
              int.Parse(parts[9][2..]));

            return new(sensorPos, beaconPos);
        }
    }
}
