using LogicalParser;

namespace LogicalMinimization;

public class OptimizedTable
{
        private List<int> ColumnSizes { get; set; }

    public List<string> UsedBlocks { get; set; }

    public string Content;

    public string ConjunctiveForm { get; private set; }

    public string DisjunctiveForm { get; private set; }
    
    public string IndexForm { get; private set; }

    private int _length;

    public OptimizedTable(List<IEvaluatable> formulas, List<Dictionary<string, bool>> options)
    {        
        Content = "";
        ConjunctiveForm = "";
        DisjunctiveForm = "";
        IndexForm = "";
        ColumnSizes = new List<int>();
        UsedBlocks = new List<string>();
        
        BuildHeading(formulas);
        BuildBody(options, formulas);
    }
    
    public OptimizedTable(string input)
    {
        input = input.ToLower();
        Content = "";
        ConjunctiveForm = "";
        DisjunctiveForm = "";
        IndexForm = "";
        ColumnSizes = new List<int>();
        var formulas = new List<string>();
        UsedBlocks = formulas;
        
        var variables = FormulaParser.FindAllPropositionalVariables(input);
        FormulaParser.Parse(input, formulas);
        var options = OptionsBuilder.BuildOptions(variables);

        var parsedFormulas = new List<IEvaluatable>();
        formulas.ForEach(f => parsedFormulas.Add(FormulaParser.Parse(f)));
        
        BuildHeading(parsedFormulas);
    }

    private void BuildHeading(List<IEvaluatable> formulas)
    { 
        _length = 0;
        
        foreach (var formula in formulas)
        {
            _length += formula.ToString().Length + 2;
            ColumnSizes.Add(formula.ToString().Length + 2);
        }
        
        Content = new string(' ', _length) + "\n";
        foreach (var formula in formulas) Content += $" {formula} ";
        Content += "\n";
    }

    private void BuildBody(List<Dictionary<string, bool>> options, List<IEvaluatable> formulas)
    {
        bool lastEvaluation = false;
        if (options.Count == 0) options.Add([]);
        
        for (int i = 0; i < options.Count; i++)
        {
            for (int j = 0; j < formulas.Count; j++)
            {
                string marginLeft = new string(' ', ColumnSizes[j] / 2);
                string marginRight = new string(' ', ColumnSizes[j] - 1 - marginLeft.Length);
                lastEvaluation = formulas[j].Evaluate(options[i]);
                Content += $"{marginLeft}{ToString(lastEvaluation)}{marginRight}";
            }

            if (lastEvaluation) BuildDisjunction(options, i);
            else BuildConjunction(options, i);
            
            IndexForm += ToString(lastEvaluation);
            Content += '\n';
        }

        Content += "\n";
    }

    private void BuildDisjunction(List<Dictionary<string, bool>> options, int i)
    { 
        if (DisjunctiveForm != "") DisjunctiveForm += "|";
        DisjunctiveForm += "(";
        foreach (var option in options[i])
        {
            if (DisjunctiveForm.Last() != '(') DisjunctiveForm += "&";
            if (option.Value) DisjunctiveForm += option.Key;
            else DisjunctiveForm += "!" + option.Key;
        }

        DisjunctiveForm += ")";
    }
    
    private void BuildConjunction(List<Dictionary<string, bool>> options, int i)
    {
        if (ConjunctiveForm != "") ConjunctiveForm += "&";
        ConjunctiveForm += "(";
        foreach (var option in options[i])
        {
            if (ConjunctiveForm.Last() != '(') ConjunctiveForm += "|";
            if (!option.Value) ConjunctiveForm += option.Key;
            else ConjunctiveForm += "!" + option.Key;
        }

        ConjunctiveForm += ")";
    }

    private string ToString(bool input)
    {
        if (input) return "1";
        return "0";
    }
}