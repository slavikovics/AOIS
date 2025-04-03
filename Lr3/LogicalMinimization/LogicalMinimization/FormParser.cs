using System.Collections.Specialized;
using System.Text.RegularExpressions;

namespace LogicalMinimization;

public class FormParser
{
    public static Form ParseForm(string input)
    {
        string pattern = @"\([a-zA-Z&|!]+\)";
        MatchCollection matches = Regex.Matches(input, pattern);

        FormType formType;
        if (matches[0].Value.Contains("&")) formType = FormType.Disjunctive;
        else formType = FormType.Conjunctive;
        
        List<Expression> expressions = new List<Expression>();
        foreach (Match match in matches) expressions.Add(ParseExpression(match.Value.Trim('(', ')')));

        return new Form(expressions, formType);
    }

    public static Expression ParseExpression(string input)
    {
        string pattern = @"!?[a-zA-Z]";
        MatchCollection matches = Regex.Matches(input, pattern);

        List<Variable> variables = [];
        foreach (Match match in matches)
        {
            string variable = match.Value;
            if (variable.StartsWith("!")) variables.Add(new Variable(variable.Substring(1), false));
            else variables.Add(new Variable(variable, true));
        }
        
        return new Expression(variables);
    }
}