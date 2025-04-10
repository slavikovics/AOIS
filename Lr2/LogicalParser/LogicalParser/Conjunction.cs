namespace LogicalParser;

public class Conjunction: IEvaluatable
{
    public IEvaluatable LeftSide { get; set; }
    
    public IEvaluatable RightSide { get; set; }

    public Conjunction(IEvaluatable leftSide, IEvaluatable rightSide)
    {
        LeftSide = leftSide;
        RightSide = rightSide;
    }
    
    public bool Evaluate(Dictionary<string, bool> variables)
    {
        return LeftSide.Evaluate(variables) && RightSide.Evaluate(variables);
    }
    
    public override string ToString()
    {
        return $"({LeftSide}&{RightSide})";
    }
}