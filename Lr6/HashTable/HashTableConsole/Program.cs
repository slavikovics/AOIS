namespace HashTable;

class Program
{
    private static readonly HashTable<GeoPosition, GeoObject> HashTable = new(20);
    
    private static void Main(string[] args)
    {
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine("GEO OBJECTS HASHTABLE\n");
        Console.ResetColor();

        while (true)
        {
            PrintOptions();
            UserInput();
            PrintHashTableState();
        }
    }

    private static void PrintOptions()
    {
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine("╔══════════════════════╗");
        Console.WriteLine("║   Select operation   ║");
        Console.WriteLine("╠══════════════════════╣");
        Console.WriteLine("║ 1. Add geo object    ║");
        Console.WriteLine("║ 2. Find geo object   ║");
        Console.WriteLine("║ 3. Update geo object ║");
        Console.WriteLine("║ 4. Delete geo object ║");
        Console.WriteLine("║ 5. Exit              ║");
        Console.WriteLine("╚══════════════════════╝");
        Console.ResetColor();
        Console.Write("\nYour choice: ");
    }

    private static void UserInput()
    {
        var response = Console.ReadLine();
        Console.WriteLine();

        switch (response)
        {
            case "1": AddGeoObject(); break;
            case "2": FindGeoObject(); break;
            case "3": UpdateGeoObject(); break;
            case "4": DeleteGeoObject(); break;
            case "5": Environment.Exit(0); break;
            default: 
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Invalid option!");
                Console.ResetColor();
                break;
        }
    }

    private static GeoPosition ReadGeoPosition()
    {
        Console.WriteLine("Enter coordinates:");
        Console.Write("Latitude (0-90): ");
        int lat = int.Parse(Console.ReadLine() ?? "0");
        
        Console.Write("Latitude direction (N/S): ");
        char latDir = char.ToUpper(Console.ReadLine()?.FirstOrDefault() ?? 'N');

        Console.Write("Longitude (0-180): ");
        int lon = int.Parse(Console.ReadLine() ?? "0");
        
        Console.Write("Longitude direction (E/W): ");
        char lonDir = char.ToUpper(Console.ReadLine()?.FirstOrDefault() ?? 'E');

        return new GeoPosition(lat, latDir, lon, lonDir);
    }

    private static GeoObject ReadGeoObject()
    {
        Console.Write("Enter object name: ");
        string name = Console.ReadLine() ?? "Unnamed";

        Console.WriteLine("Select type:");
        var types = Enum.GetValues(typeof(GeoTypes)).Cast<GeoTypes>();
        foreach (var type in types)
        {
            Console.WriteLine($"{(int)type}. {type}");
        }
        GeoTypes selectedType = (GeoTypes)int.Parse(Console.ReadLine() ?? "0");

        return new GeoObject(name, selectedType);
    }

    private static void AddGeoObject()
    {
        try
        {
            var position = ReadGeoPosition();
            var geoObject = ReadGeoObject();
            
            HashTable.Add(position, geoObject);
            
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\nObject successfully added!");
            Console.ResetColor();
        }
        catch (Exception ex)
        {
            ShowError(ex.Message);
        }
    }

    private static void FindGeoObject()
    {
        try
        {
            var position = ReadGeoPosition();
            var result = HashTable.Find(position);
            
            Console.WriteLine("\nFound object:");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine($"Position: {position}");
            Console.WriteLine($"Name: {result.Name}");
            Console.WriteLine($"Type: {result.Type}");
            Console.ResetColor();
        }
        catch (Exception ex)
        {
            ShowError(ex.Message);
        }
    }

    private static void UpdateGeoObject()
    {
        try
        {
            var position = ReadGeoPosition();
            var newObject = ReadGeoObject();
            
            HashTable.Update(position, newObject);
            
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\nObject successfully updated!");
            Console.ResetColor();
        }
        catch (Exception ex)
        {
            ShowError(ex.Message);
        }
    }

    private static void DeleteGeoObject()
    {
        try
        {
            var position = ReadGeoPosition();
            HashTable.Remove(position);
            
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\nObject successfully deleted!");
            Console.ResetColor();
        }
        catch (Exception ex)
        {
            ShowError(ex.Message);
        }
    }

    private static void PrintHashTableState()
    {
        Console.ForegroundColor = ConsoleColor.DarkGray;
        Console.WriteLine("\n════════════ Current Table State ════════════");
        Console.WriteLine(HashTable);
        Console.WriteLine("═══════════════════════════════════════════\n");
        Console.ResetColor();
    }

    private static void ShowError(string message)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine($"\nError: {message}");
        Console.ResetColor();
    }
}