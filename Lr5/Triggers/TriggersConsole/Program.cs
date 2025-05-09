using LogicalMinimization;

namespace TriggersConsole;

class Program
{
    static void Main(string[] args)
    {
        var options = BuildOptions();
        var table = new OptimizedTable([
            new FirstTriggerInput(), 
            new FirstTriggerOutput(), 
            new SecondTriggerInput(), 
            new SecondTriggerOutput(), 
            new ThirdTriggerInput(), 
            new ThirdTriggerOutput(),
            new FourthTriggerInput(),
            new FourthTriggerOutput(),
        ], options);
        Console.WriteLine(table.Content);
        
        table = new OptimizedTable([new FirstTriggerInput()], options);
        Console.WriteLine(table.DisjunctiveForm);
        table = new OptimizedTable([new SecondTriggerInput()], options);
        Console.WriteLine(table.DisjunctiveForm);
        table = new OptimizedTable([new ThirdTriggerInput()], options);
        Console.WriteLine(table.DisjunctiveForm);
        table = new OptimizedTable([new FourthTriggerInput()], options);
        Console.WriteLine(table.DisjunctiveForm);
        Console.ReadKey();
    }

    private static List<Dictionary<string, bool>> BuildOptions()
    {
        var options = new List<Dictionary<string, bool>>();

        for (int i = 0; i < 16; i++)
        {
            bool value = false || i % 2 == 0;
            
            var dict = new Dictionary<string, bool>();
            dict.Add("a", value);
            options.Add(dict);
        }
        
        return options;
    }
}