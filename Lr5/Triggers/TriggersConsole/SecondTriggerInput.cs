using LogicalParser;

namespace TriggersConsole;

public class SecondTriggerInput : IEvaluatable
{
    public bool Evaluate(Dictionary<string, bool> variables)
    {
        return variables["a"];
    }

    public override string ToString()
    {
        return "T2I";
    }
}