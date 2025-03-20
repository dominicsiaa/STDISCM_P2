enum DungeonStatus
{
    Empty,
    Active
}

class Dungeon
{
    private uint _id;
    private uint MinClearTime { get; }
    private uint MaxClearTime { get; }
    private DungeonStatus Status { get; set; } = DungeonStatus.Empty;
    private Party? CurrentParty { get; set; } = null;
    private uint PartiesServed { get; set; } = 0;
    private uint TotalTimeServed { get; set; } = 0;
    private Thread? thread;

    public Dungeon(uint id, uint minClearTime, uint maxClearTime)
    {
        _id = id;
        MinClearTime = minClearTime;
        MaxClearTime = maxClearTime;
    }

    public DungeonStatus GetStatus()
    {
        return Status;
    }

    public void PrintStatus()
    {
        Console.WriteLine($"Dungeon Status: {Status}");
    }

    public void StartDungeon(Party party)
    {
        if (Status != DungeonStatus.Empty)
        {
            Console.WriteLine("Dungeon is currently occupied.");
            return;
        }

        Status = DungeonStatus.Active;
        CurrentParty = party;
        thread = new Thread(RunDungeon);
        thread.Start();
    }

    private void RunDungeon()
    {
        if (CurrentParty == null)
        {
            Console.WriteLine("Error: No party found.");
            Status = DungeonStatus.Empty;
            return;
        }

        Console.WriteLine($"Starting Dungeon for {CurrentParty}");
        uint clearTime = (uint)new Random().Next((int)MinClearTime, (int)MaxClearTime + 1);
        Thread.Sleep((int)clearTime * 1000);
        Console.WriteLine($"Dungeon Completed for {CurrentParty} in {clearTime} seconds.");

        PartiesServed++;
        TotalTimeServed += clearTime;
        Status = DungeonStatus.Empty;
        CurrentParty = null;
    }

    public void PrintStatistics()
    {
        Console.WriteLine($"[Dungeon {_id}] Parties Served: {PartiesServed}, Total Time Served: {TotalTimeServed} seconds");
    }
}