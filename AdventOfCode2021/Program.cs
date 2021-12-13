using AdventOfCode2021.Days;
using System;

namespace AdventOfCode2021
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Advent of Code 2021");

            var day = new Day07(7);

            var result = day.SolvePart1();

            Console.WriteLine(result);

            var result2 = day.SolvePart2();

            Console.WriteLine(result2);

            Console.WriteLine("Program Done,  Press Any Key to Exit.");

            Console.ReadKey();
        }
    }
}
