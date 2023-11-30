using Microsoft.Extensions.Configuration;

namespace AdventOfCode2023.Days;

public abstract class DayBase : IDay
{
    private int _year;
    private int _day;

    private string _session;
    private string _baseUrl;

    private string _inputPath = "";
    private string _testInputPath = "";


    public DayBase(
        IConfiguration config, 
        int year, 
        int day, 
        bool isTest = false)
    {
        _session = config.GetValue<string>("Session") ?? throw new Exception("Session not found");
        _baseUrl = config.GetValue<string>("Url") ?? throw new Exception("Base Url not found");

        _year = year;
        _day = day;

        var binDir = Environment.CurrentDirectory;
        string path = $"{Directory.GetParent(binDir)?.Parent?.Parent?.FullName}\\Inputs\\";
        _testInputPath = $"{path}Day{_day}-test.txt";
        _inputPath = (isTest) ? _testInputPath : $"{path}Day{_day}.txt";
    }

    public async Task LoadInput()
    {
        if (!File.Exists(_testInputPath))
            File.Create(_testInputPath);

        if (File.Exists(_inputPath))
            return;        

        HttpClient httpClient = new();
        httpClient.DefaultRequestHeaders.Add("cookie", "session=" + _session);
        httpClient.DefaultRequestHeaders.UserAgent.ParseAdd(".NET");
        httpClient.BaseAddress = new Uri(_baseUrl);
        
        HttpResponseMessage response = await httpClient.GetAsync($"/{_year}/day/{_day}/input");
        Stream stream = await response.Content.ReadAsStreamAsync();

        using var file = File.Create(_inputPath);
        CopyStream(stream, file);
    }

    public void CopyStream(Stream input, Stream output)
    {
        byte[] buffer = new byte[8 * 1024];
        int len;
        while ((len = input.Read(buffer, 0, buffer.Length)) > 0)
            output.Write(buffer, 0, len);
    }

    public abstract string SolvePart1();
    public abstract string SolvePart2();
}
