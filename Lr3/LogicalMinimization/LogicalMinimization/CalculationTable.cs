namespace LogicalMinimization;

public class CalculationTable
{
    public Form LogicalForm { get; }

    public bool[,] Crosses { get; }
    
    public int Width { get; }
    
    public int Height { get; }

    public List<Expression> StartExpressions { get; }

    public CalculationTable(Form logicalForm, List<Expression> expressions)
    {
        LogicalForm = logicalForm;
        StartExpressions = expressions;
        Width = expressions.Count;
        Height = logicalForm.Expressions.Count;
        Crosses = new bool[Height, Width];
        CheckCrosses();
    }

    public static bool ShouldPlaceCross(Expression modified, Expression original)
    {
        foreach (var modifiedVariable in modified.Variables)
        {
            bool hasVariable = false;
            
            foreach (var originalVariable in original.Variables)
            {
                if (modifiedVariable == originalVariable)
                {
                    hasVariable = true;
                    break;
                }
            }
            
            if (hasVariable) continue;
            return false;
        }
        return true;
    }

    private void CheckCrosses()
    {
        for (int i = 0; i < Height; i++)
        {
            for (int j = 0; j < Width; j++)
            {
                Crosses[i, j] = ShouldPlaceCross(LogicalForm.Expressions[i], StartExpressions[j]);
            }
        }
    }
}