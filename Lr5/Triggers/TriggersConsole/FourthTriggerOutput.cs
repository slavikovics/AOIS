using LogicalParser;

namespace TriggersConsole;

public class FourthTriggerOutput : IEvaluatable
{
    private bool _initial = false;
    
    private FourthTriggerInput _input = new ();
    
    public bool Evaluate(Dictionary<string, bool> variables)
    {
        bool result = _initial;
        if (_input.Evaluate(variables)) _initial = !_initial;
        return result;
    }

    public override string ToString()
    {
        return "T4O";
    }
}