using Microsoft.Extensions.Configuration;
using System.Text.RegularExpressions;

namespace AdventOfCode2023.Days;
public class Day11 : DayBase
{
    public Day11(IConfiguration config, bool isTest)
        : base(config, 2023, 11, isTest)
    {
        
    }

    public override string SolvePart1()
    {
        var input = File.ReadAllLines(_inputPath);

        var regex = new Regex(@"#");
        var galaxies = input
            .Select((row, index) => regex.Matches(row).Select(col => new Galaxy((col.Index, index))))
            .Aggregate((x, y) => x.Concat(y))
            .ToArray();
      
        var columns = Enumerable.Range(0, input[0].Length - 1).ToList();
        var galaxyXPositions = galaxies.Select(galaxy => galaxy.Pos.x).ToList();
        var columnExpansions = columns.Except(galaxyXPositions).ToList();

        var rows = Enumerable.Range(0, input.Length - 1).ToList();
        var galaxyYPositions = galaxies.Select(galaxy => galaxy.Pos.y).ToList();
        var rowExpansions = rows.Except(galaxyYPositions).ToList();

        for (int i = 0; i < galaxies.Count(); i++)
        {
            Galaxy galaxy = galaxies[i];
            var dx = galaxy.Pos.x + columnExpansions.Where(col => galaxy.Pos.x > col).Count();
            var dy = galaxy.Pos.y + rowExpansions.Where(row => galaxy.Pos.y > row).Count();
            galaxies[i].Pos = (dx, dy);
        }

        long distances = 0;
        for (int i = 0; i < galaxies.Count() - 1; i++)
        {
            Galaxy first = galaxies[i];
            for (int j = i + 1; j < galaxies.Count(); j++)
            {
                Galaxy second = galaxies[j];
                distances += Math.Abs(first.Pos.x - second.Pos.x) + Math.Abs(first.Pos.y - second.Pos.y);
            }
        }

        return distances.ToString();
    }

    public override string SolvePart2()
    {
        var input = File.ReadAllLines(_inputPath);

        var regex = new Regex(@"#");
        var galaxies = input
            .Select((row, index) => regex.Matches(row).Select(col => new Galaxy((col.Index, index))))
            .Aggregate((x, y) => x.Concat(y))
            .ToArray();

        var columns = Enumerable.Range(0, input[0].Length - 1).ToList();
        var galaxyXPositions = galaxies.Select(galaxy => galaxy.Pos.x).ToList();
        var columnExpansions = columns.Except(galaxyXPositions).ToList();

        var rows = Enumerable.Range(0, input.Length - 1).ToList();
        var galaxyYPositions = galaxies.Select(galaxy => galaxy.Pos.y).ToList();
        var rowExpansions = rows.Except(galaxyYPositions).ToList();

        for (int i = 0; i < galaxies.Count(); i++)
        {
            Galaxy galaxy = galaxies[i];
            galaxy.ExpansionsX = (columnExpansions.Where(col => galaxy.Pos.x > col).Count() * 1000000) - columnExpansions.Where(col => galaxy.Pos.x > col).Count();
            galaxy.ExpansionsY = (rowExpansions.Where(row => galaxy.Pos.y > row).Count() * 1000000) - rowExpansions.Where(row => galaxy.Pos.y > row).Count();
        }

        long distances = 0;
        for (int i = 0; i < galaxies.Count() - 1; i++)
        {
            Galaxy first = galaxies[i];
            for (int j = i + 1; j < galaxies.Count(); j++)
            {
                Galaxy second = galaxies[j];

                long xDistance = Math.Abs((second.Pos.x + second.ExpansionsX) - (first.Pos.x + first.ExpansionsX));
                long yDistance = Math.Abs((second.Pos.y + second.ExpansionsY) - (first.Pos.y + first.ExpansionsY));
                distances += xDistance + yDistance;
            }
        }

        return distances.ToString();
    }

    private class Galaxy((int x, int y) pos)
    {
        public (int x, int y) Pos { get; set; } = pos;
        public long ExpansionsX { get; set; }
        public long ExpansionsY { get; set; }

        public void ExpandColumn() => Pos = (Pos.x, Pos.y + 1);
        public void ExpandRow() => Pos = (Pos.x + 1, Pos.y);
    }
}
