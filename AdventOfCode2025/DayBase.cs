using Microsoft.Extensions.Configuration;
using System.Text.RegularExpressions;

namespace AdventOfCode2025;

public abstract class DayBase : IDay
{
    private readonly string _session;
    private readonly string _baseUrl;
    private readonly string _inputPath;

    protected string[] InputLines { get; private set; } = Array.Empty<string>();
    protected string InputText { get; private set; } = string.Empty;

    public int Year { get; }
    public int Day { get; }

    protected DayBase(IConfiguration config)
    {
        _session = config.GetValue<string>("Session") ?? throw new Exception("Session not found in configuration");
        _baseUrl = config.GetValue<string>("Url") ?? throw new Exception("Base URL not found in configuration");

        var type = GetType();
        Year = ExtractYearFromNamespace(type.Namespace);
        Day = ExtractDayFromClassName(type.Name);

        var projectRoot = GetProjectRoot();
        _inputPath = Path.Combine(projectRoot, "Inputs", $"Day{Day}.txt");
    }

    private static int ExtractYearFromNamespace(string? namespaceName)
    {
        if (string.IsNullOrEmpty(namespaceName))
            throw new Exception("Namespace is required to extract year");

        var match = Regex.Match(namespaceName, @"AdventOfCode(\d{4})");
        if (!match.Success)
            throw new Exception($"Could not extract year from namespace: {namespaceName}");

        return int.Parse(match.Groups[1].Value);
    }

    private static int ExtractDayFromClassName(string className)
    {
        var match = Regex.Match(className, @"Day(\d+)");
        if (!match.Success)
            throw new Exception($"Could not extract day from class name: {className}. Expected format: Day1, Day2, etc.");

        return int.Parse(match.Groups[1].Value);
    }

    private static string GetProjectRoot()
    {
        var currentDir = new DirectoryInfo(Environment.CurrentDirectory);

        while (currentDir != null)
        {
            if (currentDir.GetFiles("*.csproj").Length > 0)
            {
                return currentDir.FullName;
            }

            currentDir = currentDir.Parent;
        }

        throw new Exception("Could not find project root directory");
    }

    public async Task LoadInputAsync()
    {
        if (!File.Exists(_inputPath))
        {
			await DownloadInputAsync();
		}

        InputText = await File.ReadAllTextAsync(_inputPath);
        InputLines = [.. (await File.ReadAllLinesAsync(_inputPath)).Where(line => !string.IsNullOrWhiteSpace(line))];
    }

    private async Task DownloadInputAsync()
    {
        Console.WriteLine($"Downloading input for Day {Day}...");

        using var httpClient = new HttpClient();
        httpClient.DefaultRequestHeaders.Add("cookie", "session=" + _session);
        httpClient.DefaultRequestHeaders.UserAgent.ParseAdd(".NET");
        httpClient.BaseAddress = new Uri(_baseUrl);

        var response = await httpClient.GetAsync($"/{Year}/day/{Day}/input");

        if (!response.IsSuccessStatusCode)
        {
            throw new Exception($"Failed to download input: {response.StatusCode}");
        }

        var content = await response.Content.ReadAsStringAsync();

        var inputDir = Path.GetDirectoryName(_inputPath);
        if (inputDir != null && !Directory.Exists(inputDir))
        {
            Directory.CreateDirectory(inputDir);
        }

        await File.WriteAllTextAsync(_inputPath, content);
        Console.WriteLine($"Input saved to {_inputPath}");
    }

    public abstract string SolvePart1();
    public abstract string SolvePart2();
}
