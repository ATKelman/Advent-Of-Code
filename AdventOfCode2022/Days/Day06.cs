using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2022.Days
{
    internal class Day06 : DayBase
    {
        private string input;

        public Day06(int day)
        : base(day)
        {
            input = File.ReadAllText(_inputPath);
        }

        public override string SolvePart1() => GetMarker(4).ToString();

        public override string SolvePart2() => GetMarker(14).ToString();

        private int GetMarker(int count)
        {
            Queue<char> marker = new();
            for (int i = 0; i < input.Length; i++)
            {
                marker.Enqueue(input[i]);

                if (marker.Count > count)
                    marker.Dequeue();

                if (marker.Distinct().Count() == count)
                    return (i + 1);
            }
            return 0;
        }
    }
}
