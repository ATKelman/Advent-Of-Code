using AdventOfCode2025;
using Microsoft.Extensions.Configuration;
using System.Reflection;

try
{
    var values = Environment.GetCommandLineArgs().Skip(1).ToArray();

    var builder = new ConfigurationBuilder()
        .SetBasePath(Directory.GetCurrentDirectory())
        .AddJsonFile("appsettings.json", optional: false);

    IConfiguration config = builder.Build();

    int? requestedDay = ParseDayFromArgs(values);
    int dayToRun = requestedDay ?? GetLatestDay();

    var day = LoadDay(dayToRun, config);

    if (day == null)
    {
        Console.WriteLine($"Day {dayToRun} not found. Available days: {string.Join(", ", GetAvailableDays())}");
        return;
    }

    Console.WriteLine($"=== Advent of Code 2025 - Day {day.Day} ===");
    Console.WriteLine();

    await day.LoadInputAsync();

    Console.WriteLine("Part 1: " + day.SolvePart1());
    Console.WriteLine("Part 2: " + day.SolvePart2());
}
catch (Exception ex)
{
    Console.WriteLine($"Error: {ex.Message}");
    Console.WriteLine();
    Console.WriteLine("Usage:");
    Console.WriteLine("  dotnet run              -> Runs the latest day");
    Console.WriteLine("  dotnet run -- 5         -> Runs day 5");
}

Console.WriteLine();
Console.WriteLine("Press any key to exit.");
Console.ReadKey();


static int? ParseDayFromArgs(string[] args)
{
    if (args.Length == 0)
        return null;

    if (int.TryParse(args[0], out int day))
        return day;

    return null;
}

static int GetLatestDay()
{
    var days = GetAvailableDays();
    return days.Any() ? days.Max() : 0;
}

static List<int> GetAvailableDays()
{
    var assembly = Assembly.GetExecutingAssembly();
    var dayTypes = assembly.GetTypes()
        .Where(t => typeof(IDay).IsAssignableFrom(t) && !t.IsInterface && !t.IsAbstract)
        .ToList();

    var days = new List<int>();
    foreach (var type in dayTypes)
    {
        var match = System.Text.RegularExpressions.Regex.Match(type.Name, @"Day(\d+)");
        if (match.Success)
        {
            days.Add(int.Parse(match.Groups[1].Value));
        }
    }

    return days.OrderBy(d => d).ToList();
}

static IDay? LoadDay(int dayNumber, IConfiguration config)
{
    var assembly = Assembly.GetExecutingAssembly();
    var dayType = assembly.GetTypes()
        .FirstOrDefault(t => typeof(IDay).IsAssignableFrom(t)
            && !t.IsInterface
            && !t.IsAbstract
            && t.Name == $"Day{dayNumber}");

    if (dayType == null)
        return null;

    return Activator.CreateInstance(dayType, config) as IDay;
}