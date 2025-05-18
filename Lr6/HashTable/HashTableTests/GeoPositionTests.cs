using HashTable;

namespace HashTableTests;

[TestFixture]
public class GeoPositionTests
{
    [Test]
    public void Constructor_WithValidParameters_ShouldCreateObject()
    {
        var position = new GeoPosition(50, 'N', 30, 'E');
            
        Assert.Multiple(() =>
        {
            Assert.That(position.Latitude, Is.EqualTo(50));
            Assert.That(position.LatitudeChar, Is.EqualTo('N'));
            Assert.That(position.Longitude, Is.EqualTo(30));
            Assert.That(position.LongitudeChar, Is.EqualTo('E'));
        });
    }

    [Test]
    public void Constructor_WithInvalidLatitude_ShouldThrowException()
    {
        Assert.Multiple(() =>
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => new GeoPosition(-1, 'N', 0, 'E'));
            Assert.Throws<ArgumentOutOfRangeException>(() => new GeoPosition(91, 'S', 0, 'W'));
        });
    }

    [Test]
    public void Constructor_WithInvalidLongitude_ShouldThrowException()
    {
        Assert.Multiple(() =>
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => new GeoPosition(0, 'N', -1, 'E'));
            Assert.Throws<ArgumentOutOfRangeException>(() => new GeoPosition(0, 'S', 181, 'W'));
        });
    }

    [Test]
    public void Constructor_WithInvalidDirectionChars_ShouldThrowException()
    {
        Assert.Multiple(() =>
        {
            Assert.Throws<ArgumentException>(() => new GeoPosition(0, 'X', 0, 'E'));
            Assert.Throws<ArgumentException>(() => new GeoPosition(0, 'N', 0, 'Y'));
        });
    }
    
    [Test]
    public void Equals_WithSameValues_ShouldReturnTrue()
    {
        var pos1 = new GeoPosition(40, 'N', 74, 'W');
        var pos2 = new GeoPosition(40, 'N', 74, 'W');
            
        Assert.That(pos1.Equals(pos2), Is.True);
    }

    [Test]
    public void Equals_WithDifferentValues_ShouldReturnFalse()
    {
        var pos1 = new GeoPosition(40, 'N', 74, 'W');
        var pos2 = new GeoPosition(41, 'S', 75, 'E');
            
        Assert.That(pos1.Equals(pos2), Is.False);
    }

    [Test]
    public void GetHashCode_ForEqualObjects_ShouldBeEqual()
    {
        var pos1 = new GeoPosition(51, 'N', 0, 'W');
        var pos2 = new GeoPosition(51, 'N', 0, 'W');
            
        Assert.That(pos1.GetHashCode(), Is.EqualTo(pos2.GetHashCode()));
    }
    
    [Test]
    public void ToString_ShouldReturnFormattedString()
    {
        var position = new GeoPosition(51, 'N', 0, 'W');
        Assert.That(position.ToString(), Is.EqualTo("N51, W0"));
    }
}