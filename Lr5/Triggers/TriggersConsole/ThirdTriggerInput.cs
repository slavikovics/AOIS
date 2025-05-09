using LogicalParser;

namespace TriggersConsole;

public class ThirdTriggerInput : IEvaluatable
{
    private bool _isOnTheSecondTick = false;
    
    public bool Evaluate(Dictionary<string, bool> variables)
    {
        bool prevTriggered = variables["a"];
        
        if (prevTriggered && !_isOnTheSecondTick)
        {
            _isOnTheSecondTick = prevTriggered;
            prevTriggered = false;
        }

        if (prevTriggered && _isOnTheSecondTick)
        {
            _isOnTheSecondTick = false;
            return true;
        }
        return false;
    }

    public override string ToString()
    {
        return "T3I";
    }
}