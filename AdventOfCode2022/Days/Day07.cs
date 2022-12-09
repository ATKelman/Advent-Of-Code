using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Schema;

namespace AdventOfCode2022.Days
{
    internal class Day07 : DayBase
    {
        private Dir root = new("/");
        private List<Dir> directories ;

        public Day07(int day)
        : base(day)
        {
            var input = File.ReadAllLines(_inputPath);

            directories = new() { root };
            Dir currentDir = root;
            for (int i = 0; i < input.Length; i++)
            {
                var values = input[i].Split(' ');
                switch (values[1])
                {
                    case "cd":
                        currentDir = values[2] switch
                        {
                            "/" => root,
                            ".." => currentDir.Parent ?? currentDir,
                            _ => currentDir.Children.First(x => x.Name == values[2])
                        };
                        break;
                    case "ls":
                        var listedValues = input.Skip(i + 1).TakeWhile(x => !x.StartsWith("$"));
                        foreach (var item in listedValues)
                        {
                            var instance = item.Split(' ');
                            if (instance[0] == "dir")
                            {
                                Dir newDir = new(instance[1], currentDir);
                                currentDir.Children.Add(newDir);
                                directories.Add(newDir);
                            }
                            else
                            {
                                currentDir.Files.Add(new(int.Parse(instance[0]), instance[1]));
                            }
                        }

                        i += listedValues.Count();
                        break;
                }
            }
        }

        public override string SolvePart1()
        {                
            return directories.Where(x => x.Size <= 100000).Sum(x => x.Size).ToString();
        }

        public override string SolvePart2()
        {
            var totalSpace = 70000000;
            var requiredSpace = 30000000;
            
            return directories
                .OrderBy(x => x.Size)
                .First(x => x.Size >=(requiredSpace - (totalSpace - root.Size))).Size
                .ToString();
        }
    }

    public class Dir
    {
        public string Name { get; set; }
        public Dir? Parent { get; set; }
        public List<Dir> Children { get; set; } = new();
        public List<(int size, string name)> Files { get; set; } = new();
        public long Size => Files.Sum(x => x.size) + Children.Sum(x => x.Size);

        public Dir(string name, Dir parent = null)
        {
            Name = name;
            Parent = parent;
        }
    }
}
