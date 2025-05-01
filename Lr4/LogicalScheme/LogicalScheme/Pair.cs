namespace LogicalScheme;

public class Pair<T1, T2>
{
    public T1 Key;

    public T2 Value;
    
    public Pair(T1 key, T2 value)
    {
        Key = key;
        Value = value;
    }
}