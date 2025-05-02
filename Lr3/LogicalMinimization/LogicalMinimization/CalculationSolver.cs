using LogicalParser;

namespace LogicalMinimization;

public static class CalculationSolver
{
    public static Form Solve(IEvaluatable formula, FormType type)
    {
        Form form;
        
        Table table = new Table(formula.ToString().ToLower());
        string disjunction = table.DisjunctiveForm;
        string conjunction = table.ConjunctiveForm;
        
        if (type == FormType.Disjunctive) form = FormParser.ParseForm(disjunction);
        else form = FormParser.ParseForm(conjunction);
        
        form.StickEverything();
        form.RemoveUnnecessary(); 
            
        return form;
    }
    
    public static Form Solve(OptimizedTable table, FormType type)
    {
        Form form;
        
        string disjunction = table.DisjunctiveForm;
        string conjunction = table.ConjunctiveForm;
        
        if (type == FormType.Disjunctive) form = FormParser.ParseForm(disjunction);
        else form = FormParser.ParseForm(conjunction);
        
        form.StickEverything();
        form.RemoveUnnecessary(); 
            
        return form;
    }
}