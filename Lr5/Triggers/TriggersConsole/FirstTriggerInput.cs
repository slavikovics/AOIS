using LogicalParser;

namespace TriggersConsole;

public class FirstTriggerInput : IEvaluatable
{
    public bool Evaluate(Dictionary<string, bool> variables)
    {
        return true;
    }

    public override string ToString()
    {
        return "T1I";
    }
}