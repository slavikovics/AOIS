namespace LogicalMinimization;

public class Variable
{
    private string Name { get; }
    
    private bool IsPositive { get; }

    public Variable(string name, bool isPositive)
    {
        Name = name;
        IsPositive = isPositive;
    }
    
    public static bool operator==(Variable left, Variable right)
    {
        return left.Name == right.Name && left.IsPositive == right.IsPositive;
    }
    
    public static bool operator!=(Variable left, Variable right)
    {
        return !(left.Name == right.Name && left.IsPositive == right.IsPositive);
    }

    public bool IsOpposite(Variable otherVariable)
    {
        return Name == otherVariable.Name && IsPositive != otherVariable.IsPositive;
    }

    public Variable Clone()
    {
        return new Variable(Name, IsPositive);
    }
}