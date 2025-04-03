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
        Console.WriteLine("Calculation method:");
        Form form = FormParser.ParseForm(formString);
        form.StickEverything();
        Console.WriteLine($"After sticking: {form.ToString()}");
        form.RemoveUnnecessary();
        Console.WriteLine($"After minimizing: {form.ToString()}");
    }
    
    private static void CalcTableMethod(string formString)
    {
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
        Console.WriteLine("Karnaugh method:");
        KarnaughMap map = new KarnaughMap(formString);
        var result = map.Minimize();
        Console.WriteLine($"After minimizing: {result.ToString()}");
    }
}