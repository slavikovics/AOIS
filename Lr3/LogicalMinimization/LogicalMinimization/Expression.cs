namespace LogicalMinimization;

public class Expression
{
    private List<Variable> Variables { get; }

    private int Length => Variables.Count;

    public Expression(List<Variable> variables)
    {
        Variables = variables;
    }

    public Expression StickTogether(Expression otherExpression)
    {
        int? inequalityIndex = null;
        if (otherExpression.Length != Length) throw new ArgumentException("Cannot compare expressions with different variables sets.");

        for (int i = 0; i < Length; i++)
        {
            if (Variables[i] == otherExpression.Variables[i]) continue;
            if (Variables[i].IsOpposite(otherExpression.Variables[i]) && inequalityIndex is null) inequalityIndex = i;
            else throw new ArgumentException("Cannot stick those expressions together.");
        }

        return WithoutVariable(inequalityIndex);
    }

    private Expression WithoutVariable(int? variableIndex)
    {
        if (variableIndex is null) throw new ArgumentException("variableIndex was null");

        List<Variable> newVariables = [];
        foreach (var variable in Variables) newVariables.Add(variable.Clone());
        newVariables.RemoveAt((int)variableIndex);

        return new Expression(newVariables);
    }

    public override string ToString()
    {
        string result = "";
        foreach (var variable in Variables) result += variable + "";

        return result.Trim();
    }

    public string ToString(FormType formType)
    {
        string result = "";
        string sign = "&";
        if (formType == FormType.Conjunctive) sign = "|";

        for (int i = 0; i < Variables.Count - 1; i++) result += Variables[i] + sign;
        result += Variables[^1];
        
        if (result.Length > 1) return $"({result})";
        return result;
    }
}