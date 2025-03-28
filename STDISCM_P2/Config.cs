class Config
{
    public uint n { get; private set; }
    public uint t { get; private set; }
    public uint h { get; private set; }
    public uint d { get; private set; }
    public uint t1 { get; private set; }
    public uint t2 { get; private set; }

    public static Config Load(string filePath)
    {
        var config = new Config();

        foreach (var line in File.ReadAllLines(filePath))
        {
            var parts = line.Split('=');
            if (parts.Length != 2) continue;

            var key = parts[0].Trim();
            var value = parts[1].Trim();

            try
            {
                if (!uint.TryParse(value, out uint parsedValue))
                {
                    FatalError($"{key.ToUpper()} must be an unsigned integer");
                }

                switch (key.ToLower())
                {
                    case "n":
                        if (parsedValue < 1 || parsedValue > 1000)
                        {
                            FatalError("N must be between 1 and 1000");
                        }
                        config.n = parsedValue;
                        break;
                    case "t":
                        if (parsedValue > 5000000)
                        {
                            FatalError("T must be less than 5000000");
                        }
                        config.t = parsedValue;
                        break;
                    case "h":
                        if (parsedValue > 5000000)
                        {
                            FatalError("H must be less than 5000000");
                        }
                        config.h = parsedValue;
                        break;
                    case "d":
                        if (parsedValue > 5000000)
                        {
                            FatalError("D must be less than 5000000");
                        }
                        config.d = parsedValue;
                        break;
                    case "t1":
                        if (parsedValue > 15 || parsedValue < 1)
                        {
                            FatalError("T1 must be between 1 and 15");
                        }
                        config.t1 = parsedValue;
                        break;
                    case "t2":
                        if (parsedValue < config.t1)
                        {
                            FatalError("T2 must be greater than or equal to T1");
                        }

                        if (parsedValue > 15 || parsedValue < 1)
                        {
                            FatalError("T2 must be between 1 and 15");
                        }
                        config.t2 = parsedValue;
                        break;
                    default:
                        FatalError($"Unknown key '{key}'");
                        break;
                }
            }
            catch (Exception ex)
            {
                FatalError($"Unexpected error parsing key '{key}': {ex.Message}");
            }
        }

        return config;
    }

    public void PrintVariables()
    {
        Console.WriteLine("Input Variables:");
        Console.WriteLine("N: " + n);
        Console.WriteLine("T: " + t);
        Console.WriteLine("H: " + h);
        Console.WriteLine("D: " + d);
        Console.WriteLine("T1: " + t1);
        Console.WriteLine("T2: " + t2);
    }

    private static void FatalError(string message)
    {
        Console.WriteLine($"[ERROR] {message}");
        Environment.Exit(1);
    }
}
