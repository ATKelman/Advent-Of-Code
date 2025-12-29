using Microsoft.Extensions.Configuration;

namespace AdventOfCode2025.Days;

public class Day10(IConfiguration config)
	: DayBase(config)
{
	public override string SolvePart1()
	{
		var machines = InputLines.Select(x =>
		{
			var parts = x.Split(' ');
			return new Machine(parts[0], parts[1..^1], parts[^1]);
		});

		long totalButtonsPressed = 0;
		foreach (var machine in machines)
		{
			int minPresses = machine.FindMinimumLightPresses();
			totalButtonsPressed += minPresses;
		}

		return totalButtonsPressed.ToString();
	}

	public override string SolvePart2()
	{
		var machines = InputLines.Select(x =>
		{
			var parts = x.Split(' ');
			return new Machine(parts[0], parts[1..^1], parts[^1]);
		});

		long totalButtonsPressed = 0;
		foreach (var machine in machines)
		{
			long minPresses = machine.FindMinimumPressesForJoltage();
			totalButtonsPressed += minPresses;
		}

		return totalButtonsPressed.ToString();
	}

	private class Machine
	{
		public bool[] LightsGoal = [];
		public int[][] Buttons = [];
		public int[] JoltageGoals = [];

		public Machine(string lights, string[] buttons, string joltages)
		{
			LightsGoal = [.. lights.ToArray()[1..^1].Select(x => x == '#')];
			Buttons = [.. buttons.Select(x => x.Replace("(", "").Replace(")", "")
				.Split(',', StringSplitOptions.RemoveEmptyEntries)
				.Select(int.Parse).ToArray())];
			JoltageGoals = joltages.Replace("{", "").Replace("}", "")
				.Split(',', StringSplitOptions.RemoveEmptyEntries)
				.Select(int.Parse).ToArray();
		}

		public int FindMinimumLightPresses()
		{
			int numLights = LightsGoal.Length;
			int numButtons = Buttons.Length;

			int minPresses = int.MaxValue;

			for (int i = 0; i < (1 << numButtons); i++)
			{
				bool[] state = new bool[numLights];
				int presses = 0;

				for (int j = 0; j < numButtons; j++)
				{
					if ((i & (1 << j)) != 0)
					{
						presses++;
						foreach (var lightIndex in Buttons[j])
							state[lightIndex] = !state[lightIndex];
					}
				}

				if (state.SequenceEqual(LightsGoal))
					minPresses = Math.Min(minPresses, presses);
			}

			return minPresses;
		}

		public int FindMinimumPressesForJoltage()
		{
			int numCounters = JoltageGoals.Length;
			int numButtons = Buttons.Length;

			double[,] matrix = new double[numCounters, numButtons + 1];
			for (int btn = 0; btn < numButtons; btn++)
			{
				foreach (int counter in Buttons[btn])
				{
					matrix[counter, btn] = 1;
				}
			}
			for (int i = 0; i < numCounters; i++)
			{
				matrix[i, numButtons] = JoltageGoals[i];
			}

			int currentRow = 0;
			int[] pivotCols = new int[numCounters];
			Array.Fill(pivotCols, -1);

			for (int col = 0; col < numButtons && currentRow < numCounters; col++)
			{
				int pivotRow = -1;
				for (int row = currentRow; row < numCounters; row++)
				{
					if (Math.Abs(matrix[row, col]) > 0.0001)
					{
						pivotRow = row;
						break;
					}
				}

				if (pivotRow == -1) continue;

				if (pivotRow != currentRow)
				{
					for (int j = 0; j <= numButtons; j++)
					{
						(matrix[currentRow, j], matrix[pivotRow, j]) =
							(matrix[pivotRow, j], matrix[currentRow, j]);
					}
				}

				pivotCols[currentRow] = col;

				double pivot = matrix[currentRow, col];
				for (int j = 0; j <= numButtons; j++)
				{
					matrix[currentRow, j] /= pivot;
				}

				for (int row = 0; row < numCounters; row++)
				{
					if (row != currentRow && Math.Abs(matrix[row, col]) > 0.0001)
					{
						double factor = matrix[row, col];
						for (int j = 0; j <= numButtons; j++)
						{
							matrix[row, j] -= factor * matrix[currentRow, j];
						}
					}
				}

				currentRow++;
			}

			List<int> freeVars = new List<int>();
			HashSet<int> pivotSet = new HashSet<int>(pivotCols.Where(p => p != -1));

			for (int col = 0; col < numButtons; col++)
			{
				if (!pivotSet.Contains(col))
				{
					freeVars.Add(col);
				}
			}

			int maxVal = JoltageGoals.Max();
			int minTotalPresses = int.MaxValue;

			EnumerateSolutions(matrix, pivotCols, freeVars, 0, new int[numButtons],
							   numButtons, numCounters, maxVal, ref minTotalPresses);

			return minTotalPresses == int.MaxValue ? -1 : minTotalPresses;
		}

		private void EnumerateSolutions(double[,] matrix, int[] pivotCols, List<int> freeVars,
										int freeVarIdx, int[] solution, int numButtons,
										int numCounters, int maxVal, ref int minTotal)
		{
			if (freeVarIdx == freeVars.Count)
			{
				int[] fullSolution = (int[])solution.Clone();
				bool valid = true;

				for (int row = 0; row < numCounters; row++)
				{
					if (pivotCols[row] == -1) continue;

					int pivotCol = pivotCols[row];
					double rhs = matrix[row, numButtons];

					for (int col = 0; col < numButtons; col++)
					{
						if (col != pivotCol)
						{
							rhs -= matrix[row, col] * fullSolution[col];
						}
					}

					double value = rhs / matrix[row, pivotCol];

					int intValue = (int)Math.Round(value);
					if (Math.Abs(value - intValue) > 0.01)
					{
						valid = false;
						break;
					}

					if (intValue < 0)
					{
						valid = false;
						break;
					}

					fullSolution[pivotCol] = intValue;
				}

				if (valid)
				{
					int[] achieved = new int[numCounters];
					for (int btn = 0; btn < numButtons; btn++)
					{
						foreach (int c in Buttons[btn])
						{
							achieved[c] += fullSolution[btn];
						}
					}

					bool correct = true;
					for (int i = 0; i < numCounters; i++)
					{
						if (achieved[i] != JoltageGoals[i])
						{
							correct = false;
							break;
						}
					}

					if (correct)
					{
						int total = fullSolution.Sum();
						if (total < minTotal)
						{
							minTotal = total;
							Console.WriteLine($"Found solution with {total} presses: {string.Join(", ", fullSolution)}");
						}
					}
				}

				return;
			}

			int freeVar = freeVars[freeVarIdx];

			int upperBound = maxVal;

			for (int val = 0; val <= upperBound; val++)
			{
				solution[freeVar] = val;
				EnumerateSolutions(matrix, pivotCols, freeVars, freeVarIdx + 1,
								  solution, numButtons, numCounters, maxVal, ref minTotal);
			}
		}
	}
}
