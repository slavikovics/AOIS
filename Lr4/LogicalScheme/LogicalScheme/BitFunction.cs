using AOISLaboratoryWork1;
using LogicalParser;

namespace LogicalScheme;

public abstract class BitFunction : IEvaluatable
{
    public bool Undefined1;

    public bool Undefined2;

    public int Value;
    
    protected BitFunction(bool undefined1, bool undefined2)
    {
        Undefined1 = undefined1;
        Undefined2 = undefined2;
    }
    
    public virtual bool Evaluate(Dictionary<string, bool> variables)
    {
        string argsSet = BoolToString(variables["a"]) + BoolToString(variables["b"]) + BoolToString(variables["c"]) +
                         BoolToString(variables["d"]);

        Value = Binary.ConvertBinaryToInteger(argsSet);

        return true;
    }

    protected static string BoolToString(bool value)
    {
        if (value) return "1";
        return "0";
    }
}