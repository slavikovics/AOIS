using LogicalParser;

namespace LogicalMinimization;

public static class CalculationTableSolver
{
    public static Form Solve(IEvaluatable formula, FormType type)
    {
        Form form;
        List<Expression> startExpressions = [];
        
        Table table = new Table(formula.ToString().ToLower());
        string disjunction = table.DisjunctiveForm;
        string conjunction = table.ConjunctiveForm;
        
        if (type == FormType.Disjunctive)
        {
            form = FormParser.ParseForm(disjunction);
            startExpressions.AddRange(form.Expressions);
            form.StickEverything();
            CalculationTable calculationTable = new CalculationTable(form, startExpressions);

            return form;
        }
        else
        {
            form = FormParser.ParseForm(conjunction);
            startExpressions.AddRange(form.Expressions);
            form.StickEverything();
            CalculationTable calculationTable = new CalculationTable(form, startExpressions);

            return form;
        }
    }
}