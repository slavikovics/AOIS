using System.Reflection.Metadata;

namespace HashTable;

public class HashTableItem<K, T>
{
    public K? Key { get; set; }
    
    public T? Value { get; set; }
    
    public bool Occupied { get; set; }

    public bool Deleted { get; set; }
    
    public HashTableItem(K key, T value)
    {
        Key = key;
        Value = value;
        Occupied = false;
        Deleted = false;
    }

    public HashTableItem()
    {
        Key = default;
        Value = default;
        Occupied = false;
        Deleted = false;
    }

    public override string ToString()
    {
        return $"Occupied: {Occupied}, Deleted: {Deleted}";
    }
}