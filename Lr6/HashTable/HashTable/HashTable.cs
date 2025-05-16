namespace HashTable;

public class HashTable<K, T>
{
    public int Count { get; private set; }

    public int Capacity { get; private set; }

    private HashTableItem<K, T>[] _items;

    public delegate int CustomHashFunction(K key);

    private CustomHashFunction? _hashFunction = null;

    public HashTable(int capacity)
    {
        _items = new HashTableItem<K, T>[capacity];
        Count = 0;
    }

    public HashTable(int capacity, CustomHashFunction customHashFunction)
    {
        _hashFunction = customHashFunction;
        _items = new HashTableItem<K, T>[capacity];
        Count = 0;
    }

    public T? Find(K key)
    {
        int tableIndex = GetBaseIndex(key);
        return _items[GetBaseIndex(key)].Value;
    }

    public void Add(K key, T value)
    {
        ForceAdd(key, value);
    }

    public void ForceAdd(K key, T value)
    {
        int tableIndex = GetBaseIndex(key);
        //var item = new HashTableItem<K, T>(key, value);
        if (!_items[tableIndex].Occupied)
        {
            _items[tableIndex].Value = value;
            _items[tableIndex].Occupied = true;
        }

        Count++;
    }

    public void Remove(K key)
    {
        int tableIndex = GetBaseIndex(key);

    }

    public double OccupationRate()
    {
        return Count / (double)Capacity;
    }

    private HashTableItem<K, T> HandleCollision(K key)
    {
        int tableIndex = GetBaseIndex(key);
        var item = _items[tableIndex];
        
        for (int i = 0; i < Capacity; i++)
        {
            tableIndex = (tableIndex + i) % Capacity; 
            if (!item.Occupied || item.Deleted || (item.Key != null && item.Key.Equals(key)))
            {
                return _items[tableIndex];
            }
        }

        throw new Exception("Couldn't find index");
    }

    private int GetBaseIndex(K key)
    {
        if (key == null) throw new ArgumentNullException(nameof(key));
        if (_hashFunction == null) return key.GetHashCode() % Capacity;
        return _hashFunction(key) % Capacity;
    }
}