using System.Text;

namespace LogicalParser;

public class Table
{
    private List<int> ColumnSizes { get; set; }

    public string Content;

    public string ConjunctiveForm { get; private set; }

    public string DisjunctiveForm { get; private set; }

    public string DigitalDisjunctiveForm { get; private set; }

    public string DigitalConjunctiveForm { get; private set; }

    public string IndexForm { get; private set; }

    private int _length;

    public Table(List<string> formulas, List<Dictionary<string, bool>> options)
    {        
        Content = "";
        ConjunctiveForm = "";
        DisjunctiveForm = "";
        DigitalDisjunctiveForm = "";
        DigitalConjunctiveForm = "";
        IndexForm = "";
        ColumnSizes = new List<int>();
        
        BuildHeading(formulas);
        BuildBody(options, formulas);
    }

    public Table(string input)
    {
        input = input.ToLower();
        Content = "";
        ConjunctiveForm = "";
        DisjunctiveForm = "";
        DigitalDisjunctiveForm = "";
        DigitalConjunctiveForm = "";
        IndexForm = "";
        ColumnSizes = new List<int>();
        var formulas = new List<string>();
        
        var variables = FormulaParser.FindAllPropositionalVariables(input);
        FormulaParser.Parse(input, formulas);
        var options = OptionsBuilder.BuildOptions(variables);
        
        BuildHeading(variables.Concat(formulas).ToList());
        BuildBody(options, variables.Concat(formulas).ToList());
    }

    private void BuildHeading(List<string> formulas)
    { 
        _length = 0;
        
        foreach (var formula in formulas)
        {
            _length += formula.Length + 2;
            ColumnSizes.Add(formula.Length + 2);
        }
        
        Content = new string(' ', _length) + "\n";
        foreach (var formula in formulas) Content += $" {formula} ";
        Content += "\n";
    }

    private void BuildBody(List<Dictionary<string, bool>> options, List<string> formulas)
    {
        bool lastEvaluation = false;
        if (options.Count == 0) options.Add([]);
        
        for (int i = 0; i < options.Count; i++)
        {
            for (int j = 0; j < formulas.Count; j++)
            {
                string marginLeft = new string(' ', ColumnSizes[j] / 2);
                string marginRight = new string(' ', ColumnSizes[j] - 1 - marginLeft.Length);
                lastEvaluation = FormulaParser.Parse(formulas[j]).Evaluate(options[i]);
                Content += $"{marginLeft}{ToString(lastEvaluation)}{marginRight}";
            }

            if (lastEvaluation)
            {
                BuildDisjunction(options, i);
                BuildDisjunctionDigitalForm(lastEvaluation, i);
            }
            else
            {
                BuildConjunction(options, i);
                BuildConjunctionDigitalForm(lastEvaluation, i);
            }

            IndexForm += ToString(lastEvaluation);
            Content += '\n';
        }

        Content += "\n";
        DigitalDisjunctiveForm += ")";
        DigitalConjunctiveForm += ")";
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

    private void BuildDisjunctionDigitalForm(bool lastEvaluation, int i)
    {
        if (DigitalDisjunctiveForm == "") DigitalDisjunctiveForm += "| (";
        else DigitalDisjunctiveForm += ",";
        DigitalDisjunctiveForm += i;
    }
    
    private void BuildConjunctionDigitalForm(bool lastEvaluation, int i)
    {
        if (DigitalConjunctiveForm == "") DigitalConjunctiveForm += "& (";
        else DigitalConjunctiveForm += ",";
        DigitalConjunctiveForm += i;
    }

    private string ToString(bool input)
    {
        if (input) return "1";
        return "0";
    }
}