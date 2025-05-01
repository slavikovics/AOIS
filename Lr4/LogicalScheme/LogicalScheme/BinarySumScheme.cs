using LogicalParser;

namespace LogicalScheme;

public class BinarySumScheme : IEvaluatable
{
    public bool Evaluate(Dictionary<string, bool> variables)
    {
        int sum = Sum(variables["a"], variables["b"], variables["c"]);
        return sum % 2 == 1;
    }

    private int Sum(bool a, bool b, bool c)
    {
        return BoolToInt(a) + BoolToInt(b) + BoolToInt(c);
    }

    private int BoolToInt(bool value)
    {
        if (value) return 1;
        return 0;
    }

    public static List<string> GetVariables()
    {
        return ["a", "b", "c"];
    }

    public override string ToString()
    {
        return "S";
    }
}