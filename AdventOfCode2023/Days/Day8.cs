using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AdventOfCode2023.Days;
public class Day8 : DayBase
{
    public Day8(IConfiguration config, bool isTest)
        : base(config, 2023, 8, isTest)
    {

    }
    public override string SolvePart1()
    {
        var input = File.ReadAllText(_inputPath)
            .Split("\n", StringSplitOptions.RemoveEmptyEntries);

        var instructions = input[0].ToCharArray();
        Regex regex = new(@"[A-Z]+");
        var nodes = input
            .Skip(1)
            .Select(x => regex.Matches(x).ToArray())
            .ToDictionary(x => x[0].Value, x => (left: x[1].Value, right: x[2].Value));

        return GetSteps(instructions, nodes, "AAA", "ZZZ").ToString();
    }

    public override string SolvePart2()
    {
        var input = File.ReadAllText(_inputPath)
            .Split("\n", StringSplitOptions.RemoveEmptyEntries);

        var instructions = input[0].ToCharArray();
        Regex regex = new(@"[A-Z]+");
        var nodes = input
            .Skip(1)
            .Select(x => regex.Matches(x).ToArray())
            .ToDictionary(x => x[0].Value, x => (left: x[1].Value, right: x[2].Value));

        var result = nodes.Keys.Where(x => x[2] == 'A')
            .Select(x => GetSteps(instructions, nodes, x, "Z"))
            .Aggregate(LCM);

        return result.ToString();
    }

    private long GetSteps(char[] instructions, Dictionary<string, (string left, string right)> nodes, string node, string ending)
    {
        long steps = 0;
        while (!node.EndsWith(ending))
        {
            node = instructions[steps % instructions.Length] == 'L'
                ? nodes[node].left
                : nodes[node].right;
            steps++;
        }
        return steps;
    }

    // Googled LCM implementation C# and found this result. 
    private long LCM(long a, long b) => (a / GCF(a, b)) * b;

    private long GCF(long a, long b)
    {
        while (b != 0)
        {
            long temp = b;
            b = a % b;
            a = temp;
        }
        return a;
    }
}
