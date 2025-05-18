using System.Reflection.Metadata;

namespace HashTable;

public class GeoObject(string name, GeoTypes type)
{
    public GeoTypes Type { get; } = type;

    public string Name { get; } = name;

    public override string ToString()
    {
        return $"{Type} [{Name}]";
    }
}