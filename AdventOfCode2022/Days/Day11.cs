using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2022.Days
{
    internal class Day11 : DayBase
    {
        public Day11(int day)
        : base(day)
        { }

        public override string SolvePart1()
        {
            var input = File.ReadAllText(_inputPath).Split("\r\n\r\n").Select(x => x.Split("\r\n"));

            List<Monkey> monkeys = new();
            foreach (var m in input)
                monkeys.Add( new Monkey(m));

            for (int round = 0; round < 20; round++)
            {
                for (int i = 0; i <= monkeys.Count - 1; i++)
                {
                    foreach (var item in monkeys[i].Items)
                    {
                        long worry = monkeys[i].InspectItem(item);
                        worry /= 3;

                        if (worry % monkeys[i].Divisble == 0)
                            monkeys[monkeys[i].TrueMonkeyIndex].Items.Add(worry);
                        else
                            monkeys[monkeys[i].FalseMonkeyIndex].Items.Add(worry);
                    }

                    monkeys[i].Items.Clear();
                }
            }

            return monkeys.OrderByDescending(x => x.InspectedItemCount).Take(2).Select(x => x.InspectedItemCount).Aggregate((x, i) => x * i).ToString();
        }

        public override string SolvePart2()
        {
            var input = File.ReadAllText(_inputPath).Split("\r\n\r\n").Select(x => x.Split("\r\n"));

            List<Monkey> monkeys = new();
            foreach (var m in input)
                monkeys.Add(new Monkey(m));

            long divisor = 1;
            foreach (var m in monkeys)
                divisor *= m.Divisble;

            for (int round = 0; round < 10000; round++)
            {
                for (int i = 0; i <= monkeys.Count - 1; i++)
                {
                    foreach (var item in monkeys[i].Items)
                    {
                        long worry = monkeys[i].InspectItem(item) % divisor;

                        if (worry % monkeys[i].Divisble == 0)
                            monkeys[monkeys[i].TrueMonkeyIndex].Items.Add(worry);
                        else
                            monkeys[monkeys[i].FalseMonkeyIndex].Items.Add(worry);
                    }

                    monkeys[i].Items.Clear();
                }
                
            }

            return monkeys.OrderByDescending(x => x.InspectedItemCount).Take(2).Select(x => x.InspectedItemCount).Aggregate((x, i) => x * i).ToString();
        }
    }

    public class Monkey
    {
        public List<long> Items { get; set; }
        public string[] Operation { get; set; }

        public int Divisble { get; set; }
        public int TrueMonkeyIndex { get; set; }
        public int FalseMonkeyIndex { get; set; }

        public long InspectedItemCount { get; set; }

        public Monkey(string[] inputs)
        {
            Items = inputs[1].Replace("Starting items:", "").Replace(",", "").Split(" ", StringSplitOptions.RemoveEmptyEntries).Select(long.Parse).ToList();
            Operation = inputs[2].Replace("Operation: new = ", "").Split(" ", StringSplitOptions.RemoveEmptyEntries);

            Divisble = int.Parse(inputs[3].Split(' ').Last());

            TrueMonkeyIndex = int.Parse(inputs[4].Split(' ').Last());
            FalseMonkeyIndex = int.Parse(inputs[5].Split(' ').Last());
        }

        public long InspectItem(long item)
        {
            InspectedItemCount++;

            var i1 = Operation[0] == "old" ? item : long.Parse(Operation[0]);
            var i2 = Operation[2] == "old" ? item : long.Parse(Operation[2]);

            return Operation[1] == "*" ? (i1 * i2) : (i1 + i2);
        }
    }
}
