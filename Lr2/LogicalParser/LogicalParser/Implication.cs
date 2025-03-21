namespace LogicalParser;

public class Implication: IEvaluatable
{
    public IEvaluatable LeftSide { get; set; }
    
    public IEvaluatable RightSide { get; set; }

    public Implication(IEvaluatable leftSide, IEvaluatable rightSide)
    {
        LeftSide = leftSide;
        RightSide = rightSide;
    }
    
    public bool Evaluate(Dictionary<string, bool> variables)
    {
        if (LeftSide.Evaluate(variables) && RightSide.Evaluate(variables) == false)
        {
            return false;
        }
        return true;
    }
    
    public override string ToString()
    {
        return LeftSide.ToString() + RightSide.ToString() + $"({LeftSide.ToString()} -> {RightSide.ToString()})";
    }
}