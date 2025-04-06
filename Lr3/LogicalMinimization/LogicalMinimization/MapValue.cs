namespace LogicalMinimization;

public class MapValue
{
    public bool Value { get; set; }

    public bool WasUsed { get; set; }

    public MapValue(bool value)
    {
        Value = value;
        WasUsed = false;
    }

    public MapValue()
    {
        Value = false;
        WasUsed = false;
    }

    public override string ToString()
    {
        if (Value) return "1";
        return "0";
    }
}