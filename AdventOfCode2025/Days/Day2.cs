using Microsoft.Extensions.Configuration;

namespace AdventOfCode2025.Days;

public class Day2 (IConfiguration config)
    : DayBase(config)
{
    public override string SolvePart1()
    {
        var invalIds = InputText
            .Split(',')
            .Select(x =>
            {
                var split = x.Split('-');
                var start = long.Parse(split[0]);
                var end = long.Parse(split[1]);

                int startLen = start.ToString().Length;
                int endLen = end.ToString().Length;
                List<long> instances = new();

                for (int i = startLen; i <= endLen; i++)
                {
                    if (i % 2 != 0)
                        continue;

                    int halfLen = i / 2;
                    long minHalf = (long)Math.Pow(10, halfLen - 1);
                    long maxHalf = (long)Math.Pow(10, halfLen) - 1;

                    for (long half = minHalf; half <= maxHalf; half++)
                    {
                        string halfStr = half.ToString();
                        long instance = long.Parse(halfStr + halfStr);

                        if (instance >= start && instance <= end)
							instances.Add(instance);

                        if (instance > end)
                            break;
					}
				}

                return instances;
			})
            .ToList();

        return invalIds.Sum(x => x.Sum()).ToString();
	}

    public override string SolvePart2()
    {
		var invalIds = InputText
			.Split(',')
			.Select(x =>
			{
				var split = x.Split('-');
				var start = long.Parse(split[0]);
				var end = long.Parse(split[1]);

				int startLen = start.ToString().Length;
				int endLen = end.ToString().Length;
				List<long> instances = new();

				for (long num = start; num <= end; num++)
				{
                    string str = num.ToString();
                    int len = str.Length;
					foreach (int divisor in GetDivisors(len))
                    {
                        int patternLength = len / divisor;
						var parts = str.Chunk(patternLength)
			               .Select(chars => new string(chars))
			               .ToList();
						if (parts.All(x => x == parts.First()))
                        {
                            instances.Add(num);
                            break;
                        }
					}
				}

				return instances;
			})
			.ToList();

		return invalIds.Sum(x => x.Sum()).ToString();
	}

	static IEnumerable<int> GetDivisors(int n)
	{
		for (int patternLen = 2; patternLen <= n; patternLen++) 
		{
			if (n % patternLen == 0)
				yield return patternLen;
		}
	}
}
