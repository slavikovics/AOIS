using LogicalMinimization;
using LogicalParser;

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

        //SolveFirstTriggerInput();
        SolveSecondTriggerInput();
        SolveThirdTriggerInput();
        SolveFourthTriggerInput();
        
        Console.ReadKey();
    }

    public static void PrintTable(OptimizedTable table)
    {
        Console.WriteLine(table.Content.Trim());
        
        Console.WriteLine($"Disjunctive: {table.DisjunctiveForm}");
        var form = KarnaughSolver.Solve(FormulaParser.Parse(table.DisjunctiveForm), FormType.Disjunctive);
        Console.WriteLine($"Disjunctive minimized: {form}");
        
        Console.WriteLine($"Conjunctive: {table.ConjunctiveForm}");
        form = KarnaughSolver.Solve(FormulaParser.Parse(table.ConjunctiveForm), FormType.Conjunctive);
        Console.WriteLine($"Conjunctive minimized: {form}");
    }

    public static void SolveFirstTriggerInput()
    {
        Console.WriteLine("\nFirst trigger:");
        
        var optionsForSecond = BuildOptionsForSecondTrigger();
        var table = new OptimizedTable([new FirstTriggerInput()], BuildOptions());
        
        PrintTable(table);
    }

    public static void SolveSecondTriggerInput()
    {
        Console.WriteLine("\nSecond trigger:");
        
        var optionsForSecond = BuildOptionsForSecondTrigger();
        var table = new OptimizedTable([new FirstTriggerInput(), 
            new FirstTriggerOutput(), 
            new SecondTriggerInput()], optionsForSecond);
        
        PrintTable(table);
    }

    private static List<Dictionary<string, bool>> BuildOptionsForSecondTrigger()
    {
        var options1 = BuildOptions();
        var options2 = BuildComplexOptions(new FirstTriggerOutput(), "b");
        return MergeOptions(options1, options2);
    }

    public static void SolveThirdTriggerInput()
    {
        Console.WriteLine("\nThird trigger:");
        
        var optionsForSecond = BuildOptionsForThirdTrigger();
        var table = new OptimizedTable([
            new FirstTriggerInput(), 
            new FirstTriggerOutput(), 
            new SecondTriggerOutput(), 
            new ThirdTriggerInput()], 
            optionsForSecond);
        
        PrintTable(table);
    }
    
    private static List<Dictionary<string, bool>> BuildOptionsForThirdTrigger()
    {
        var options1 = BuildOptions();
        var options2 = BuildComplexOptions(new FirstTriggerOutput(), "b");
        var options3 = BuildComplexOptions(new SecondTriggerOutput(), "c");
        return MergeOptions(MergeOptions(options1, options2), options3);
    }

    public static void SolveFourthTriggerInput()
    {
        Console.WriteLine("\nFourth trigger:");
        
        var optionsForSecond = BuildOptionsForFourthTrigger();
        var table = new OptimizedTable([
            new FirstTriggerInput(), 
            new FirstTriggerOutput(), 
            new SecondTriggerOutput(), 
            new ThirdTriggerOutput(), 
            new FourthTriggerInput()], 
            optionsForSecond);
        
        PrintTable(table);
    }
    
    private static List<Dictionary<string, bool>> BuildOptionsForFourthTrigger()
    {
        var options1 = BuildOptions();
        var options2 = BuildComplexOptions(new FirstTriggerOutput(), "b");
        var options3 = BuildComplexOptions(new SecondTriggerOutput(), "c");
        var options4 = BuildComplexOptions(new ThirdTriggerOutput(), "d");
        return MergeOptions(MergeOptions(MergeOptions(options1, options2), options3), options4);
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

    private static List<Dictionary<string, bool>> BuildComplexOptions(IEvaluatable trigger, string variableName)
    {
        var complexOptions = new List<Dictionary<string, bool>>();
        var options = BuildOptions();

        for (int i = 0; i < 16; i++)
        {
            bool value = trigger.Evaluate(options[i]);
            var dict = new Dictionary<string, bool>();
            dict.Add(variableName, value);
            complexOptions.Add(dict);
        }
        
        return complexOptions;
    }

    private static List<Dictionary<string, bool>> MergeOptions(List<Dictionary<string, bool>> left,
        List<Dictionary<string, bool>> right)
    {
        var result = new List<Dictionary<string, bool>>();

        for (int i = 0; i < left.Count; i++)
        {
            result.Add(left[i]);

            foreach (var argumentValue in right[i])
            {
                result[i].Add(argumentValue.Key, argumentValue.Value);
            }
        }
        
        return result;
    }
}