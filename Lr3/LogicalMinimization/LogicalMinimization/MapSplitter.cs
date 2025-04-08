using LogicalParser;

namespace LogicalMinimization;

public class MapSplitter
{
    public Form SplitFormula(IEvaluatable formula, FormType type)
    {
        var variables = FormulaParser.FindAllPropositionalVariables(formula.ToString());
        if (variables.Count <= 4) return CalculateKarnaugh(formula.ToString(), type);

        var baseVariables = variables.GetRange(0, 4);
        variables.RemoveRange(0, 4);

        var forms = new List<Form>();
        Calculate(variables, formula.ToString(), forms, type);

        for (int i = 1; i < forms.Count; i++)
        {
            foreach (var exp in forms[i].Expressions)
            {
                forms[0].Expressions.Add(exp);
            }
        }

        DeleteRepeats(forms[0].Expressions);
        return forms[0];
    }
    
    public Form CalculateKarnaugh(string formula, FormType type)
    {
        KarnaughMap karnaughMap = new KarnaughMap(formula);
        if (type == FormType.Disjunctive) return karnaughMap.MinimizeToDisjunctional();
        return karnaughMap.MinimizeToConjunctional();
    }

    public bool DependsOnOtherVariables(Expression check, List<Expression> expressions)
    {
        foreach (var exp in expressions)
        {
            bool expIsSubsetForCheck = true;
            
            foreach (var form2Variable in exp.Variables)
            {
                bool hasVariable = false;
                foreach (var form1Variable in check.Variables)
                {
                    if (form1Variable == form2Variable)
                    {
                        hasVariable = true;
                        break;
                    }
                }

                if (!hasVariable)
                {
                    expIsSubsetForCheck = false;
                    break;
                }
            }

            if (expIsSubsetForCheck) return false;
        }

        return true;
    }

    private void DeleteRepeats(List<Expression> expressions)
    {
        for (int i = 0; i < expressions.Count; i++)
        {
            for (int j = i + 1; j < expressions.Count; j++)
            {
                bool hasAllVariables = true; 
                    
                foreach (var variable in expressions[i].Variables)
                {
                    if (!expressions[j].Variables.Contains(variable, new VariableEqualityComparer()))
                    {
                        hasAllVariables = false;
                        break;
                    }
                }

                if (hasAllVariables)
                {
                    expressions.RemoveAt(j);
                    j--;
                }
            }
        }
    }

    public void Calculate(List<string> variables, string formula, List<Form> forms, FormType type)
    {
        string current = variables[0];
        variables.RemoveAt(0);
        bool truth = type == FormType.Disjunctive;
        
        if (variables.Count == 0)
        {
            var left = formula.Replace(current, "1");
            var right = formula.Replace(current, "0");
            
            var leftResult = CalculateKarnaugh(left, type);
            var rightResult = CalculateKarnaugh(right, type);
            bool shouldAddLeft = true;
            bool shouldAddRight = true;

            if (rightResult.Expressions.Count == 1 && rightResult.Expressions[0].Variables.Count == 0)
                shouldAddLeft = false;
            if (leftResult.Expressions.Count == 1 && leftResult.Expressions[0].Variables.Count == 0)
                shouldAddRight = false;
            
            foreach (var exp in leftResult.Expressions)
                if (!DependsOnOtherVariables(exp, rightResult.Expressions)) exp.IsFull = true;
            
            foreach (var exp in rightResult.Expressions)
                if (!DependsOnOtherVariables(exp, leftResult.Expressions)) exp.IsFull = true;

            if (shouldAddLeft)
                leftResult.Expressions.ForEach(x => { if (!x.IsFull) x.Variables.Add(new Variable(current, truth));});
            forms.Add(leftResult);

            if (shouldAddRight)
                rightResult.Expressions.ForEach(x => { if (!x.IsFull) x.Variables.Add(new Variable(current, !truth));});
            forms.Add(rightResult);
            
            return;
        }

        Calculate(variables, formula.Replace(current, "1"), forms, type);
        Calculate(variables, formula.Replace(current, "0"), forms, type);
    }
}