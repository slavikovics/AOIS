using LogicalParser;

namespace TriggersConsole;

public class FourthTriggerOutput : IEvaluatable
{
    private bool _initial = true;
    
    private FourthTriggerInput _input = new ();
    
    public bool Evaluate(Dictionary<string, bool> variables)
    {
        if (_input.Evaluate(variables)) _initial = !_initial;
        return _initial;
    }

    public override string ToString()
    {
        return "T4O";
    }
}