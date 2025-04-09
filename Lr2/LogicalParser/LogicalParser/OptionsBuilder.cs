using System.Diagnostics.CodeAnalysis;

namespace LogicalParser;

public static class OptionsBuilder
{
    public static List<string> BuildArguments(List<string> arguments)
    {
        List<string> options = [];
        int count = arguments.Count;
        if (count == 0) return options;
        
        string firstOption = new string('0', count -1) + "0";
        string diff = new string('0', count -1) + "1";
        options.Add(firstOption);

        while (options.Last().Contains("0"))
        {
            firstOption = Sum(firstOption, diff);
            options.Add(firstOption);
        }
        
        return options;
    }
    
    private static string Sum(string firstArgument, string secondArgument)
    {
        string result = "";

        int memorizedOne = 0;
        for (int i = firstArgument.Length - 1; i >= 0; i--)
        {
            int iterationResult = Convert.ToInt32(firstArgument.Substring(i, 1)) + Convert.ToInt32(secondArgument.Substring(i, 1)) + memorizedOne;
            switch (iterationResult)
            {
                case 0: memorizedOne = 0;
                    result = "0" + result;
                    break;
                case 1: memorizedOne = 0;
                    result = "1" + result;
                    break;
                case 2: memorizedOne = 1;
                    result = "0" + result;
                    break;
                case 3: memorizedOne = 1;
                    result = "1" + result;
                    break;
            }
        }
        if (memorizedOne == 1) result = "1" + result;

        return result;
    }

    private static bool CharConversion(char c)
    {
        if (c == '0') return false;
        return true;
    }
    
    public static List<Dictionary<string, bool>> BuildOptions(List<string> arguments, List<string> propositionalVariables)
    {
        List<Dictionary<string, bool>> options = new List<Dictionary<string, bool>>();
        
        for (int i = 0; i < arguments.Count; i++)
        {
            if (arguments[0].Length != propositionalVariables.Count)
                throw new ArgumentException("Propositional variable count mismatch");

            Dictionary<string, bool> option = new Dictionary<string, bool>();

            for (int j = 0; j < arguments[i].Length; j++)
            {
                option.Add(propositionalVariables[j], CharConversion(arguments[i][j]));
            }
            
            options.Add(option);
        }
        
        return options;
    }

    public static List<Dictionary<string, bool>> BuildOptions(List<string> propositionalVariables)
    {
        List<string> arguments = BuildArguments(propositionalVariables);
        return BuildOptions(arguments, propositionalVariables);
    }
}