using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2023.Days;
public class Day5 : DayBase
{

    public Day5(IConfiguration config, bool isTest)
        : base(config, 2023, 5, isTest)
    {
        
    }

    public override string SolvePart1()
    {
        var input = File.ReadAllText(_inputPath).Split("\n\n", StringSplitOptions.RemoveEmptyEntries);
        var inputs = input.Skip(1)
            .Select(x => 
                x.Split("\n", StringSplitOptions.RemoveEmptyEntries)
                    .Skip(1)
                    .Select(y => y.Split(" ", StringSplitOptions.RemoveEmptyEntries).Select(z => long.Parse(z)).ToArray())
            ).ToArray();

        Dictionary<Range, Range>[] maps = new Dictionary<Range, Range>[inputs.Count()];
        for (var i = 0; i < inputs.Length; i++)
        {
            Dictionary<Range, Range> map = new();

            foreach (var item in inputs[i])
            {
                var destination = item[0];
                var source = item[1];
                var length = item[2];

                map.Add(new Range(source, source + length), new Range(destination, destination + length));
            }

            maps[i] = map;
        }

        // Regex?
        var seeds = input[0]
            .Split(":")[1]
            .Split(" ", StringSplitOptions.RemoveEmptyEntries)
            .Select(long.Parse)
            .ToArray();

        long location = long.MaxValue;
        foreach (var seed in seeds)
        {
            long instance = seed;
            for (var i = 0; i < maps.Count(); i++)
            {
                var map = maps[i];
                var match = map.Where(x => x.Key.from <= instance && x.Key.to >= instance).FirstOrDefault();
                
                // if a translation is not mapped then the translation is 1=1
                if (match.Key is not null)
                {
                    var difference = instance - match.Key.from;
                    instance = match.Value.from + difference;
                }
            }
            if (instance < location)
                location = instance;
        }
            

        return location.ToString();
    }

    record Range(long from, long to);

    public override string SolvePart2()
    {
        var input = File.ReadAllText(_inputPath).Split("\n\n", StringSplitOptions.RemoveEmptyEntries);
        var inputs = input.Skip(1)
            .Select(x =>
                x.Split("\n", StringSplitOptions.RemoveEmptyEntries)
                    .Skip(1)
                    .Select(y => y.Split(" ", StringSplitOptions.RemoveEmptyEntries).Select(z => long.Parse(z)).ToArray())
            ).ToArray();

        // Dictionary Mapping: Key = Source, Value = Destination 
        Dictionary<Range, Range>[] maps = new Dictionary<Range, Range>[inputs.Count()];
        for (var i = 0; i < inputs.Length; i++)
        {
            Dictionary<Range, Range> map = new();

            foreach (var item in inputs[i])
            {
                var destination = item[0];
                var source = item[1];
                var length = item[2];

                map.Add(new Range(source, source + length - 1), new Range(destination, destination + length - 1));
            }

            maps[i] = map;
        }

        var seeds = input[0]
            .Split(":")[1]
            .Split(" ", StringSplitOptions.RemoveEmptyEntries)
            .Select(long.Parse)
            .ToArray();

        List<Range> seedRanges = new();
        for (int i = 0; i < seeds.Length; i += 2)
        {
            var instanceRange = new Range(seeds[i], seeds[i] + seeds[i + 1] - 1); 
            seedRanges.Add(instanceRange);
        }

        List<Range> r = seedRanges;
        List<Range> nextMapRanges = new();
        for (int i = 0; i < maps.Length; i++)
        {
            var map = maps[i];
            nextMapRanges = new();

            while (r.Count > 0)
            {
                var instance = r[0];
                r.RemoveAt(0);

                var intersects = map.FirstOrDefault(x => Intersects(instance, x.Key));
                // No match, keep current range 
                if (intersects.Key is null)
                    nextMapRanges.Add(instance);
                // Intersection contains our entire range
                // Add matched destination ranges  
                else if (instance.from >= intersects.Key.from && instance.to <= intersects.Key.to)
                {
                    var destination = intersects.Value;
                    var startingShift = instance.from - intersects.Key.from;
                    var endingShift = (instance.to - instance.from) + startingShift;
                    nextMapRanges.Add(new Range(destination.from + startingShift, destination.from + endingShift));
                }
                // Intersects on upper portion
                else if (instance.from < intersects.Key.from)
                {
                    r.Add(new Range(instance.from, intersects.Key.from - 1));
                    r.Add(new Range(intersects.Key.from, instance.to));
                }
                // Intersects on lower portion
                else 
                {
                    r.Add(new Range(instance.from, intersects.Key.to));
                    r.Add(new Range(intersects.Key.to + 1, instance.to));
                }
                
            }
            r = nextMapRanges;
        }

        return nextMapRanges.Select(x => x.from).Min().ToString();
    }

    // r1 start should be lower than r2, while r1 should end within r2
    // r1 | ------ | 
    // r2      | ---------- | 
    private bool Intersects(Range r1, Range r2)
    {
        return r1.from <= r2.to && r1.to >= r2.from;
    }
}
