using System.ComponentModel.Design;

namespace LogicalParser;

public static class FormulaStringChecker
{
    public static bool Check(string formula)
    {
        IEvaluatable parsedFormula = FormulaParser.Parse(formula);
        
        if (parsedFormula.ToString() == formula) return true;
        return false;
    }

    public static bool Check(IEvaluatable parsedFormula, string formula)
    {
        if (parsedFormula.ToString() == formula) return true;
        return false;
    }
}