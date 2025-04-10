namespace LogicalParser;

public class Equivalence: IEvaluatable
{
    public IEvaluatable LeftSide { get; set; }
    
    public IEvaluatable RightSide { get; set; }

    public Equivalence(IEvaluatable leftSide, IEvaluatable rightSide)
    {
        LeftSide = leftSide;
        RightSide = rightSide;
    }
    
    public bool Evaluate(Dictionary<string, bool> variables)
    {
        if (LeftSide.Evaluate(variables) == RightSide.Evaluate(variables))
        {
            return true;
        }
        
        return false;
    }
    
    public override string ToString()
    {
        return $"({LeftSide}~{RightSide})";
    }
}