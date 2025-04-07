namespace LogicalParser;

public class Truth: IEvaluatable
{
    public bool Evaluate(Dictionary<string, bool> variables)
    {
        return true;
    }

    public override string ToString()
    {
        return "1";
    }
}