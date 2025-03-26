namespace LogicalMinimization;

public class Form
{
    public List<Expression> Expressions { get; set; }

    public FormType Type { get; }

    public Form(List<Expression> expressions, FormType type)
    {
        Expressions = expressions;
        Type = type;
    }

    public void StickEverything()
    {
        bool changed;
        
        do changed = Stick();
        while (changed);
    }

    public bool IsNecessary(Expression expression)
    {
        return true;
    }

    public bool Stick()
    {
        List<Expression> result = [];
        bool[] wasStuck = new bool[Expressions.Count];
        
        for (int i = 0; i < wasStuck.Length; i++) wasStuck[i] = false;

        for (int i = 0; i < wasStuck.Length; i++)
        {
            if (wasStuck[i]) continue;
            
            for(int j = 0; j < wasStuck.Length; j++)
            {
                try
                {
                    Expression newExp = Expressions[i].StickTogether(Expressions[j]);
                    if (result.Find(x => x.ToString() == newExp.ToString()) is null) result.Add(newExp);
                    wasStuck[i] = true;
                }
                catch (ArgumentException e)
                {
                }
            }
        }

        FindRemainingExpressions(wasStuck, result);
        Expressions = result;

        foreach (var stuck in wasStuck) if (stuck) return true;
        return false;
    }

    private void FindRemainingExpressions(bool[] wasStuck, List<Expression> result)
    {
        for (int i = 0; i < wasStuck.Length; i++)
        {
            if (!wasStuck[i]) result.Add(Expressions[i]);
        }
    }

    public override string ToString()
    {
        string result = "";
        
        for (int i = 0; i < Expressions.Count - 1; i++)
        {
            result += Expressions[i].ToString(Type);
            if (Type == FormType.Conjunctive) result += "&";
            else result += "|";
        }
        result += Expressions[^1].ToString(Type);

        return result;
    }
}