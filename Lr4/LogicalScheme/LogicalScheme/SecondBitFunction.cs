using AOISLaboratoryWork1;

namespace LogicalScheme;

public class SecondBitFunction : BitFunction
{
    public SecondBitFunction(bool undefined1, bool undefined2) : base(undefined1, undefined2)
    {
    }

    public override bool Evaluate(Dictionary<string, bool> variables)
    {
        string argsSet = BoolToString(variables["b"]) + BoolToString(variables["c"]) +
                         BoolToString(variables["d"]);

        Value = Binary.ConvertBinaryToInteger(argsSet);

        switch (Value)
        {
            case 0: return false;
            case 1: return false;
            case 2: return true;
            case 3: return true;
            case 4: return true;
            case 5: return true;
            case 6: return false;
            case 7: return false;
            case 8: return Undefined1;
            case 9: return Undefined2;
        }

        throw new Exception("Can't evaluate BitFunction");
    }
}