// See https://aka.ms/new-console-template for more information
using AdventOfCode2023.Days;
using Microsoft.Extensions.Configuration;

try
{
    var builder = new ConfigurationBuilder()
        .SetBasePath(Directory.GetCurrentDirectory())
        .AddJsonFile("appsettings.json", optional: false);

    IConfiguration config = builder.Build();

    Console.WriteLine("Advent of Code 2023");
    string key = Console.ReadLine();
    bool isTest = string.Equals(key.ToLower(), "t");

    // TODO fix so this is generated
    var day = new Day12(config, isTest);
    await day.LoadInput();

    Console.WriteLine("Part 1:" + day.SolvePart1());
    Console.WriteLine("Part 2:" + day.SolvePart2());
}
catch (Exception ex)
{
    Console.WriteLine(ex.Message);
}

Console.WriteLine("Press any key to exit.");
Console.ReadKey();
