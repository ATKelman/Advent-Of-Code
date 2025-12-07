using Microsoft.Extensions.Configuration;

namespace AdventOfCode2025.Days;

public class Day1(IConfiguration config)
	: DayBase(config)
{
	private const int ROTATION_START = 50;

	public override string SolvePart1()
	{
		int rotation = ROTATION_START;
		int[] dialValues = new int[InputLines.Length - 1];

		for (int i = 0; i < InputLines.Length - 1; i++)
		{
			var direction = InputLines[i][0] == 'R' ? 1 : -1;
			var value = int.Parse(InputLines[i].Substring(1)) * direction;
			rotation = ((rotation + value) % 100 + 100) % 100;

			dialValues[i] = rotation;
		}

		return dialValues
			.Where(x => x == 0)
			.Count()
			.ToString();
	}

	public override string SolvePart2()
	{
		int rotation = ROTATION_START;
		int zeros = 0;

		for (int i = 0; i < InputLines.Length; i++)
		{
			var direction = InputLines[i][0] == 'R' ? 1 : -1;
			var value = int.Parse(InputLines[i].Substring(1)) * direction;

			zeros += CountZeros(rotation, value);

			rotation = ((rotation + value) % 100 + 100) % 100;
		}

		return zeros.ToString();
	}

	private static int CountZeros(int startPos, int movement)
	{
		if (movement == 0)
			return 0;

		int absMovement = Math.Abs(movement);
		int distanceToZero;

		if (movement > 0)
			startPos = (100 - startPos);

		distanceToZero = startPos % 100;

		if (distanceToZero == 0)
			distanceToZero = 100;

		if (absMovement < distanceToZero)
			return 0;

		return 1 + (absMovement - distanceToZero) / 100;
	}
}
