using HashTable;

namespace HashTableTests;

[TestFixture]
    
public class ExceptionTests
{
    private HashTable<GeoPosition, GeoObject> _table;
    private GeoPosition _existingKey;
    private GeoObject _existingValue;

    [SetUp]
    public void Setup()
    {
        _table = new HashTable<GeoPosition, GeoObject>(5);
        _existingKey = new GeoPosition(40, 'N', 74, 'W');
        _existingValue = new GeoObject("Statue of Liberty", GeoTypes.Monument);
        _table.Add(_existingKey, _existingValue);
    }

    [Test]
    public void Update_NonExistentKey_ShouldThrowKeyNotFoundException()
    {
        var nonExistentKey = new GeoPosition(51, 'N', 0, 'W');
            
        var ex = Assert.Throws<KeyNotFoundException>(() => 
            _table.Update(nonExistentKey, new GeoObject("Test", GeoTypes.City)));
            
        Assert.That(ex.Message, Is.EqualTo("Could not find item in hash table"));
    }

    [Test]
    public void Update_DeletedKey_ShouldThrowKeyNotFoundException()
    {
        _table.Remove(_existingKey);
            
        var ex = Assert.Throws<KeyNotFoundException>(() => 
            _table.Update(_existingKey, new GeoObject("Updated", GeoTypes.Monument)));
            
        Assert.That(ex.Message, Is.EqualTo("Could not find item in hash table"));
    }

    [Test]
    public void Remove_NonExistentKey_ShouldThrowKeyNotFoundException()
    {
        var nonExistentKey = new GeoPosition(34, 'S', 58, 'E');
            
        var ex = Assert.Throws<KeyNotFoundException>(() => 
            _table.Remove(nonExistentKey));
            
        Assert.That(ex.Message, Is.EqualTo("Could not find item in hash table"));
    }

    [Test]
    public void Remove_AlreadyDeletedKey_ShouldThrowKeyNotFoundException()
    {
        _table.Remove(_existingKey);
            
        var ex = Assert.Throws<KeyNotFoundException>(() => 
            _table.Remove(_existingKey));
            
        Assert.That(ex.Message, Is.EqualTo("Could not find item in hash table"));
    }

    [Test]
    public void Update_FullTableWithoutKey_ShouldThrow()
    { 
        for (int i = 0; i < _table.Capacity - _table.Count; i++)
        {
            var pos = new GeoPosition(i, 'N', i, 'E');
            _table.Add(pos, new GeoObject($"Obj{i}", GeoTypes.City));
        }
            
        var nonExistentKey = new GeoPosition(90, 'N', 180, 'E');
            
        var ex = Assert.Throws<KeyNotFoundException>(() => 
            _table.Update(nonExistentKey, new GeoObject("Fail", GeoTypes.City)));
            
        Assert.That(ex.Message, Is.EqualTo("Could not find item in hash table"));
    }
}