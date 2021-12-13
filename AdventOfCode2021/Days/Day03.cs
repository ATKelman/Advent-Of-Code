using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2021.Days
{
    public class Day03 : DayBase
    {
        public Day03(int day)
            : base(day)
        { }

        public override string SolvePart1()
        {
            string gamma = ""; // MCV
            string epsilon = ""; // LCV

            var input = File.ReadAllLines(_inputPath);

            for (int i = 0; i < input[0].Length; i++)
            {
                var ones = input.Where(x => x[i] == '1').Count();

                if (ones > input.Length / 2)
                {
                    gamma += "1";
                    epsilon += "0";
                }
                else
                {
                    gamma += "0";
                    epsilon += "1";
                }
            }

            var g = Convert.ToInt32(gamma, 2);
            var e = Convert.ToInt32(epsilon, 2);
            return (g * e).ToString();
        }

        public override string SolvePart2()
        {
            var input = File.ReadAllLines(_inputPath);
            var oxygen = input;
            var co2 = input;

            var oValue = 0;
            var cValue = 0;

            for (int i = 0; i < input[0].Length; i++)
            {
                var ones = oxygen.Where(x => x[i] == '1').Count();

                if (ones >= (decimal)oxygen.Length / 2)
                {
                    oxygen = oxygen.Where(x => x[i] == '1').ToArray();
                }
                else
                {
                    oxygen = oxygen.Where(x => x[i] == '0').ToArray();
                }

                if (oxygen.Length == 1)
                {
                    oValue = Convert.ToInt32(oxygen.First(), 2);
                    break;
                } 
            }

            for (int i = 0; i < input[0].Length; i++)
            {
                var zeros = co2.Where(x => x[i] == '0').Count();
                var half = co2.Length / 2;

                if (zeros <= (decimal)co2.Length / 2)
                {
                    co2 = co2.Where(x => x[i] == '0').ToArray();
                }
                else
                {
                    co2 = co2.Where(x => x[i] == '1').ToArray();
                }

                if (co2.Length == 1)
                {
                    cValue = Convert.ToInt32(co2.First(), 2);
                    break;
                }
            }

            return (oValue * cValue).ToString();
        }
    }
}
