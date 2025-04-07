namespace LogicalParser;

public class False: IEvaluatable
{
    public bool Evaluate(Dictionary<string, bool> variables)
    {
        return false;
    }

    public override string ToString()
    {
        return "0";
    }
}