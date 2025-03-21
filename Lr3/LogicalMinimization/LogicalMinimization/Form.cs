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

    public void Stick()
    {
        List<Expression> result = [];
        bool[] wasStuck = new bool[Expressions.Count];
        
        for (int i = 0; i < wasStuck.Length; i++)
        {
            wasStuck[i] = false;
        }

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

        for (int i = 0; i < wasStuck.Length; i++)
        {
            if (!wasStuck[i]) result.Add(Expressions[i]);
        }
        
        Expressions = result;
    }
}