using LogicalParser;

namespace TriggersConsole;

public class FourthTriggerInput : IEvaluatable
{
    private ThirdTriggerInput _thirdTriggerInput = new ThirdTriggerInput();
    
    private bool _isOnTheSecondTick = true;
    
    public bool Evaluate(Dictionary<string, bool> variables)
    {
        bool prevTriggered = _thirdTriggerInput.Evaluate(variables);
        
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
        return "T4I";
    }
}