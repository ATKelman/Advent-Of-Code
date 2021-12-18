using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2021.Days
{
    public class Day11 : DayBase
    {
        public Day11(int day)
            : base(day)
        { }

        public override string SolvePart1()
        {
            List<Octopus> octopi = new();

            File
                .ReadAllLines(_inputPath)
                .Select((x, n) => 
                new { 
                    value = x
                        .ToArray()
                        .Select((y, i) => new { value = int.Parse(y.ToString()), Index = i })
                        .ToList(), 
                    Index = n })
                .ToList()
                .ForEach(x => 
                {
                    x.value.ForEach(y =>
                    {
                        octopi.Add(new Octopus(x.Index, y.Index, y.value));
                    });
                });

            octopi.ForEach(x => x.FindNeighbours(octopi));

            for (int i = 0; i < 100; i++)
            {
                octopi.ForEach(x => x.Increase(i));
                octopi
                    .Where(x => x.StepsWithFlash.Contains(i))
                    .ToList()
                    .ForEach(x => x.Energy = 0);
            }

            return octopi
                .Sum(x => x.StepsWithFlash.Count)
                .ToString();
        }

        public override string SolvePart2()
        {
            List<Octopus> octopi = new();

            File
                .ReadAllLines(_inputPath)
                .Select((x, n) =>
                new {
                    value = x
                        .ToArray()
                        .Select((y, i) => new { value = int.Parse(y.ToString()), Index = i })
                        .ToList(),
                    Index = n
                })
                .ToList()
                .ForEach(x =>
                {
                    x.value.ForEach(y =>
                    {
                        octopi.Add(new Octopus(x.Index, y.Index, y.value));
                    });
                });

            octopi.ForEach(x => x.FindNeighbours(octopi));

            int i = 1;
            while(true)
            {
                octopi.ForEach(x => x.Increase(i));
                octopi
                    .Where(x => x.StepsWithFlash.Contains(i))
                    .ToList()
                    .ForEach(x => x.Energy = 0);

                if (octopi.All(x => x.StepsWithFlash.Contains(i))) return i.ToString();

                i++;
            }
        }
    }

    public class Octopus
    {
        public int Energy { get; set; } = 0;
        public (int x, int y) Position { get; set; }

        public List<Octopus> Neighbours { get; set; }
        public List<int> StepsWithFlash { get; set; } = new List<int>();

        public Octopus(int x, int y, int energy)
        {
            Position = (x, y);
            Energy = energy;
        }

        public void FindNeighbours(List<Octopus> others)
        {
            Neighbours = new();

            for (int x = -1; x <= 1; x++)
            {
                for (int y = -1; y <= 1; y++)
                {
                    if (x == 0 && y == 0) continue;

                    var neighbour = others
                        .Where(i => i.Position.x == Position.x + x && i.Position.y == Position.y + y)
                        .FirstOrDefault();

                    if (neighbour != null)
                        Neighbours.Add(neighbour);
                }
            }
        }

        public void Increase(int step)
        {
            Energy++;

            if (Energy > 9 && !StepsWithFlash.Contains(step))
            {
                StepsWithFlash.Add(step);
                Neighbours
                    .ForEach(x => x.Increase(step));
            }
        }
    }
}
