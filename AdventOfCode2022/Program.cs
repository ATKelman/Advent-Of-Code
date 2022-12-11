using AdventOfCode2022.Days;
using System;

namespace AdventOfCode2022
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Advent of Code 2022");

            var day = new Day11(11);

            var result = day.SolvePart1();

            Console.WriteLine(result);

            var result2 = day.SolvePart2();

            Console.WriteLine(result2);

            Console.WriteLine("Program Done,  Press Any Key to Exit.");

            Console.ReadKey();
        }
    }
}
