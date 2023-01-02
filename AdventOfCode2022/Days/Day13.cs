using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Text.Json;

namespace AdventOfCode2022.Days
{
    public class Day13 : DayBase
    {
        public Day13(int day)
            : base(day)
        { }

        public override string SolvePart1()
        {
            var inputs = File.ReadAllText(_inputPath)
                .Split("\r\n\r\n")
                .Select(x => x.Split("\r\n"));

            int sum = 0;
            int index = 1;

            foreach (var input in inputs)
            {
                // input to queue
                Packet item1 = Packet.Parse(input[0]);
                Packet item2 = Packet.Parse(input[1]);

                if (item1.Compare(item2) <= 0)
                    sum += index;
                index++;
            }

            return sum.ToString();
        }

        public override string SolvePart2()
        {
            List<Packet> packets = File.ReadAllText(_inputPath)
                .Split('\n')
                .Where(x => x.Trim() != string.Empty)
                .Select(x => x.Trim())
                .Select(Packet.Parse)
                .ToList();

            Packet marker1 = Packet.Parse("[[2]]");
            packets.Add(marker1);

            Packet marker2 = Packet.Parse("[[6]]");
            packets.Add(marker2);

            packets.Sort((p0, p1) => p0.Compare(p1));

            int indexMarker1 = packets.IndexOf(marker1) + 1;
            int indexMarker2 = packets.IndexOf(marker2) + 1;

            return (indexMarker1 * indexMarker2).ToString();
        }       
    }

    public abstract record Packet
    {
        public int Compare(Packet other)
        {
            return (this, other) switch
            {
                (PacketValue p0, PacketValue p1) => Math.Sign(p0.val - p1.val),
                (PacketValue p0, _) => p0.AsList().Compare(other),
                (_, PacketValue p1) => this.Compare(p1.AsList()),
                (PacketList p0, PacketList p1) => CompareLists(p0.Packets, p1.Packets),
                _ => throw new Exception($"Unexpected compare {this} with {other}"),
            };
        }

        public static int CompareLists(List<Packet> ls, List<Packet> js)
        {
            for (int i = 0; i < ls.Count; i++)
            {
                if (i >= js.Count)
                {
                    return 1; // First list is longer than second list
                }
                Packet p0 = ls[i];
                Packet p1 = js[i];
                int diff = p0.Compare(p1);
                if (diff == 0)
                {
                    continue;
                }
                return diff;
            }
            return Math.Sign(ls.Count - js.Count);
        }

        public static Packet Parse(string packet)
        {
            Queue<char> input = new(packet.ToList());
            return Parse(input);
        }

        private static Packet Parse(Queue<char> input)
        {
            char c = input.Peek();
            if (char.IsDigit(c))
                return ParseInt(input);
            else if (c == '[')
            {
                input.Dequeue();
                return input.Peek() switch
                {
                    ']' => new PacketList(),
                    _ => new PacketList(ParseList(input, new List<Packet>()))
                };
            }
            else
                throw new Exception();
        }

        private static List<Packet> ParseList(Queue<char> input, List<Packet> container)
        {
            Packet packet = Parse(input);
            container.Add(packet);
            char c = input.Dequeue();
            return c switch
            {
                ',' => ParseList(input, container),
                ']' => container,
                _ => throw new InvalidDataException()
            };
        }

        private static PacketValue ParseInt(Queue<char> input)
        {
            StringBuilder sb = new();
            while (input.Count > 0 && char.IsDigit(input.Peek())) 
                sb.Append(input.Dequeue());
            return new PacketValue(int.Parse(sb.ToString()));
        }
    }

    public record PacketList(List<Packet> Packets) : Packet
    {
        public PacketList(params Packet[] packets) : this(packets.ToList()) { }

        public static List<Packet> Parse(char[] data, int i)
        {
            List<Packet> packets = new();
            return packets;
        }
    }

    public record PacketValue(int val) : Packet
    {
        public PacketList AsList() { return new PacketList(this); }
    }
}
