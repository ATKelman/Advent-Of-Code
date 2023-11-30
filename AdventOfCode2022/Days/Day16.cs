using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AdventOfCode2022.Days
{
    public class Day16 : DayBase
    {
        public Day16(int day)
            : base(day)
        { }

        public override string SolvePart1()
        {
            Regex rx = new(@"Valve (?<name>[A-Z]+) has flow rate=(?<pressure>\d+); tunnels* leads* to valves* (?<valves>([A-Z]+,* *)+)");

            List<Valve> valves = File.ReadAllLines(_inputPath)
                .Select(x =>
                {
                    var matches = rx.Match(x).Groups;

                    return new Valve(
                        matches["name"].Value, 
                        int.Parse(matches["pressure"].Value), 
                        matches["valves"].Value.Split(", ", StringSplitOptions.RemoveEmptyEntries).ToList());                
                })
                .ToList();

            // Starting point is AA

            valves = valves
                .OrderBy(x => x.Name)
                .ToList();

            return "";
        }

        public override string SolvePart2()
        {
            return "";
        }
    }

    record Valve(string Name, int Pressure, List<string> Tunnels)
    {

    }
}
