using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2021.Days
{
    public class Day12 : DayBase
    {
        public Day12(int day)
            : base(day)
        { }

        Dictionary<string, List<string>> _pathBindings = new();
        int _paths = 0;

        public override string SolvePart1()
        {
            var input = File
                .ReadAllLines(_inputPath)
                .ToList();
            var bindings = input
                .Select(x => x.Split("-"))
                .ToList();

            _pathBindings = new();
            input
                .SelectMany(x => x.Split("-"))
                .Distinct()
                .ToList()
                .ForEach(x =>
                {
                    _pathBindings.Add(x, 
                        bindings
                        .Where(y => y[0] == x || y[1] == x)
                        .SelectMany(y => y)
                        .Where(y => y != x)
                        .ToList());
                });

            List<string> visited = new ();
            CalculatePath("start", visited, GetNextPart1);

            return _paths.ToString();
        }

        public override string SolvePart2()
        {
            var input = File
                .ReadAllLines(_inputPath)
                .ToList();
            var bindings = input
                .Select(x => x.Split("-"))
                .ToList();

            _pathBindings = new();
            _paths = 0;
            input
                .SelectMany(x => x.Split("-"))
                .Distinct()
                .ToList()
                .ForEach(x =>
                {
                    _pathBindings.Add(x,
                        bindings
                        .Where(y => y[0] == x || y[1] == x)
                        .SelectMany(y => y)
                        .Where(y => y != x)
                        .ToList());
                });

            List<string> visited = new();
            CalculatePath("start", visited, GetNextPart2);
            
            return _paths.ToString();
        }

        public void CalculatePath(string current, List<string> visited, Func<string, List<string>, List<string>> getNext)
        {
            visited.Add(current);
            if (current == "end")
            {
                _paths++;
                return;
            }

            foreach (var instance in getNext(current, visited))         
                CalculatePath(instance, visited.ToList(), getNext);           
        }

        private List<string> GetNextPart1(string current, List<string> visited)
        {
            return _pathBindings[current]
                .Where(x => x.ToUpper() == x || x == "end" || !visited.Contains(x) && x != "start")
                .ToList();
        }

        private List<string> GetNextPart2(string current, List<string> visited)
        {
            var multipleSmall = visited
                .Any(x => x.ToLower() == x && visited.Count(y => y == x) == 2);

            var next = multipleSmall 
                ? _pathBindings[current].Where(x => x.ToUpper() == x || x == "end" || !visited.Contains(x) && x != "start").ToList()
                : _pathBindings[current].Where(x => x != "start").ToList();

            return next;
        }
    }
}
