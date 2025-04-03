namespace LogicalMinimization;

public class CalculationTable
{
    public Form LogicalForm { get; }

    public bool[,] Crosses { get; }
    
    public int Width { get; }
    
    public int Height { get; }

    public List<Expression> StartExpressions { get; }
    
    public List<Expression> UnnecessaryExpressions { get; }

    public CalculationTable(Form logicalForm, List<Expression> startExpressions)
    {
        LogicalForm = logicalForm;
        StartExpressions = startExpressions;
        Width = startExpressions.Count;
        Height = logicalForm.Expressions.Count;
        Crosses = new bool[Height, Width];
        CheckCrosses();
        UnnecessaryExpressions = RemoveUnnecessaryExpressions();
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

    private bool IsRowSubset(int first, int second)
    {
        for (int i = 0; i < Width; i++)
        {
            if (Crosses[first, i] == Crosses[second, i] || Crosses[second, i]) continue;
            return false;
        }

        return true;
    }

    private bool IsRowOdd(int rowNumber)
    {
        for (int i = 0; i < rowNumber; i++)
            if (IsRowSubset(rowNumber, i)) return true;
        
        for (int i = rowNumber + 1; i < Height; i++)
            if (IsRowSubset(rowNumber, i)) return true;
        
        return false;
    }

    public List<Expression> RemoveUnnecessaryExpressions()
    {
        List<Expression> unnecessaryExpressions = [];
        
        for (int i = 0; i < Height; i++)
            if (IsRowOdd(i)) unnecessaryExpressions.Add(LogicalForm.Expressions[i]);
        
        foreach (var exp in unnecessaryExpressions)
            LogicalForm.Expressions.Remove(exp);
        
        return unnecessaryExpressions;
    }
}