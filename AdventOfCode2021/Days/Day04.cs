using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2021.Days
{
    public class Day04 : DayBase
    {
        private List<Board> _boards { get; set; }

        public Day04(int day)
            : base(day)
        {
            var input = File.ReadAllText(_inputPath)
                    .Split($"\r\n\r\n", StringSplitOptions.RemoveEmptyEntries)
                    .ToList();
            var numbers = input[0]
                .Split(',', StringSplitOptions.RemoveEmptyEntries)
                .Select(int.Parse)
                .ToArray();
            input.RemoveAt(0);

            var boardValues = input
                .Select(x =>
                    x.Replace("\r\n", " ")
                    .Split(" ", StringSplitOptions.RemoveEmptyEntries)
                    .Select(int.Parse)
                    .ToArray());

            _boards = new List<Board>();
            foreach (var values in boardValues)
            {
                var board = new Board(values, numbers);
                board.RunSimulation();
                _boards.Add(board);
            }
        }

        public override string SolvePart1()
        {
            return _boards
                .OrderBy(x => x.WinInMoves)
                .Select(x => x.SumOfUnmarked * x.WinningNumber)
                .First()
                .ToString();
        }

        public override string SolvePart2()
        {
            return _boards
                .OrderBy(x => x.WinInMoves)
                .Select(x => x.SumOfUnmarked * x.WinningNumber)
                .Last()
                .ToString();
        }
    }

    public class Board
    {
        public int WinInMoves { get; set; }
        public int WinningNumber { get; set; }
        public int SumOfUnmarked { get; set; }

        private (int value, bool marked)[][] BingoBoard { get; set; }
        private int[] BingoNumbers { get; set; }

        public Board(int[] values, int[] numbers)
        {
            BingoBoard = new (int value, bool marked)[5][];
            for (int i = 0; i < 5; i++)
                BingoBoard[i] = new (int value, bool marked)[5];

            for (int i = 0; i < values.Length; i++)
                BingoBoard[i / 5][i % 5] = (values[i], false);

            SumOfUnmarked = values.Sum();
            BingoNumbers = numbers;
        }

        public void RunSimulation()
        {
            for (int number = 0; number < BingoNumbers.Length; number++)
            {
                MarkNumber(BingoNumbers[number]);
                if (CheckBingo())
                {
                    WinningNumber = BingoNumbers[number];
                    WinInMoves = number + 1;
                    break;
                }
            }
        }

        public void MarkNumber(int number)
        {
            for (int row = 0; row < 5; row++)
            {
                for (int col = 0; col < 5; col++)
                {
                    if (BingoBoard[row][col].value == number)
                    {
                        BingoBoard[row][col].marked = true;
                        SumOfUnmarked -= number;
                    }
                }
            }
        }

        public bool CheckBingo()
        {
            for (int row = 0; row < 5; row++)
            {
                if (CheckRow(row))
                    return true;
            }

            for (int col = 0; col < 5; col++)
            {
                if (CheckColumn(col))
                    return true;
            }

            return false;
        }

        public bool CheckRow(int row)
        {
            return BingoBoard[row].Count(x => x.marked == true) == 5;
        }

        public bool CheckColumn(int col)
        {
            return BingoBoard.Select(x => x[col]).Count(x => x.marked == true) == 5;
        }
    }
}
