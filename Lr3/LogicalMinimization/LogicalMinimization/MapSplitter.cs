using LogicalParser;

namespace LogicalMinimization;

public class MapSplitter
{
    public Form SplitFormula(IEvaluatable formula)
    {
        var variables = FormulaParser.FindAllPropositionalVariables(formula.ToString());
        if (variables.Count <= 4) return CalculateKarnaugh(formula.ToString());

        var baseVariables = variables.GetRange(0, 4);
        variables.RemoveRange(0, 4);

        var forms = new List<Form>();
        Calculate(variables, formula.ToString(), forms);

        for (int i = 1; i < forms.Count; i++)
        {
            foreach (var exp in forms[i].Expressions)
            {
                forms[0].Expressions.Add(exp);
            }
        }

        return forms[0];
    }
    
    public Form CalculateKarnaugh(string formula)
    {
        KarnaughMap karnaughMap = new KarnaughMap(formula);
        return karnaughMap.MinimizeToDisjunctional();
    }

    public void Calculate(List<string> variables, string formula, List<Form> forms)
    {
        string current = variables[0];
        variables.RemoveAt(0);
        
        if (variables.Count == 0)
        {
            var left = formula.Replace(current, "1");
            var right = formula.Replace(current, "0");
            var form = CalculateKarnaugh(left);
            form.Expressions.Add(new Expression([new Variable(current, true)]));
            
            forms.Add(form);
            forms.Add(CalculateKarnaugh(right));
            return;
        }

        Calculate(variables, formula.Replace(current, "1"), forms);
        Calculate(variables, formula.Replace(current, "0"), forms);
    }
}