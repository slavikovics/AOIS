using System.Reflection.Metadata;

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
    
    public bool[,] FlipCrosses()
    {
        bool[,] flipped = new bool[Width, Height];
        
        for (int i = 0; i < Height; i++)
        {
            for (int j = 0; j < Width; j++)
            {
                flipped[j, i] = Crosses[i, j];
            }
        }

        return flipped;
    }

    private bool IsEnough(List<int> rowNumbers)
    {
        bool[] isEnough = new bool[Width];
        for (int i = 0; i < Width; i++) isEnough[i] = false;
        
        foreach (var rowNumber in rowNumbers)
        {
            for (int i = 0; i < Width; i++)
                if (Crosses[rowNumber, i])
                    isEnough[i] = true;
        }
        
        for (int i = 0; i < Width; i++) if (isEnough[i] == false) return false;
        return true;
    }

    public List<List<int>> GenerateCombinations()
    {
        var result = new List<List<int>>();

        for (int length = 1; length <= Height; length++)
        {
            GenerateCombinationsRecursive(result, new List<int>(), 0, length);
        }

        return result;
    }

    private void GenerateCombinationsRecursive(List<List<int>> result, List<int> current, int start, int length)
    {
        if (current.Count == length)
        {
            result.Add(new List<int>(current));
            return;
        }

        for (int i = start; i < Height; i++)
        {
            current.Add(i);
            GenerateCombinationsRecursive(result, current, i + 1, length);
            current.RemoveAt(current.Count - 1);
        }
    }

    private List<Expression> RemoveUnnecessaryExpressions()
    {
        List<Expression> unnecessaryExpressions = [];
        List<int> selectedCombo = [];

        var combos = GenerateCombinations();
        foreach (var combo in combos)
        {
            if (IsEnough(combo))
            {
                selectedCombo = combo;
                break;
            }
        }

        for (int i = 0; i < Height; i++)
        {
            if (!selectedCombo.Contains(i)) unnecessaryExpressions.Add(LogicalForm.Expressions[i]);
        }
        
        foreach (var exp in unnecessaryExpressions)
            LogicalForm.Expressions.Remove(exp);
        
        return unnecessaryExpressions;
    }
}