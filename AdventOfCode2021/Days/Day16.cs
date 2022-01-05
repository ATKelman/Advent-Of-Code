using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2021.Days
{
    public class Day16 : DayBase
    {
        public int VersionsSum { get; set; }
        public long Sum { get; set; }

        public Day16(int day)
            : base(day)
        {
            var input = File
                .ReadAllText(_inputPath);

            var binary = StringToBinary(input);

            Sum = HandlePacket(ref binary);
        }

        public override string SolvePart1()
        {
            return VersionsSum.ToString();
        }

        public override string SolvePart2()
        {
            return Sum.ToString();
        }

        private string StringToBinary(string data)
        {
            StringBuilder sb = new();

            foreach (var c in data.ToCharArray())
            {
                sb.Append(
                    Convert.ToString(
                        Convert.ToInt32(c.ToString(), 16)
                        , 2)
                    .PadLeft(4, '0'));
            }
            return sb.ToString();
        }

        private long HandlePacket(ref string binary)
        {
            var version = binary.Substring(0, 3);
            binary = binary.Remove(0, 3);

            VersionsSum += Convert.ToInt32(version, 2);

            var typeBinary = binary.Substring(0, 3);
            binary = binary.Remove(0, 3);
            var type = Convert.ToInt32(typeBinary, 2);

            if (type == 4)
            {
                // Literal Value 
                string instance = "";
                StringBuilder literalValue = new();
                do
                {
                    instance = binary.Substring(0, 5);
                    binary = binary.Remove(0, 5);

                    literalValue.Append(instance.Substring(1, 4));
                } while (instance.StartsWith('1'));

                return Convert.ToInt64(literalValue.ToString(), 2);
            }
            else
            {
                List<long> subPacketValues = new();

                var id = binary.Substring(0, 1);
                binary = binary.Remove(0, 1);

                if (id == "0")
                {
                    var length = 15;
                    var subPacketsLength = binary.Substring(0, length);
                    binary = binary.Remove(0, length);

                    var subPacketsBinaryLength = Convert.ToInt32(subPacketsLength, 2);
                    var subPacketsBinary = binary.Substring(0, subPacketsBinaryLength);
                    binary = binary.Remove(0, subPacketsBinaryLength);

                    while (subPacketsBinary.Length >= 11)
                        subPacketValues.Add(HandlePacket(ref subPacketsBinary));
                }
                else
                {
                    var amountOfSubpackets = binary.Substring(0, 11);
                    binary = binary.Remove(0, 11);
                    for (int i = 0; i < Convert.ToInt32(amountOfSubpackets, 2); i++)
                        subPacketValues.Add(HandlePacket(ref binary));
                }

                return type switch
                {
                    0 => subPacketValues.Sum(),
                    1 => subPacketValues.Aggregate((current, next) => current * next),
                    2 => subPacketValues.Min(),
                    3 => subPacketValues.Max(),
                    5 => (subPacketValues[0] > subPacketValues[1]) ? 1 : 0,
                    6 => (subPacketValues[0] < subPacketValues[1]) ? 1 : 0,
                    7 => (subPacketValues[0] == subPacketValues[1]) ? 1 : 0,
                    _ => throw new NotImplementedException($"Case not implemented for type [{type}]"),
                };
            }
        }
    }
}
