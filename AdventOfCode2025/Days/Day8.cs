using Microsoft.Extensions.Configuration;

namespace AdventOfCode2025.Days;

public class Day8 (IConfiguration config)
    : DayBase(config)
{
    private const int PAIRS = 1000;

    public override string SolvePart1()
    {
        var junctions = InputLines
            .Select(x => x.Split(",").Select(long.Parse).ToArray())
            .Select(x => new Junction(x[0], x[1], x[2]))
            .ToArray();

        List<Distance> distances = [];

        for (int i = 0; i < junctions.Length; i++)
        {
            var currentJunction = junctions[i];

            for (int j = i + 1; j < junctions.Length; j++)
            {
                var nextJunction = junctions[j];
                var distance = CalculateDistance(currentJunction, nextJunction);
                distances.Add(new Distance(currentJunction, nextJunction, distance));
			}
		}

		List<List<Junction>> circuits = [];
		Dictionary<Junction, List<Junction>> junctionToCircuit = [];

        var shortestDistances = distances.OrderBy(x => x.Value).Take(PAIRS);
        foreach (var distance in shortestDistances)
        {
			var hasFrom = junctionToCircuit.TryGetValue(distance.From, out var fromCircuit);
			var hasTo = junctionToCircuit.TryGetValue(distance.To, out var toCircuit);

			switch (hasFrom, hasTo)
			{
                case (false, false):
					var newCircuit = new List<Junction> { distance.From, distance.To };
					circuits.Add(newCircuit);
					junctionToCircuit[distance.From] = newCircuit;
					junctionToCircuit[distance.To] = newCircuit;
					break;
				case (true, true) when fromCircuit == toCircuit:
					break;
				case (false, true):
					toCircuit!.Add(distance.From);
					junctionToCircuit[distance.From] = toCircuit;
					break;
				case (true, false):
					fromCircuit!.Add(distance.To);
					junctionToCircuit[distance.To] = fromCircuit;
					break;
				case (true, true):
					fromCircuit!.AddRange(toCircuit!);
					foreach (var junction in toCircuit!)
						junctionToCircuit[junction] = fromCircuit;
					circuits.Remove(toCircuit);
					break;
			}
        }

		return circuits.OrderByDescending(x => x.Count)
            .Take(3)
            .Aggregate(1, (a, b) => a * b.Count)
            .ToString();
    }

    public override string SolvePart2()
    {
		var junctions = InputLines
			.Select(x => x.Split(",").Select(long.Parse).ToArray())
			.Select(x => new Junction(x[0], x[1], x[2]))
			.ToArray();

		List<Distance> distances = [];

		for (int i = 0; i < junctions.Length; i++)
		{
			var currentJunction = junctions[i];

			for (int j = i + 1; j < junctions.Length; j++)
			{
				var nextJunction = junctions[j];
				var distance = CalculateDistance(currentJunction, nextJunction);
				distances.Add(new Distance(currentJunction, nextJunction, distance));
			}
		}

		List<List<Junction>> circuits = [];
		Dictionary<Junction, List<Junction>> junctionToCircuit = [];
		var shortestDistances = distances.OrderBy(x => x.Value);
		foreach (var distance in shortestDistances)
		{
			var hasFrom = junctionToCircuit.TryGetValue(distance.From, out var fromCircuit);
			var hasTo = junctionToCircuit.TryGetValue(distance.To, out var toCircuit);

			switch (hasFrom, hasTo)
			{
				case (false, false):
					var newCircuit = new List<Junction> { distance.From, distance.To };
					circuits.Add(newCircuit);
					junctionToCircuit[distance.From] = newCircuit;
					junctionToCircuit[distance.To] = newCircuit;
					break;
				case (true, true) when fromCircuit == toCircuit:
					break;
				case (false, true):
					toCircuit!.Add(distance.From);
					junctionToCircuit[distance.From] = toCircuit;
					break;
				case (true, false):
					fromCircuit!.Add(distance.To);
					junctionToCircuit[distance.To] = fromCircuit;
					break;
				case (true, true):
					fromCircuit!.AddRange(toCircuit!);
					foreach (var junction in toCircuit!)
						junctionToCircuit[junction] = fromCircuit;
					circuits.Remove(toCircuit);
					break;
			}

			if (circuits.OrderByDescending(x => x.Count).First().Count == junctions.Length)
				return (distance.From.X * distance.To.X).ToString();			
		}

		return "Failed to find a singular circuit";
	}

    private static double CalculateDistance(Junction j1, Junction j2)
    {
        return Math.Sqrt(Math.Pow(Math.Abs(j1.X - j2.X), 2) + Math.Pow(Math.Abs(j1.Y - j2.Y), 2) + Math.Pow(Math.Abs(j1.Z - j2.Z), 2));
    }

    private record Junction(long X, long Y, long Z);
    private record Distance(Junction From, Junction To, double Value);
}
