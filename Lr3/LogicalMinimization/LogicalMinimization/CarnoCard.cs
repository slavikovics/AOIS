using System.Reflection.Metadata.Ecma335;
using LogicalParser;

namespace LogicalMinimization;

public class CarnoCard
{
    public List<string> ColumnVariables { get; private set; }

    public List<string> ColumnArguments { get; private set; }

    public List<string> RowVariables { get; private set; }

    public List<string> RowArguments { get; private set; }

    public bool[,] Table { get; private set; }

    public IEvaluatable Formula { get; private set; }

    public CarnoCard(IEvaluatable formula)
    {
        var variables = FormulaParser.FindAllPropositionalVariables(formula.ToString());
        Formula = formula;
        SplitVariables(variables);
        InitializeTable();
    }

    private void SplitVariables(List<string> variables)
    {
        int column = variables.Count / 2;
        int row = variables.Count - column;

        ColumnVariables = variables.GetRange(0, column);
        RowVariables = variables.GetRange(column + 1, row);
    }

    public static void FindSelection(bool[,] table, int height, int width, int x, int y)
    {
        var initial = new CarnoSelection(x, y, height, width);
        List<CarnoSelection> currentGen = [initial];
        List<CarnoSelection> nextGen = [];

        do
        {
            foreach (var selection in currentGen)
            {
                var nearRight = selection.Right();
                if (nearRight.IsValid(table)) nextGen.Add(nearRight);
            
                var nearLeft = selection.Left();
                if (nearLeft.IsValid(table)) nextGen.Add(nearLeft);
            
                var nearUp = selection.Up();
                if (nearUp.IsValid(table)) nextGen.Add(nearUp);
            
                var nearDown = selection.Down();
                if (nearDown.IsValid(table)) nextGen.Add(nearDown);
            }

            if (nextGen.Count == 0)
            {
                // TODO return biggest selection
                return;
            }

            currentGen.Clear();
            currentGen.AddRange(nextGen);
            nextGen.Clear();
            
        } while (true);
    }

    private void InitializeTable()
    {
        ColumnArguments = GenerateGrayCode(ColumnVariables.Count);
        RowArguments = GenerateGrayCode(RowVariables.Count);
        Table = new bool[RowArguments.Count, ColumnArguments.Count];

        for (int i = 0; i < ColumnArguments.Count; i++)
        {
            for (int j = 0; j < RowArguments.Count; j++)
            {
                var match = MatchVariablesWithArguments(ColumnArguments[i], RowArguments[j]);
                Table[j, i] = Formula.Evaluate(match);
            }
        }
    }

    private Dictionary<string, bool> MatchVariablesWithArguments(string columnArguments, string rowArguments)
    {
        Dictionary<string, bool> match = [];
        
        for (int i = 0; i < ColumnVariables.Count; i++)
        {
            if (columnArguments[i] == '1') match.Add(ColumnVariables[i], true);
            else match.Add(ColumnVariables[i], false);
        }
        
        for (int i = 0; i < RowVariables.Count; i++)
        {
            if (columnArguments[i] == '1') match.Add(RowVariables[i], true);
            else match.Add(RowVariables[i], false);
        }

        return match;
    }
    
    private static List<string> GenerateGrayCode(int n)
    {
        List<string> result = new List<string>();
        
        result.Add("0");
        result.Add("1");

        for (int i = 2; i <= n; i++)
        {
            List<string> reversed = new List<string>(result);
            reversed.Reverse();
            
            for (int j = 0; j < result.Count; j++) result[j] = "0" + result[j];
            for (int j = 0; j < reversed.Count; j++) reversed[j] = "1" + reversed[j];
            
            result.AddRange(reversed);
        }

        return result;
    }
}