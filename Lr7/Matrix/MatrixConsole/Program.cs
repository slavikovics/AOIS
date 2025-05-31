namespace MatrixConsole;

class Program
{
    static Matrix.Matrix _matrix = new();

    static void Main()
    {
        Console.WriteLine("Matrix initialized with dimension 16.\n");

        while (true)
        {
            PrintMenu();
            Console.Write("Enter choice: ");
            string choice = Console.ReadLine();

            try
            {
                switch (choice)
                {
                    case "1": HandleSetWord();                  
                        break;
                    case "2": HandleGetWord();                  
                        break;
                    case "3": HandleSetAddressColumn();         
                        break;
                    case "4": HandleGetAddressColumn();         
                        break;
                    case "5": HandleConjunction();              
                        break;
                    case "6": HandleShefferNegation();          
                        break;
                    case "7": HandleFirstArgumentRepeat();      
                        break;
                    case "8": HandleFirstArgumentNegation();    
                        break;
                    case "9": HandleCompareWithArgument();      
                        break;
                    case "10": HandleFindCeil();                
                        break;
                    case "11": HandleFindFloor();               
                        break;
                    case "12": HandleSumWordsWithPrefix();      
                        break;
                    case "0": Console.WriteLine("Exiting."); 
                        return;
                    default: Console.WriteLine("Invalid choice."); 
                        break;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }
    }

    static void PrintMenu()
    {
        Console.WriteLine("\nSelect an operation:");
        Console.WriteLine("1  - Set a word");
        Console.WriteLine("2  - Get a word");
        Console.WriteLine("3  - Set address column");
        Console.WriteLine("4  - Get address column");
        Console.WriteLine("5  - Conjunction");
        Console.WriteLine("6  - Sheffer negation");
        Console.WriteLine("7  - First argument repeat");
        Console.WriteLine("8  - First argument negation");
        Console.WriteLine("9  - Compare column with argument");
        Console.WriteLine("10 - Find Ceil");
        Console.WriteLine("11 - Find Floor");
        Console.WriteLine("12 - Sum words with prefix");
        Console.WriteLine("0  - Exit");
    }

    static void HandleSetWord()
    {
        Console.Write("Enter word index: ");
        int idx = int.Parse(Console.ReadLine());
        bool[] values = ReadBoolArray(_matrix.Dimension);
        _matrix.SetWord(idx, values);
        Console.WriteLine("Word set successfully.");
        PrintMatrix();
    }

    static void HandleGetWord()
    {
        Console.Write("Enter word index: ");
        int idx = int.Parse(Console.ReadLine());
        Console.WriteLine("Word: " + BoolArrayToString(_matrix.GetWord(idx)));
    }

    static void HandleSetAddressColumn()
    {
        Console.Write("Enter column index: ");
        int idx = int.Parse(Console.ReadLine());
        bool[] values = ReadBoolArray(_matrix.Dimension);
        _matrix.SetAddressColumn(idx, values);
        Console.WriteLine("Address column set.");
        PrintMatrix();
    }

    static void HandleGetAddressColumn()
    {
        Console.Write("Enter column index: ");
        int idx = int.Parse(Console.ReadLine());
        Console.WriteLine("Address column: " + BoolArrayToString(_matrix.GetAddressColumn(idx)));
    }

    static void HandleConjunction()
    {
        Console.Write("Enter first and second indices (separated by space): ");
        var parts = Console.ReadLine().Split();
        int a = int.Parse(parts[0]), b = int.Parse(parts[1]);
        var result = _matrix.Conjunction(a, b);
        Console.WriteLine("Conjunction: " + BoolArrayToString(result));
    }

    static void HandleShefferNegation()
    {
        Console.Write("Enter first and second indices (separated by space): ");
        var parts = Console.ReadLine().Split();
        int a = int.Parse(parts[0]), b = int.Parse(parts[1]);
        var result = _matrix.ShefferNegation(a, b);
        Console.WriteLine("Sheffer negation: " + BoolArrayToString(result));
    }

    static void HandleFirstArgumentRepeat()
    {
        Console.Write("Enter first and second indices (separated by space): ");
        var parts = Console.ReadLine().Split();
        int a = int.Parse(parts[0]), b = int.Parse(parts[1]);
        var result = _matrix.FirstArgumentRepeat(a, b);
        Console.WriteLine("First argument repeat: " + BoolArrayToString(result));
    }

    static void HandleFirstArgumentNegation()
    {
        Console.Write("Enter first and second indices (separated by space): ");
        var parts = Console.ReadLine().Split();
        int a = int.Parse(parts[0]), b = int.Parse(parts[1]);
        var result = _matrix.FirstArgumentNegation(a, b);
        Console.WriteLine("First argument negation: " + BoolArrayToString(result));
    }

    static void HandleCompareWithArgument()
    {
        Console.Write("Enter column index: ");
        int idx = int.Parse(Console.ReadLine());
        Console.WriteLine("Enter comparison argument:");
        bool[] arg = ReadBoolArray(_matrix.Dimension);
        var (greater, less, equal) = _matrix.CompareWithArgument(idx, arg);
        Console.WriteLine($"Greater: {greater}, Less: {less}, Equal: {equal}");
    }

    static void HandleFindCeil()
    {
        Console.WriteLine("Enter argument for ceil search:");
        bool[] arg = ReadBoolArray(_matrix.Dimension);
        var res = _matrix.FindCeil(arg);
        if (res == null)
            Console.WriteLine("No ceil found.");
        else
            Console.WriteLine($"Ceil at index {res.Value.index}: {BoolArrayToString(res.Value.word)}");
    }

    static void HandleFindFloor()
    {
        Console.WriteLine("Enter argument for floor search:");
        bool[] arg = ReadBoolArray(_matrix.Dimension);
        var res = _matrix.FindFloor(arg);
        if (res == null)
            Console.WriteLine("No floor found.");
        else
            Console.WriteLine($"Floor at index {res.Value.index}: {BoolArrayToString(res.Value.word)}");
    }

    static void HandleSumWordsWithPrefix()
    {
        Console.WriteLine("Enter prefix bits (0 or 1) separated by space:");
        bool[] prefix = Console.ReadLine().Split(' ').Select(s => s == "1").ToArray();
        var results = _matrix.SumWordsWithPrefix(prefix);
        if (!results.Any())
        {
            Console.WriteLine("No matching words found.");
            return;
        }

        foreach (var (index, word) in results)
            Console.WriteLine($"Updated word at index {index}: {BoolArrayToString(word)}");
        
        PrintMatrix();
    }

    static bool[] ReadBoolArray(int length)
    {
        while (true)
        {
            Console.WriteLine($"Enter a {length}-bit string (e.g. 01011) without spaces:");
            string input = Console.ReadLine()?.Trim() ?? string.Empty;

            if (input.Length != length)
            {
                Console.WriteLine($"Error: Expected exactly {length} characters, but got {input.Length}.");
                continue;
            }

            bool[] result = new bool[length];
            bool valid = true;

            for (int i = 0; i < length; i++)
            {
                char c = input[i];
                if (c == '0')
                    result[i] = false;
                else if (c == '1')
                    result[i] = true;
                else
                {
                    Console.WriteLine($"Error: Invalid character '{c}' at position {i + 1}. Only '0' or '1' are allowed.");
                    valid = false;
                    break;
                }
            }

            if (valid) return result;
        }
    }

    static string BoolArrayToString(bool[] array) =>
        string.Concat(array.Select(b => b ? '1' : '0'));
    
    static public void PrintMatrix()
    {
        for (int row = 0; row < _matrix.Dimension; row++)
        {
            var line = new System.Text.StringBuilder(_matrix.Dimension);
            for (int col = 0; col < _matrix.Dimension; col++)
            {
                line.Append(_matrix.Values[row, col] ? '1' : '0');
            }
            Console.WriteLine(line.ToString());
        }
    }

}