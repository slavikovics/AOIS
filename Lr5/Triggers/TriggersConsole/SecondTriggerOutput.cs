using LogicalParser;

namespace TriggersConsole;

public class SecondTriggerOutput : IEvaluatable
{
    private bool _initial = false;
    
    private SecondTriggerInput _input = new SecondTriggerInput();
    
    public bool Evaluate(Dictionary<string, bool> variables)
    {
        if (_input.Evaluate(variables)) _initial = !_initial;
        return _initial;
    }

    public override string ToString()
    {
        return "T2O";
    }
}