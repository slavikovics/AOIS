using LogicalMinimization;
using LogicalParser;

namespace LogicalScheme;

public class BitFunctionMinimizer
{
    private static Form MinimizeBitFunctionVariation(BitFunction bitFunction)
    {
        var formulas = BuildFormulas(bitFunction);
        var options = BuildOptions();
        var table = new OptimizedTable(formulas, options);

        var conjunctiveForm = CalculationSolver.Solve(table, FormType.Conjunctive);
        var disjunctiveForm = CalculationSolver.Solve(table, FormType.Disjunctive);

        if (conjunctiveForm.ToString().Length < disjunctiveForm.ToString().Length) return conjunctiveForm;
        return disjunctiveForm;
    }

    private static Form MinimizeFirstBitFunction()
    {
        var form1 = MinimizeBitFunctionVariation(new FirstBitFunction(false, false));
        return form1;
    }
    
    private static Form MinimizeSecondBitFunction()
    {
        var form1 = MinimizeBitFunctionVariation(new SecondBitFunction(false, false));
        return form1;
    }
    
    private static Form MinimizeThirdBitFunction()
    {
        var form1 = MinimizeBitFunctionVariation(new ThirdBitFunction(false, false));
        return form1;
    }
    
    private static Form MinimizeFourthBitFunction()
    {
        var form1 = MinimizeBitFunctionVariation(new FourthBitFunction(false, false));
        return form1;
    }

    private static List<Dictionary<string, bool>> BuildOptions()
    {
        return OptionsBuilder.BuildOptions(["b", "c", "d"]).GetRange(0, 8);
    }

    private static List<IEvaluatable> BuildFormulas(BitFunction function)
    {
        return [
            new PropositionalVariable("b"),
            new PropositionalVariable("c"),
            new PropositionalVariable("d"),
            function
        ];
    }

    public static void BuildMinimizedScheme()
    {
        var minForm1 = MinimizeFirstBitFunction();
        var minForm2 = MinimizeSecondBitFunction();
        var minForm3 = MinimizeThirdBitFunction();
        var minForm4 = MinimizeFourthBitFunction();
        var options = BuildOptions();

        Console.WriteLine("\n" + minForm1.ToString());
        var blocks = Program.PrintUsedBlocks(minForm1, options);
        Console.WriteLine("\n" + minForm2.ToString());
        blocks.AddRange(Program.PrintUsedBlocks(minForm2, options));
        Console.WriteLine("\n" + minForm3.ToString());
        blocks.AddRange(Program.PrintUsedBlocks(minForm3, options));
        Console.WriteLine("\n" + minForm4.ToString());
        blocks.AddRange(Program.PrintUsedBlocks(minForm4, options));
        
        Console.WriteLine("\nAll blocks for BCD+2 conversion:");
        Program.PrintAllBlocks(blocks);
    }
}