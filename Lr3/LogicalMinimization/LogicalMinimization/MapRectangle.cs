namespace LogicalMinimization;

public class MapRectangle
{
    public int X { get; private set; }

    public int Y { get; private set; }

    public bool WasUsed { get; private set; }

    public Dictionary<string, bool> Match { get; set; }

    public MapRectangle(int x, int y, bool wasUsed)
    {
        X = x;
        Y = y;
        WasUsed = wasUsed;
    }
}