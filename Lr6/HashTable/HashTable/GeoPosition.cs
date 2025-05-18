namespace HashTable;

public class GeoPosition
{
    public int Latitude { get; }
    public char LatitudeChar { get; }
    public int Longitude { get; }
    public char LongitudeChar { get; }

    public GeoPosition(int latitude, char latitudeChar, int longitude, char longitudeChar)
    {
        ValidateCoordinates(latitude, latitudeChar, longitude, longitudeChar);

        Latitude = latitude;
        LatitudeChar = latitudeChar;
        Longitude = longitude;
        LongitudeChar = longitudeChar;
    }

    private void ValidateCoordinates(int lat, char latChar, int lon, char lonChar)
    {
        if (lat < 0 || lat > 90)
            throw new ArgumentOutOfRangeException(nameof(lat), "Latitude must be 0-90");
        
        if (latChar != 'N' && latChar != 'S')
            throw new ArgumentException("Latitude character must be N or S", nameof(latChar));

        if (lon < 0 || lon > 180)
            throw new ArgumentOutOfRangeException(nameof(lon), "Longitude must be 0-180");
        
        if (lonChar != 'E' && lonChar != 'W')
            throw new ArgumentException("Longitude character must be E or W", nameof(lonChar));
    }

    public override string ToString()
    {
        return $"{LatitudeChar}{Latitude}, {LongitudeChar}{Longitude}";
    }

    public override bool Equals(object? obj)
    {
        return obj is GeoPosition other &&
               Latitude == other.Latitude &&
               LatitudeChar == other.LatitudeChar &&
               Longitude == other.Longitude &&
               LongitudeChar == other.LongitudeChar;
    }

    public override int GetHashCode()
    {
        unchecked
        {
            int hash = 17;
            hash = hash * 23 + Latitude;
            hash = hash * 23 + LatitudeChar.GetHashCode();
            hash = hash * 23 + Longitude;
            hash = hash * 23 + LongitudeChar.GetHashCode();
            
            return hash & 0x7FFFFFFF;
        }
    }
}