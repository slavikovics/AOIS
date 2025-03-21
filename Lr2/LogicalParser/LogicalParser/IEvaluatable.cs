namespace LogicalParser;

public interface IEvaluatable
{
    public bool Evaluate(Dictionary<string, bool> variables);
}