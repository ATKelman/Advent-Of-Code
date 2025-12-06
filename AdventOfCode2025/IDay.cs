namespace AdventOfCode2025;

public interface IDay
{
    int Year { get; }
    int Day { get; }
    Task LoadInputAsync();
    string SolvePart1();
    string SolvePart2();
}
