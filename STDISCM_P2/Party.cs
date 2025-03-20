public struct Party
{
    public string Tank { get; set; }
    public string Healer { get; set; }
    public string DPS1 { get; set; }
    public string DPS2 { get; set; }
    public string DPS3 { get; set; }

    public Party(string tank, string healer, string dps1, string dps2, string dps3)
    {
        Tank = tank;
        Healer = healer;
        DPS1 = dps1;
        DPS2 = dps2;
        DPS3 = dps3;
    }

    public override string ToString()
    {
        return $"Party: {{ {Tank}, {Healer}, {DPS1}, {DPS2}, {DPS3} }}";
    }
}
