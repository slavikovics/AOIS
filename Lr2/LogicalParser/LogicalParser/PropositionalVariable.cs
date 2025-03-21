namespace LogicalParser;

public class PropositionalVariable: IEvaluatable
{
    public bool Value { get; }
    
    public string Name { get; }

    public PropositionalVariable(string name)
    {
        Name = name;
    }
    
    public bool Evaluate(Dictionary<string, bool> variables)
    {
        return variables[Name];
    }

    public override string ToString()
    {
        return Name + " ";
    }
}