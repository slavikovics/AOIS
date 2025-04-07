using LogicalParser;

namespace LogicalMinimization;

public class MapSplitter
{
    public void SplitFormula(IEvaluatable formula)
    {
        var variables = FormulaParser.FindAllPropositionalVariables(formula.ToString());
        if (variables.Count <= 4) return;

        var baseVariables = variables.GetRange(0, 4);
        variables.RemoveRange(0, 4);

        var forms = new List<Form>();
        Calculate(variables, formula, forms);
    }
    
    public Form CalculateKarnaugh(IEvaluatable formula)
    {
        KarnaughMap karnaughMap = new KarnaughMap(formula);
        return karnaughMap.MinimizeToDisjunctional();
    }

    public void ReplaceVariable(IEvaluatable formula, string variableName)
    {
    }

    public void Calculate(List<string> variables, IEvaluatable formula, List<Form> forms)
    {
        string current = variables[0];
        variables.RemoveAt(0);
        
        if (variables.Count == 0)
        {
            var left = formula.Replace(current, "1");
            var right = formula.Replace(current, "0");
            forms.Add(CalculateKarnaugh(left));
            forms.Add(CalculateKarnaugh(right));
            return;
        }

        Calculate(variables, formula.Replace(current, "1"), forms);
        Calculate(variables, formula.Replace(current, "0"), forms);
    }
}