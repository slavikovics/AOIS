using LogicalMinimization;

namespace LogicalMinimizationConsole;

class Program
{
    private static void Main(string[] args)
    {
        while (true)
        {
            Console.WriteLine("Enter logical formula");
            string? formString = Console.ReadLine();
            if (formString is null) return;

            try
            {
                CalcMethod(formString);
                CalcTableMethod(formString);
                KarnaughMethod(formString);
            }
            catch (Exception e)
            {
                Console.WriteLine("Something went wrong.");
            }
        
            Console.WriteLine();
        }
    }

    private static void CalcMethod(string formString)
    {
        Console.WriteLine();
        Console.WriteLine("Calculation method:");
        Form form = FormParser.ParseForm(formString);
        form.StickEverything();
        Console.WriteLine($"After sticking: {form.ToString()}");
        form.RemoveUnnecessary();
        Console.WriteLine($"After minimizing: {form.ToString()}");
    }
    
    private static void CalcTableMethod(string formString)
    {
        Console.WriteLine();
        Console.WriteLine("Calculation + table method:");
        Form form = FormParser.ParseForm(formString);
        
        var startExpressions = new List<Expression>(); 
        startExpressions.AddRange(form.Expressions);
        
        form.StickEverything();
        Console.WriteLine($"After sticking: {form.ToString()}");

        CalculationTable calculationTable = new CalculationTable(form, startExpressions);
        calculationTable.RemoveUnnecessaryExpressions();
        Console.WriteLine($"After minimizing: {form.ToString()}");
    }
    
    private static void KarnaughMethod(string formString)
    {
        Console.WriteLine();
        Console.WriteLine("Karnaugh method:");
        KarnaughMap map = new KarnaughMap(formString);

        string rowVariables = "";
        map.RowVariables.ForEach(x => rowVariables += x);
        List<string> rows = [rowVariables];
        rows.AddRange(map.RowArguments);
        
        string columnVariables = "";
        map.ColumnVariables.ForEach(x => columnVariables += x);
        List<string> columns = [columnVariables];
        columns.AddRange(map.ColumnArguments);
        
        //var table = new TableBuilder<string, string, bool>(rows, columns, map.Table);
        
        var disjunctional = map.MinimizeToDisjunctional();
        Console.WriteLine($"Disjunctional after minimizing: {disjunctional.ToString()}");
        
        var conjunctional = map.MinimizeToConjunctional();
        Console.WriteLine($"Conjunctional after minimizing: {conjunctional.ToString()}");
    }
}