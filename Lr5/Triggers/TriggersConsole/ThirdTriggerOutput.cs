using LogicalParser;

namespace TriggersConsole;

public class ThirdTriggerOutput : IEvaluatable
{
    private bool _initial = false;
    
    private ThirdTriggerInput _input = new ThirdTriggerInput();
    
    public bool Evaluate(Dictionary<string, bool> variables)
    {
        bool result = _initial;
        if (_input.Evaluate(variables)) _initial = !_initial;
        return result;
    }

    public override string ToString()
    {
        return "T3O";
    }
}