using LogicalParser;

namespace TriggersConsole;

public class FirstTriggerOutput : IEvaluatable
{
    private bool _initial = true;
    
    public bool Evaluate(Dictionary<string, bool> variables)
    { 
        _initial = !_initial;
        return _initial;
    }

    public override string ToString()
    {
        return "T1O";
    }
}