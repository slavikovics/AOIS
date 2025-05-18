using HashTable;

namespace HashTableTests;

[TestFixture]
public class HashTableWithGeoTypesTests
{
    private GeoPosition _nycPosition;
    private GeoObject _nycObject;
    private GeoPosition _londonPosition;
    private GeoObject _londonObject;

    [SetUp]
    public void Setup()
    {
        _nycPosition = new GeoPosition(40, 'N', 74, 'W');
        _nycObject = new GeoObject("New York City", GeoTypes.City);

        _londonPosition = new GeoPosition(51, 'N', 0, 'W');
        _londonObject = new GeoObject("London", GeoTypes.City);
    }

    [Test]
    public void Add_WithGeoPositionKey_ShouldStoreGeoObject()
    {
        var table = new HashTable<GeoPosition, GeoObject>(10);
        table.Add(_nycPosition, _nycObject);

        var result = table.Find(_nycPosition);
        Assert.Multiple(() =>
        {
            Assert.That(result.Name, Is.EqualTo("New York City"));
            Assert.That(result.Type, Is.EqualTo(GeoTypes.City));
        });
    }

    [Test]
    public void Update_WithExistingGeoPositionKey_ShouldChangeValue()
    {
        var table = new HashTable<GeoPosition, GeoObject>(10);
        table.Add(_nycPosition, _nycObject);

        var updatedObject = new GeoObject("NYC Metro", GeoTypes.Metropolis);
        table.Update(_nycPosition, updatedObject);

        Assert.That(table.Find(_nycPosition).Name, Is.EqualTo("NYC Metro"));
    }

    [Test]
    public void Remove_WithGeoPositionKey_ShouldDeleteEntry()
    {
        var table = new HashTable<GeoPosition, GeoObject>(10);
        table.Add(_nycPosition, _nycObject);
        table.Remove(_nycPosition);

        Assert.Multiple(() =>
        {
            Assert.Throws<KeyNotFoundException>(() => table.Find(_nycPosition));
            Assert.That(table.Count, Is.EqualTo(0));
        });
    }

    [Test]
    public void Add_DuplicateGeoPositionKeys_ShouldThrowException()
    {
        var table = new HashTable<GeoPosition, GeoObject>(10);
        table.Add(_nycPosition, _nycObject);

        var duplicateObject = new GeoObject("NYC Duplicate", GeoTypes.City);
        Assert.Throws<Exception>(() => table.Add(_nycPosition, duplicateObject));
    }

    [Test]
    public void Find_WithCollidingGeoPositions_ShouldThrowException()
    {
        var pos1 = new GeoPosition(10, 'N', 20, 'E');
        var pos2 = new GeoPosition(10, 'N', 20, 'E');

        var table = new HashTable<GeoPosition, GeoObject>(2);
        table.Add(pos1, new GeoObject("First", GeoTypes.Monument));

        Assert.Throws<Exception>(() => table.Add(pos2, new GeoObject("Second", GeoTypes.Monument)));
    }

    [Test]
    public void Add_MultipleGeoObjects_ShouldMaintainCorrectCount()
    {
        var table = new HashTable<GeoPosition, GeoObject>(5);
        table.Add(_nycPosition, _nycObject);
        table.Add(_londonPosition, _londonObject);

        Assert.That(table.Count, Is.EqualTo(2));
    }

    [Test]
    public void OccupationRate_AfterMixedOperations_ShouldBeAccurate()
    {
        var table = new HashTable<GeoPosition, GeoObject>(3);
        table.Add(_nycPosition, _nycObject);
        table.Add(_londonPosition, _londonObject);
        table.Remove(_nycPosition);

        Assert.That(table.OccupationRate(), Is.EqualTo(1 / 3.0).Within(0.001));
    }
}