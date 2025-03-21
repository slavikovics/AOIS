using LogicalParser;

namespace LogicalParserConsole;

class Program
{
    static void Main(string[] args)
    {
        while (true)
        {
            Console.WriteLine("Enter logical formula");
            string? formula = Console.ReadLine();
            if (formula is null) return;

            Table table = new Table(formula.ToLower());
            Console.WriteLine("Table:");
            Console.WriteLine(table.Content);
            Console.WriteLine($"Disjunctive form: {table.DisjunctiveForm}");
            Console.WriteLine($"Disjunction digital form: {table.DigitalDisjunctiveForm}");
            Console.WriteLine($"Conjunctive form: {table.ConjunctiveForm}");
            Console.WriteLine($"Conjunction digital form: {table.DigitalConjunctiveForm}");
            Console.WriteLine($"Index form: {table.IndexForm}");
            Console.WriteLine("");
        }
        
        Console.ReadKey();
    }
}