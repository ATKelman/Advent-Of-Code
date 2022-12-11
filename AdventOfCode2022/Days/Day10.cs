using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2022.Days
{
    internal class Day10 : DayBase
    {
        public Day10(int day)
        : base(day)
        { }

        public override string SolvePart1()
        {
            var input = File.ReadAllLines(_inputPath);

            int x = 1;
            int cycle = 0;
            int signalStrength = 0;
            for (int i = 0; i < input.Length; i++)
            {
                IncreaseCycle(ref cycle, ref signalStrength, x);
                    
                if (input[i].StartsWith("addx"))
                {
                    IncreaseCycle(ref cycle, ref signalStrength, x);

                    var values = input[i].Split(' ');
                    x += int.Parse(values[1]);
                }
            }

            return signalStrength.ToString();
        }

        private void IncreaseCycle(ref int cycle, ref int signalStrength, int x)
        {
            cycle++;
            if (cycle <= 220 && (cycle - 20) % 40 == 0)
                signalStrength += (cycle * x);
        }

        public override string SolvePart2()
        {
            var input = File.ReadAllLines(_inputPath);

            int x = 1;
            int cycle = 0;

            for (int i = 0; i < input.Length; i++)
            {
                IncreaseCycleAndDraw(ref cycle, x);

                if (input[i].StartsWith("addx"))
                {
                    IncreaseCycleAndDraw(ref cycle, x);

                    var values = input[i].Split(' ');
                    x += int.Parse(values[1]);
                }
            }

            return "";
        }

        private void IncreaseCycleAndDraw(ref int cycle, int x)
        {
            if (Math.Abs((cycle % 40) - x) <= 1)
                Console.Write('#');
            else
                Console.Write('.');

            cycle++;
            if (cycle % 40 == 0)
                Console.WriteLine();
        }
    }
}
