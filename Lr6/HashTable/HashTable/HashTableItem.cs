using System.Diagnostics.CodeAnalysis;
using System.Reflection.Metadata;

namespace HashTable;

public class HashTableItem<K, T>
{
    public K? Key { get; set; }
    
    public T? Value { get; set; }
    
    public bool Occupied { get; set; }

    public bool Deleted { get; set; }

    public HashTableItem()
    {
        Key = default;
        Value = default;
        Occupied = false;
        Deleted = false;
    }
    
    public override string ToString()
    {
        if (!Occupied) return "[Empty]";
        if (Deleted) return "[Deleted]";
        return $"{Key} -> {Value}";
    }
}