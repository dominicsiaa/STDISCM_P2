class Program
{
    static void Main(string[] args)
    {
        Config config = Config.Load("config.txt");
        config.PrintVariables();

        Queue<Party> partyQueue = new Queue<Party>(CreateParties(config));
        List<Dungeon> dungeons = CreateDungeons(config);
        Queue<Dungeon> dungeonEmptyQueue = new Queue<Dungeon>(dungeons);
        List<Dungeon> dungeonActiveQueue = new List<Dungeon>();

        Console.WriteLine("Starting dungeon runs...\n");
        while (partyQueue.Count > 0 || dungeonActiveQueue.Count > 0)
        {
            for (int i = dungeonActiveQueue.Count - 1; i >= 0; i--)
            {
                if (dungeonActiveQueue[i].GetStatus() == DungeonStatus.Empty)
                {
                    dungeonEmptyQueue.Enqueue(dungeonActiveQueue[i]);
                    dungeonActiveQueue.RemoveAt(i);
                }
            }

            if (partyQueue.Count > 0 && dungeonEmptyQueue.Count > 0)
            {
                var dungeon = dungeonEmptyQueue.Dequeue();
                var party = partyQueue.Dequeue();
                dungeon.StartDungeon(party);
                dungeonActiveQueue.Add(dungeon);
            }
        }

        Console.WriteLine("\nAll parties completed.\n");

        Console.WriteLine("Dungeon Statistics:");
        foreach (var dungeon in dungeons)
        {
            dungeon.PrintStatistics();
        }
    }

    static List<Party> CreateParties(Config config)
    {
        List<Party> parties = new List<Party>();
        int tankCounter = 1, healerCounter = 1, dpsCounter = 1;
        uint tanksRemaining = config.t, healersRemaining = config.h, dpsRemaining = config.d;

        while (tanksRemaining >= 1 && healersRemaining >= 1 && dpsRemaining >= 3)
        {
            Party party = new Party(
                $"Tank {tankCounter++}",
                $"Healer {healerCounter++}",
                $"DPS {dpsCounter++}",
                $"DPS {dpsCounter++}",
                $"DPS {dpsCounter++}"
            );
            parties.Add(party);
            tanksRemaining--;
            healersRemaining--;
            dpsRemaining -= 3;
        }

        Console.WriteLine();
        if (tanksRemaining > 0 || healersRemaining > 0 || dpsRemaining > 0)
        {
            Console.WriteLine($"Leftover Players: {tanksRemaining} Tanks, {healersRemaining} Healers, {dpsRemaining} DPS");
        }
        else
        {
            Console.WriteLine("All players assigned to parties.");
        }

        return parties;
    }

    static List<Dungeon> CreateDungeons(Config config)
    {
        List<Dungeon> dungeons = new List<Dungeon>();
        for (uint i = 1; i <= config.n; i++)
        {
            dungeons.Add(new Dungeon(i, config.t1, config.t2));
        }
        Console.WriteLine($"Created {config.n} dungeons.\n");
        return dungeons;
    }
}
