using Microsoft.Extensions.Configuration;

namespace AdventOfCode2023.Days;
public class Day12 : DayBase
{
    public Day12(IConfiguration config, bool isTest)
        : base(config, 2023, 12, isTest)
    {
        
    }

    public override string SolvePart1()
    {
        var input = File.ReadAllLines(_inputPath).ToList();

        long result = 0;
        foreach (var line in input)
        {
            var instance = line.Split(' ');
            result += Compare(instance[0], instance[1].Split(',', StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray());
        }

        return result.ToString();
    }

    private long Compare(string instance, int[] sequence)
    {
        var indexOf = instance.IndexOf('?');
        if (indexOf == -1)
        {
            var isValid = instance.Split('.', StringSplitOptions.RemoveEmptyEntries).Select(x => x.Length).SequenceEqual(sequence);
            return (isValid ? 1 : 0);
        }

        var instanceAsEmpty = instance.Remove(indexOf, 1).Insert(indexOf, ".");
        var resultEmpty = Compare(instanceAsEmpty, sequence);

        var instanceAsSpring = instance.Remove(indexOf, 1).Insert(indexOf, "#");
        var resultSpring = Compare(instanceAsSpring, sequence);

        return resultEmpty + resultSpring;
    }

    public override string SolvePart2()
    {
        var input = File.ReadAllLines(_inputPath).ToList();

        long result = 0;
        foreach (var line in input)
        {
            var instance = line.Split(' ');
            var i = string.Join("?", new string[] { instance[0], instance[0], instance[0], instance[0], instance[0] });
            var s = instance[1].Split(',', StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray();
            var sequence = s.Concat(s).Concat(s).Concat(s).Concat(s).ToArray();
            result += Compare(i, sequence);
        }

        return result.ToString();
    }
}
