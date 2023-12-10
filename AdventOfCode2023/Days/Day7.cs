using Microsoft.Extensions.Configuration;

namespace AdventOfCode2023.Days;
public class Day7 : DayBase
{
    private string cardOrderPartOne = "AKQJT98765432";
    private string CardOrderPartTwo = "AKQT98765432J";

    public Day7(IConfiguration config, bool isTest)
        : base(config, 2023, 7, isTest)
    {

    }

    public override string SolvePart1()
    {
        var hands = File.ReadAllLines(_inputPath)
            .Select(x =>
            {
                var instance = x.Split(" ");
                return new Hand(cardOrderPartOne)
                {
                    Cards = instance[0],
                    Bid = int.Parse(instance[1]),
                    Type = CalculateType(instance[0])
                };
            })
            .ToList()
            .OrderBy(x => x.Type)
            .ThenBy(x => x.CardStrength[0])
            .ThenBy(x => x.CardStrength[1])
            .ThenBy(x => x.CardStrength[2])
            .ThenBy(x => x.CardStrength[3])
            .ThenBy(x => x.CardStrength[4])
            .ToArray();

        long result = 0;
        for (int i = 0; i < hands.Length; i++)
            result += hands[i].Bid * (hands.Length - i);
        
        return result.ToString();
    }

    public override string SolvePart2()
    {
        var hands = File.ReadAllLines(_inputPath)
           .Select(x =>
           {
               var instance = x.Split(" ");
               return new Hand(CardOrderPartTwo)
               {
                   Cards = instance[0],
                   Bid = int.Parse(instance[1]),
                   Type = CalculateType(instance[0], true)
               };
           })
           .ToList()
           .OrderBy(x => x.Type)
           .ThenBy(x => x.CardStrength[0])
           .ThenBy(x => x.CardStrength[1])
           .ThenBy(x => x.CardStrength[2])
           .ThenBy(x => x.CardStrength[3])
           .ThenBy(x => x.CardStrength[4])
           .ToArray();

        long result = 0;
        for (int i = 0; i < hands.Length; i++)
            result += hands[i].Bid * (hands.Length - i);

        return result.ToString();
    }

    private HandType CalculateType(string hand)
    {
        var groups = hand
            .ToCharArray()
            .GroupBy(x => x)
            .ToList();

        return groups.Count switch
        {
            1 => HandType.FiveOfAKind,
            2 => (groups.Any(x => x.Count() == 4))
                ? HandType.FourOfAKind : HandType.FullHouse,
            3 => (groups.Any(x => x.Count() == 3)) 
                ? HandType.ThreeOfAKind : HandType.TwoPair,
            4 => HandType.OnePair,
            5 => HandType.HighCard,
            _ => HandType.Error
        };
    }

    private HandType CalculateType(string hand, bool jokerRule = false)
    {
        var groups = hand
            .ToCharArray()
            .GroupBy(x => x)
            .ToDictionary(x => x.Key, x => x.Count());

        // No group of 5 and a joker exists 
        if (jokerRule 
            && groups.TryGetValue('J', out int count) 
            && !groups.Any(x => x.Value == 5))
        {
            var topGroup = groups
                .Where(x => x.Key != 'J')
                .OrderByDescending(x => x.Value)
                .First();
            groups[topGroup.Key] = topGroup.Value + count;
            groups.Remove('J');
        }

        return groups.Count switch
        {
            1 => HandType.FiveOfAKind,
            2 => (groups.Any(x => x.Value == 4))
                ? HandType.FourOfAKind : HandType.FullHouse,
            3 => (groups.Any(x => x.Value == 3))
                ? HandType.ThreeOfAKind : HandType.TwoPair,
            4 => HandType.OnePair,
            5 => HandType.HighCard,
            _ => HandType.Error
        };
    }

    private class Hand (string CardOrder)
    {
        public string Cards { get; set; } = "";
        public int Bid { get; set; }
        public HandType Type { get; set; }
        public int[] CardStrength => Cards.Select(x => CardOrder.IndexOf(x)).ToArray();
    }

    private enum HandType
    {
        FiveOfAKind,
        FourOfAKind,
        FullHouse,
        ThreeOfAKind,
        TwoPair,
        OnePair,
        HighCard,
        Error
    }
}
