using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2022.Days
{
    internal class Day05 : DayBase
    {
        private string[] columns;
        private List<(int move, int source, int dest)> instructions = new();

        

        public Day05(int day)
        : base(day)
        {
            var input = File.ReadAllText(_inputPath).Split("\r\n\r\n");

            // Get Columns 
            columns = GetColumns(input[0]);

            // Get Instructions
            foreach (var row in input[1].Split("\r\n"))
            {
                var i = row.Split(" ");
                instructions.Add((int.Parse(i[1]), int.Parse(i[3]), int.Parse(i[5])));
            }
        }

        private List<List<char>> GetTowersList() 
        {
            List<List<char>> towers = new();

            foreach (var row in columns.Where(x => char.IsDigit(x[^1])))
            {
                List<char> temp = new();            
                foreach (char instance in row.Reverse().Skip(1).Where(x => x >= 65 && x <= 90))
                    temp.Add(instance);

                towers.Add(temp);
            }
            return towers;
        }

        private List<Stack<char>> GetTowersStack()
        {
            List<Stack<char>> towers = new();

            foreach (var row in columns.Where(x => char.IsDigit(x[^1])))
            {
                Stack<char> temp = new();
                foreach (char instance in row.Reverse().Skip(1).Where(x => x >= 65 && x <= 90))
                    temp.Push(instance);

                towers.Add(temp);
            }
            return towers;
        }

        public override string SolvePart1()
        {
            List<Stack<char>> towers = GetTowersStack();

            foreach (var instruction in instructions)
                for (int i = 0; i < instruction.move; i++)
                    towers[instruction.dest - 1].Push(towers[instruction.source - 1].Pop());

            return string.Join("", towers.Select(x => x.Peek()));
        }

        public override string SolvePart2()
        {
            List<List<char>> towers = GetTowersList();

            foreach (var instruction in instructions)
            {
                towers[instruction.dest - 1].AddRange(towers[instruction.source - 1].TakeLast(instruction.move));
                towers[instruction.source - 1].RemoveRange(towers[instruction.source - 1].Count - instruction.move, instruction.move);
            }

            return string.Join("", towers.Select(x => x.Last()));
        }

        private string[] GetColumns(string input)
        {
            var rows = input.Split("\r\n");
            int colCount = rows.Max(x => x.Length);

            var columns = new string[colCount];
            for (int i = 0; i < colCount; i++)
            {
                string col = "";
                foreach (var row in rows)
                {
                    try
                    {
                        col += row[i];
                    }
                    catch (Exception)
                    {
                        col += " ";
                    }
                }
                columns[i] = col;
            }

            return columns;
        }
    }
}
