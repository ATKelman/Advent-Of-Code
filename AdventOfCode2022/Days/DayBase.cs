using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2022.Days
{
    public abstract class DayBase : IDay
    {
        protected string _inputPath = "";

        public DayBase(int day)
        {
            var binDir = Environment.CurrentDirectory;
            _inputPath = $"{Directory.GetParent(binDir).Parent.Parent.FullName}\\Inputs\\Day{day}.txt";
        }

        public abstract string SolvePart1();
        public abstract string SolvePart2();
    }
}
