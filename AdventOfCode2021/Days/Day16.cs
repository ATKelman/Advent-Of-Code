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
        public Day16(int day)
            : base(day)
        { }

        int versionsSum = 0;

        public override string SolvePart1()
        {
            var input = File
                .ReadAllText(_inputPath);

            var binary = StringToBinary(input);

            _ = HandlePacket(binary);
                    
            return versionsSum.ToString();
        }

        public override string SolvePart2()
        {
            return "";
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

        private string HandlePacket(string binary)
        {
            var version = binary.Substring(0, 3);
            binary = binary.Remove(0, 3);

            versionsSum += Convert.ToInt32(version, 2);

            var type = binary.Substring(0, 3);
            binary = binary.Remove(0, 3);

            switch (Convert.ToInt32(type, 2))
            {
                case 4:
                    // Type = 4 Literal Value 
                    string instance = "";
                    StringBuilder literalValue = new();
                    do
                    {
                        instance = binary.Substring(0, 5);
                        binary = binary.Remove(0, 5);

                        literalValue.Append(instance.Substring(1, 4));
                    } while (instance.StartsWith('1'));

                    break;
                default:
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
                            subPacketsBinary = HandlePacket(subPacketsBinary);
                    }
                    else
                    {
                        var amountOfSubpackets = binary.Substring(0, 11);
                        binary = binary.Remove(0, 11);
                        for (int i = 0; i < Convert.ToInt32(amountOfSubpackets, 2); i++)
                        {
                            binary = HandlePacket(binary);
                        }                       
                    }

                    break;
            }

            return binary;
        }
    }
}
