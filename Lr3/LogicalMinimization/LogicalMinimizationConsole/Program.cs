using LogicalMinimization;
using LogicalParser;

namespace LogicalMinimizationConsole;

class Program
{
    private static void Main(string[] args)
    {
        while (true)
        {
            Console.WriteLine("Enter logical formula");
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            string? formula = Console.ReadLine();
            Console.Clear();
            if (formula is null) continue;
            Console.WriteLine($"Formula: {formula}");
            Console.ForegroundColor = ConsoleColor.White;
            
            Table table = new Table(formula.ToLower());
            string disjunction = table.DisjunctiveForm;
            Console.WriteLine($"Disjunction: {disjunction}");
            string conjunction = table.ConjunctiveForm;
            Console.WriteLine($"Conjunction: {conjunction}");
            
            try
            {
                CalcMethod(disjunction, conjunction);
                CalcTableMethod(disjunction, conjunction);
                KarnaughMethod(disjunction);
            }
            catch (Exception e)
            {
                Console.WriteLine("Something went wrong.");
            }
            Console.WriteLine();
        }
    }

    private static void CalcMethod(string disjunction, string conjunction)
    {
        Console.WriteLine();
        Console.ForegroundColor = ConsoleColor.DarkBlue;
        Console.WriteLine("Calculation method:\n");
        Console.ForegroundColor = ConsoleColor.White;
        Console.WriteLine("DISJUNCTION:");
        
        Form disjunctiveForm = FormParser.ParseForm(disjunction);
        disjunctiveForm.StickEverything();
        
        Console.WriteLine($"After sticking: {disjunctiveForm}");
        disjunctiveForm.RemoveUnnecessary();
        
        Console.ForegroundColor = ConsoleColor.DarkGreen;
        Console.WriteLine($"After minimizing: {disjunctiveForm}");
        Console.ForegroundColor = ConsoleColor.White;
        
        Console.WriteLine("\nCONJUNCTION:");
        Form conjunctiveForm = FormParser.ParseForm(conjunction);
        conjunctiveForm.StickEverything();
        
        Console.WriteLine($"After sticking: {conjunctiveForm}");
        conjunctiveForm.RemoveUnnecessary();
        
        Console.ForegroundColor = ConsoleColor.DarkGreen;
        Console.WriteLine($"After minimizing: {conjunctiveForm}");
        Console.ForegroundColor = ConsoleColor.White;
    }

    private static void OneCalcTable(string formula)
    {
        Form form = FormParser.ParseForm(formula);
        
        var startExpressions = new List<Expression>(); 
        startExpressions.AddRange(form.Expressions);
        
        form.StickEverything();
        Console.WriteLine($"After sticking: {form.ToString()}");

        CalculationTable calculationTable = new CalculationTable(form, startExpressions);
        Console.ForegroundColor = ConsoleColor.DarkGreen;
        Console.WriteLine($"After minimizing: {form}");
        Console.ForegroundColor = ConsoleColor.White;
        
        List<Expression> rows = [new Expression([])];
        rows.AddRange(startExpressions);
        
        List<Expression> columns = [new Expression([])];
        columns.AddRange(form.Expressions);

        var table = new TableBuilder<Expression, Expression, bool>(rows, columns, calculationTable.FlipCrosses());
        Console.WriteLine(table.Build());
    }
    
    private static void CalcTableMethod(string disjunction, string conjunction)
    {
        Console.WriteLine();
        Console.ForegroundColor = ConsoleColor.DarkBlue;
        Console.WriteLine("Calculation-table method:");
        Console.ForegroundColor = ConsoleColor.White;
        Console.WriteLine("\nDISJUNCTION:");
        OneCalcTable(disjunction);
        Console.WriteLine("\nCONJUNCTION:");
        OneCalcTable(conjunction);
    }
    
    private static void KarnaughMethod(string formString)
    {
        Console.WriteLine();
        Console.ForegroundColor = ConsoleColor.DarkBlue;
        Console.WriteLine("Karnaugh method:");
        Console.ForegroundColor = ConsoleColor.White;

        MapSplitter mapSplitter = new MapSplitter();
        var form = mapSplitter.SplitFormula(FormulaParser.Parse(formString));
        
        KarnaughMap map = new KarnaughMap(formString);

        string rowVariables = "";
        map.RowVariables.ForEach(x => rowVariables += x);
        List<string> rows = [rowVariables];
        rows.AddRange(map.RowArguments);
        
        string columnVariables = "";
        map.ColumnVariables.ForEach(x => columnVariables += x);
        List<string> columns = [columnVariables];
        columns.AddRange(map.ColumnArguments);
        
        var table = new TableBuilder<string, string, MapValue>(rows, columns, map.Table);
        Console.WriteLine(table.Build());
        
        var disjunctional = map.MinimizeToDisjunctional();
        Console.ForegroundColor = ConsoleColor.DarkGreen;
        Console.WriteLine($"Disjunctional after minimizing: {form.ToString()}");
        Console.ForegroundColor = ConsoleColor.White;
        
        var conjunctional = map.MinimizeToConjunctional();
        Console.ForegroundColor = ConsoleColor.DarkGreen;
        Console.WriteLine($"Conjunctional after minimizing: {conjunctional.ToString()}");
        Console.ForegroundColor = ConsoleColor.White;
    }
}