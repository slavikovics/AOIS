﻿using AOISLaboratoryWork1;

namespace LogicalScheme;

public class ThirdBitFunction : BitFunction
{
    public ThirdBitFunction(bool undefined1, bool undefined2) : base(undefined1, undefined2)
    {
    }

    public override bool Evaluate(Dictionary<string, bool> variables)
    {
        string argsSet = BoolToString(variables["b"]) + BoolToString(variables["c"]) +
                         BoolToString(variables["d"]);

        Value = Binary.ConvertBinaryToInteger(argsSet);

        switch (Value)
        {
            case 0: return true;
            case 1: return true;
            case 2: return false;
            case 3: return false;
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