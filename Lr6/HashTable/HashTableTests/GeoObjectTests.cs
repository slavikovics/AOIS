using HashTable;

namespace HashTableTests;

[TestFixture]
public class GeoObjectTests
{
    [Test]
    public void Constructor_ShouldInitializePropertiesCorrectly()
    {
        var geoObject = new GeoObject("Eiffel Tower", GeoTypes.Monument);
            
        Assert.Multiple(() =>
        {
            Assert.That(geoObject.Name, Is.EqualTo("Eiffel Tower"));
            Assert.That(geoObject.Type, Is.EqualTo(GeoTypes.Monument));
        });
    }

    [Test]
    public void Properties_ShouldBeReadOnly()
    {
        var geoObject = new GeoObject("Test", GeoTypes.City);
            
        Assert.Multiple(() =>
        {
            Assert.That(geoObject.GetType().GetProperty("Name")?.CanWrite, Is.False);
            Assert.That(geoObject.GetType().GetProperty("Type")?.CanWrite, Is.False);
        });
    }
}