using LogicalParser;

namespace FictionalVariablesConsole;

class Program
{
    static void Main(string[] args)
    {
        while (true)
        {
            Console.WriteLine("Input logical formula");
            var formula = Console.ReadLine();
            PrintFictionalVariables(formula);
            Console.WriteLine();
        }
    }

    static void PrintFictionalVariables(string formula)
    {
        try
        {
            if (!FormulaStringChecker.Check(formula)) throw new Exception("Not logical formula");
            FictionalVariablesFinder fictionalVariablesFinder = new FictionalVariablesFinder(formula);
            fictionalVariablesFinder.FindFictionalVariables();
            
            Console.WriteLine("Found variables:");
            foreach (var variable in fictionalVariablesFinder.FictionalVariables)
            {
                Console.WriteLine(variable);
            }
        }
        catch (Exception e)
        {
            Console.WriteLine("It's not logical formula");
        }
    }
}