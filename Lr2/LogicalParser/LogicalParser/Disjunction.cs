namespace LogicalParser;

public class Disjunction: IEvaluatable
{
    public IEvaluatable LeftSide { get; set; }
    
    public IEvaluatable RightSide { get; set; }

    public Disjunction(IEvaluatable leftSide, IEvaluatable rightSide)
    {
        LeftSide = leftSide;
        RightSide = rightSide;
    }
    
    public bool Evaluate(Dictionary<string, bool> variables)
    {
        return LeftSide.Evaluate(variables) || RightSide.Evaluate(variables);
    }
    
    public override string ToString()
    {
        return LeftSide.ToString() + RightSide.ToString() + $"({LeftSide.ToString()} | {RightSide.ToString()})";
    }
}