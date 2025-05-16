using System.Reflection.Metadata;

namespace HashTable;

public class HashTableItem<K, T>
{
    public K Key { get; private set; }
    public T Value { get; set; }
    
    public bool Collision { get; set; }
    
    public bool Occupied { get; set; }

    public bool Deleted { get; set; }
    
    public HashTableItem(K key, T value)
    {
        Key = key;
        Value = value;
        Collision = false;
        Occupied = false;
        Deleted = false;
    }
}