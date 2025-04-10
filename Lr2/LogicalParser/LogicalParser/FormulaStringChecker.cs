using System.ComponentModel.Design;

namespace LogicalParser;

public static class FormulaStringChecker
{
    public static bool Check(string formula)
    {
        IEvaluatable parsedFormula;
        
        try
        {
            parsedFormula = FormulaParser.Parse(formula);
        }
        catch (Exception e)
        {
            return false;
        }

        if (formula.Contains("(0)") || formula.Contains("(1)")) return false;
        if (parsedFormula.ToString() == formula) return true;
        return false;
    }

    public static bool Check(IEvaluatable parsedFormula, string formula)
    {
        if (parsedFormula.ToString() == formula) return true;
        return false;
    }
}