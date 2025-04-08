namespace LogicalMinimization;

public class VariableEqualityComparer: IEqualityComparer<Variable>
{
    public bool Equals(Variable? x, Variable? y)
    {
        if (ReferenceEquals(x, y)) return true;
        if (x is null) return false;
        if (y is null) return false;
        if (x.GetType() != y.GetType()) return false;
        return x.Name == y.Name && x.IsPositive == y.IsPositive;
    }

    public int GetHashCode(Variable obj)
    {
        return HashCode.Combine(obj.Name, obj.IsPositive);
    }
}