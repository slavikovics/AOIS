using LogicalParser;

namespace LogicalMinimization;

public class MapSplitter
{
    public static void SplitFormula(IEvaluatable formula)
    {
        var variables = FormulaParser.FindAllPropositionalVariables(formula.ToString());
        if (variables.Count <= 4) return;

        var baseVariables = variables.GetRange(0, 4);
        variables.RemoveRange(0, 4);
        
        
    }
}