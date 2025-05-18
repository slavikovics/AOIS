using System.Diagnostics.CodeAnalysis;

namespace HashTable;

public class HashTable<K, T>
{
    public int Count { get; private set; }

    public int Capacity { get; private set; }

    private readonly HashTableItem<K, T>[] _items;

    public HashTable(int capacity)
    {
        if (capacity <= 0) throw new ArgumentOutOfRangeException(nameof(capacity),  "Capacity must be greater than zero");
        
        _items = new HashTableItem<K, T>[capacity];
        for (int i = 0; i < capacity; i++)
        {
            _items[i] = new HashTableItem<K, T>();
        }
        
        Capacity = capacity;
        Count = 0;
    }

    public T? Find(K key)
    {
        int baseIndex = GetBaseIndex(key);
        
        for (int i = 0; i < Capacity; i++)
        {
            var tableIndex = (baseIndex + i) % Capacity;
            var item = _items[tableIndex];
            
            if (item.Occupied && !item.Deleted && item.Key.Equals(key))
            {
                return item.Value;
            }
            if (!item.Occupied)
            {
                break;
            }
        }

        throw new KeyNotFoundException("Could not find item in hash table");
    }

    public void Add(K key, T value)
    {
        int baseIndex = GetBaseIndex(key);
        
        for (int i = 0; i < Capacity; i++)
        {
            var tableIndex = (baseIndex + i) % Capacity;
            var item = _items[tableIndex];
            
            if (item.Occupied && !item.Deleted && item.Key.Equals(key))
            {
                throw new Exception("Item with such key already exists");
            }
            if (!item.Occupied || item.Deleted)
            {
                item.Key = key;
                item.Value = value;
                item.Occupied = true;
                item.Deleted = false;
                Count++;
                return;
            }
        }

        throw new Exception("Table is full.");
    }

    public void Update(K key, T newValue)
    {
        int baseIndex = GetBaseIndex(key);
        
        for (int i = 0; i < Capacity; i++)
        {
            var tableIndex = (baseIndex + i) % Capacity;
            var item = _items[tableIndex];
            
            if (item.Occupied && !item.Deleted && item.Key.Equals(key))
            {
                item.Value = newValue;

                return;
            }
            if (!item.Occupied)
            {
                break;
            }
        }

        throw new KeyNotFoundException("Could not find item in hash table");
    }

    public void Remove(K key)
    {
        int baseIndex = GetBaseIndex(key);
        
        for (int i = 0; i < Capacity; i++)
        {
            var tableIndex = (baseIndex + i) % Capacity;
            var item = _items[tableIndex];
            
            if (item.Occupied && !item.Deleted && item.Key.Equals(key))
            {
                item.Deleted = true;
                Count--;
                return;
            }
            if (!item.Occupied)
            {
                break;
            }
        }

        throw new KeyNotFoundException("Could not find item in hash table");
    }

    public double OccupationRate()
    {
        return Count / (double)Capacity;
    }

    private int GetBaseIndex(K key)
    {
        if (key == null) throw new ArgumentNullException(nameof(key));
        return (key.GetHashCode() & 0x7FFFFFFF) % Capacity;
    }
    
    public override string ToString()
    {
        string result = "";
        int i = 1;

        foreach (var item in _items)
        {
            result += i + ". " + item + "\n";
            i++;
        }
        
        result += $"Count: {Count}\n";
        result += $"Capacity: {Capacity}\n";
        result += $"OccupationRate: {OccupationRate()}\n";

        return result.Trim('\n');
    }
}