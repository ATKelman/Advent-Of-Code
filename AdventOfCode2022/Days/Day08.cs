using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2022.Days
{
    internal class Day08 : DayBase
    {
        private Tree[,] trees;

        public Day08(int day)
        : base(day)
        {
            var input = File.ReadAllLines(_inputPath);

            trees = new Tree[input.Length, input[0].Length];
            for (int row = 0; row < input.Length; row++)
            {
                for (int col = 0; col < input[0].Length; col++)
                {
                    var tree = new Tree(int.Parse(input[row][col].ToString()), row, col);
                    // Tree on edge
                    if (row == 0 || col == 0 || row == input.Length - 1 || col == input[0].Length - 1)
                        tree.Visible = true;
                    trees[row, col] = tree;
                }
            }

            for (int row = 0; row < input.Length; row++)
            {
                for (int col = 0; col < input[0].Length; col++)
                {
                    if (!trees[row, col].Visible)
                        GetVisibiltyOutwards(trees[row, col]);
                }
            }
        }

        public override string SolvePart1()
        {
            int visible = 0;
            for (int row = 0; row <= trees.GetUpperBound(0); row++)
            {
                for (int col = 0; col <= trees.GetUpperBound(1); col++)
                {
                    if (trees[row, col].Visible)
                        visible++;
                }
            }

            return visible.ToString();
        }

        public override string SolvePart2()
        {
            int score = 0;
            for (int row = 0; row <= trees.GetUpperBound(0); row++)
            {
                for (int col = 0; col <= trees.GetUpperBound(1); col++)
                {
                    if (trees[row, col].Score > score)
                        score = trees[row, col].Score;
                }
            }

            return score.ToString();
        }

        private void GetVisibiltyOutwards (Tree tree)
        {
            int isVisible = 0;
            (int left, int right, int top, int bottom) visibilityDirections = (0, 0, 0, 0);
            
            // Check to Top
            for (int i = tree.Row - 1; i >= 0; i--)
            {
                visibilityDirections.top++;
                if (tree.Height <= trees[i, tree.Col].Height)
                {
                    isVisible++;
                    break;
                }
            }

            // Check to Bottom
            for (int i = tree.Row + 1; i <= trees.GetUpperBound(1); i++)
            {
                visibilityDirections.bottom++;
                if (tree.Height <= trees[i, tree.Col].Height)
                {
                    isVisible++;
                    break;
                }
            }

            // Check to Left
            for (int i = tree.Col - 1; i >= 0; i--)
            {
                visibilityDirections.left++;
                if (tree.Height <= trees[tree.Row, i].Height)
                {
                    isVisible++;
                    break;
                }
            }

            // Check to Right
            for (int i = tree.Col + 1; i <= trees.GetUpperBound(0); i++)
            {
                visibilityDirections.right++;
                if (tree.Height <= trees[tree.Row, i].Height)
                {
                    isVisible++;
                    break;
                }
            }

            if (isVisible != 4)
                tree.Visible = true;

            tree.Score = visibilityDirections.bottom * visibilityDirections.top * visibilityDirections.right * visibilityDirections.left;
        }
    }

    public class Tree
    {
        public int Height { get; set; }
        public bool Visible { get; set; }
        public int Score { get; set; }

        public int Row { get; set; }
        public int Col { get; set; }

        public Tree(int height, int row, int col)
        {
            Height = height;
            Row = row;
            Col = col;
        }
    }
}
