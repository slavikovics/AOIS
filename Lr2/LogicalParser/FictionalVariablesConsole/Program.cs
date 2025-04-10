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
            Console.WriteLine();
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
            
            Console.WriteLine("Found fictional variables:");
            foreach (var variable in fictionalVariablesFinder.FictionalVariables)
            {
                Console.WriteLine(variable);
            }

            if (fictionalVariablesFinder.FictionalVariables.Count == 0)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("No fictional variables found");
                Console.ForegroundColor = ConsoleColor.White;
            }
        }
        catch (Exception e)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("It's not logical formula");
            Console.ForegroundColor = ConsoleColor.White;
        }
    }
}