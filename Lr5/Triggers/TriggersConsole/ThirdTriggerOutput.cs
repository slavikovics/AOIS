using LogicalParser;

namespace TriggersConsole;

public class ThirdTriggerOutput : IEvaluatable
{
    private bool _initial = true;
    
    private ThirdTriggerInput _input = new ThirdTriggerInput();
    
    public bool Evaluate(Dictionary<string, bool> variables)
    {
        if (_input.Evaluate(variables)) _initial = !_initial;
        return _initial;
    }

    public override string ToString()
    {
        return "T3O";
    }
}