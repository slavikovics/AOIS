namespace LogicalParser;

public class FictionalVariablesFinder
{
    public string FormulaString { get; private set; }

    public List<string> FictionalVariables { get; private set; }
    
    public FictionalVariablesFinder(string formulaString)
    {
        FormulaString = formulaString;
        FictionalVariables = [];
    }

    public bool IsFictionalVariable(string variableName)
    {
        var formulaWithOne = FormulaString.Replace(variableName, "1");
        var formulaWithZero = FormulaString.Replace(variableName, "0");
        
        var tableOne = new Table(formulaWithOne);
        var resultOne = tableOne.IndexForm;
        
        var tableZero = new Table(formulaWithZero);
        var resultZero = tableZero.IndexForm;
        
        return resultOne == resultZero;
    }

    public void FindFictionalVariables()
    {
        var allVariables = FormulaParser.FindAllPropositionalVariables(FormulaString);

        foreach (var variable in allVariables)
        {
            if (IsFictionalVariable(variable)) FictionalVariables.Add(variable);
        }
    }
}