using System.Text.Json;
using LogicalMinimization;
using LogicalParser;

namespace LogicalScheme;

class Program
{
    private static void Main(string[] args)
    {
        var blocks = PrintSum();
        blocks.AddRange(PrintCarryOut());
        
        PrintAllBlocks(blocks);
        Console.ReadKey();
    }

    private static List<Pair<string, string>> PrintSum()
    {
        Console.WriteLine("Sum:");
        var formulasForSum = BuildFormulasForSum();
        var options = OptionsBuilder.BuildOptions(BinarySumScheme.GetVariables());
        var table = new OptimizedTable(formulasForSum, options);
        Console.WriteLine(table.Content.Trim('\n'));
        
        Console.WriteLine($"Conjunctive form: {table.ConjunctiveForm}");
        var form = KarnaughSolver.Solve(FormulaParser.Parse(table.ConjunctiveForm), FormType.Conjunctive);
        Console.WriteLine($"After minimizing: {form}");
        Console.WriteLine();
        
        return PrintUsedBlocks(form, options);
    }

    private static List<Pair<string, string>> PrintCarryOut()
    {
        Console.WriteLine("\nCarry out:");
        var formulasForCarryOut = BuildFormulasForCarryOut();
        var options = OptionsBuilder.BuildOptions(BinarySumScheme.GetVariables());
        var table = new OptimizedTable(formulasForCarryOut, options);
        Console.WriteLine(table.Content.Trim('\n'));
        
        Console.WriteLine($"Conjunctive form: {table.ConjunctiveForm}");
        var form = KarnaughSolver.Solve(FormulaParser.Parse(table.ConjunctiveForm), FormType.Conjunctive);
        Console.WriteLine($"After minimizing: {form}");
        Console.WriteLine();

        return PrintUsedBlocks(form, options);
    }

    private static List<Pair<string, string>> PrintUsedBlocks(Form form,  List<Dictionary<string,bool>> options)
    {
        var resultFormula = FormulaParser.Parse(form.ToString());
        var resultTable = new OptimizedTable(resultFormula.ToString());
        
        int i = 1;
        List<Pair<string, string>> blocksWithParameters = [];
        resultTable.UsedBlocks.ForEach(block =>
        {
            var usedBlockFormula = FormulaParser.Parse(block);
            var usedBlockTable = new OptimizedTable([usedBlockFormula], options);
            var variables = FormulaParser.FindAllPropositionalVariables(block);
            variables.Sort();

            string sortedVariables = "";
            variables.ForEach(v =>
            {
                sortedVariables += v + " ";
            });
            
            blocksWithParameters.Add(new Pair<string, string>(block, sortedVariables + usedBlockTable.IndexForm));
        });

        foreach (var pair in blocksWithParameters)
        {
            Console.WriteLine($"{i}. {pair.Key} {pair.Value}");
            i++;
        }

        return blocksWithParameters;
    }

    private static void PrintAllBlocks(List<Pair<string, string>> blocks)
    {
        for (int i = 0; i < blocks.Count; i++)
        {
            if (!blocks[i].Key.Contains("replaced")) Console.WriteLine($"Block {i + 1}. {blocks[i].Key} {blocks[i].Value}");
            else
            {
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine($"Block {i + 1}. {blocks[i].Key} {blocks[i].Value}");
                Console.ResetColor();
            }
            
            for (int j = i + 1; j < blocks.Count; j++)
            {
                if (blocks[j].Value == blocks[i].Value) blocks[j].Key = $"replaced by {blocks[i].Key}[{i + 1}]" + blocks[j].Key;
            }
        }
    }

    private static List<IEvaluatable> BuildFormulasForSum()
    {
        List<IEvaluatable> formulasForSum =
        [
            new PropositionalVariable("a"),
            new PropositionalVariable("b"),
            new PropositionalVariable("c"),
            new BinarySumScheme()
        ];

        return formulasForSum;
    }

    private static List<IEvaluatable> BuildFormulasForCarryOut()
    {
        List<IEvaluatable> formulasForCarryOut =
        [
            new PropositionalVariable("a"),
            new PropositionalVariable("b"),
            new PropositionalVariable("c"),
            new BinaryCarryOutScheme()
        ];

        return formulasForCarryOut;
    }
}