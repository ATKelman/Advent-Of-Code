using Microsoft.Extensions.Configuration;

namespace AdventOfCode2025.Days;

public class Day7 (IConfiguration config)
    : DayBase(config)
{
    public override string SolvePart1()
    {
        var splits = 0;
        var startIndex = InputLines[0].IndexOf('S');
        HashSet<int> beams = [ startIndex ];
        for (int i = 1; i < InputLines.Length; i++)
        {
            HashSet<int> previousBeams = beams;
			beams = [];
            
            var splitters = AllIndexOf(InputLines[i], "^");
            foreach (var beam in previousBeams)
            {
                if (splitters.Contains(beam))
                {
                    beams.Add(beam + 1);
                    beams.Add(beam - 1);
                    splits++;
                }
                else
                    beams.Add(beam);
            }
        }
        return splits.ToString();
    }

    public override string SolvePart2()
    {
		var startIndex = InputLines[0].IndexOf('S');
		
        Dictionary<int, long> beams = [];
        beams.Add(startIndex, 1);
		for (int i = 2; i < InputLines.Length; i+=2)
		{
			Dictionary<int, long> previousBeams = beams;
			beams = [];

			var splitters = AllIndexOf(InputLines[i], "^");
			foreach (var beam in previousBeams)
			{
                if (splitters.Contains(beam.Key))
                {
                    beams[(beam.Key + 1)] = beams.GetValueOrDefault(beam.Key + 1) + beam.Value;
                    beams[(beam.Key - 1)] = beams.GetValueOrDefault(beam.Key - 1) + beam.Value;
                }
                else
                    beams[beam.Key] = beams.GetValueOrDefault(beam.Key) + beam.Value;
			}
		}
		return beams.Sum(x => x.Value).ToString();
	}

    private static IEnumerable<int> AllIndexOf(string str, string searchString)
    {
        int minIndex = str.IndexOf(searchString);
        while (minIndex != -1)
        {
            yield return minIndex;
            minIndex = str.IndexOf(searchString, minIndex + searchString.Length);
        }
    }
}
