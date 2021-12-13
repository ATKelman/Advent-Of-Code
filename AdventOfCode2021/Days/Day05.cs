using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AdventOfCode2021.Days
{
    public class Day05 : DayBase
    {
        public Day05(int day)
            : base(day)
        { }

        public override string SolvePart1()
        {
            var input = File
                .ReadAllLines(_inputPath)
                .Select(x => Regex.Matches(x, @"\d{1,3}"))
                .ToArray();

            var map = new Dictionary<(int x, int y), int>();

            for (int i = 0; i < input.Length; i++)
            {
                var instance = input[i]
                    .Select(x => int.Parse(x.Value))
                    .ToList();
              
                if (instance[0] != instance[2] && instance[1] != instance[3])
                    continue;

                int idToChange = (instance[0] != instance[2]) ? 0 : 1;
                int increment = (instance[idToChange] > instance[idToChange + 2]) ? -1 : 1;

                if (!map.TryAdd((instance[0], instance[1]), 1))
                    map[(instance[0], instance[1])]++;

                while (instance[0] != instance[2] || instance[1] != instance[3])
                {
                    instance[idToChange] += increment;
                    if (!map.TryAdd((instance[0], instance[1]), 1))
                        map[(instance[0], instance[1])]++;
                }
            }

            return map
                .Where(x => x.Value >= 2)
                .Count()
                .ToString();
        }

        public override string SolvePart2()
        {
            var input = File
                .ReadAllLines(_inputPath)
                .Select(x => Regex.Matches(x, @"\d{1,3}"))
                .ToArray();

            var map = new Dictionary<(int x, int y), int>();

            for (int i = 0; i < input.Length; i++)
            {
                var instance = input[i]
                    .Select(x => int.Parse(x.Value))
                    .ToList();

                if (instance[0] != instance[2] && instance[1] != instance[3])
                {
                    var incrementX = (instance[0] > instance[2]) ? -1 : 1;
                    var incrementY = (instance[1] > instance[3]) ? -1 : 1;

                    if (!map.TryAdd((instance[0], instance[1]), 1))
                        map[(instance[0], instance[1])]++;

                    while (instance[0] != instance[2] || instance[1] != instance[3])
                    {
                        instance[0] += incrementX;
                        instance[1] += incrementY;
                        if (!map.TryAdd((instance[0], instance[1]), 1))
                            map[(instance[0], instance[1])]++;
                    }
                }
                else
                {
                    int idToChange = (instance[0] != instance[2]) ? 0 : 1;
                    int increment = (instance[idToChange] > instance[idToChange + 2]) ? -1 : 1;

                    if (!map.TryAdd((instance[0], instance[1]), 1))
                        map[(instance[0], instance[1])]++;

                    while (instance[0] != instance[2] || instance[1] != instance[3])
                    {
                        instance[idToChange] += increment;
                        if (!map.TryAdd((instance[0], instance[1]), 1))
                            map[(instance[0], instance[1])]++;
                    }
                }           
            }

            return map
                .Where(x => x.Value >= 2)
                .Count()
                .ToString();
        }
    }
}
