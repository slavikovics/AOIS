using LogicalMinimization;
using LogicalParser;

namespace LogicalMinimizationTests;

[TestClass]
public sealed class CalculationTableTests
{
    [TestMethod]
    public void ShouldPlaceCrossTest()
    {
        List<Variable> variables1 = [new Variable("a", true), 
            new Variable("b", false), 
            new Variable("c", true)];
        var expression1 = new Expression(variables1);

        List<Variable> variables2 = [new Variable("a", true)];
        var expression2 = new Expression(variables2);
        Assert.AreEqual(CalculationTable.ShouldPlaceCross(expression2, expression1), true);
        
        List<Variable> variables3 = [new Variable("a", false)];
        var expression3 = new Expression(variables3);
        Assert.AreEqual(CalculationTable.ShouldPlaceCross(expression3, expression1), false);
        
        List<Variable> variables4 = [new Variable("a", true),
        new Variable("b", false)];
        var expression4 = new Expression(variables4);
        Assert.AreEqual(CalculationTable.ShouldPlaceCross(expression4, expression1), true);
    }

    [TestMethod]
    public void FindMinimizedForm()
    {
        List<Variable> variables1 = [new Variable("a", false), 
            new Variable("b", true), 
            new Variable("c", true)];
        Expression expression1 = new Expression(variables1);
        
        List<Variable> variables2 = [new Variable("a", true), 
            new Variable("b", false), 
            new Variable("c", false)];
        Expression expression2 = new Expression(variables2);
        
        List<Variable> variables3 = [new Variable("a", true), 
            new Variable("b", false), 
            new Variable("c", true)];
        Expression expression3 = new Expression(variables3);
        
        List<Variable> variables4 = [new Variable("a", true), 
            new Variable("b", true), 
            new Variable("c", false)];
        Expression expression4 = new Expression(variables4);
        
        List<Variable> variables5 = [new Variable("a", true), 
            new Variable("b", true), 
            new Variable("c", true)];
        Expression expression5 = new Expression(variables5);

        List<Expression> expressions = [expression1, expression2, expression3, expression4, expression5];
        Form form = new Form(expressions, FormType.Disjunctive);

        form.StickEverything();
        CalculationTable table = new CalculationTable(form, expressions);
        Assert.AreEqual(table.UnnecessaryExpressions.Count, 0);
        
        form.Expressions.Add(new Expression([new Variable("a", true), new Variable("c", true)]));
        CalculationTable table2 = new CalculationTable(form, expressions);
        Assert.AreEqual(table2.UnnecessaryExpressions.Count, 1);
    }
    
    [TestMethod]
    public void MinimizeTest6()
    {
        IEvaluatable formula = FormulaParser.Parse("((((a|b)&(!c&d))->e)~a)");
        var disjunctive = CalculationTableSolver.Solve(formula, FormType.Disjunctive);
        var conjunctive = CalculationTableSolver.Solve(formula, FormType.Conjunctive);
        Assert.AreEqual(disjunctive.ToString(), "(a&!d)|(a&e)|(a&c)|(!a&b&!c&d&!e)");
        Assert.AreEqual(conjunctive.ToString(), "(a|b)&(a|d)&(a|!e)&(a|!c)&(!a|c|!d|e)");
    }
    
    [TestMethod]
    public void MinimizeTest7()
    {
        IEvaluatable formula = FormulaParser.Parse("a|b|c|d|e");
        var disjunctive = CalculationTableSolver.Solve(formula, FormType.Disjunctive);
        var conjunctive = CalculationTableSolver.Solve(formula, FormType.Conjunctive);
        Assert.AreEqual(disjunctive.ToString(), "e|d|c|b|a");
        Assert.AreEqual(conjunctive.ToString(), "(a|b|c|d|e)");
    }

    [TestMethod]
    public void MinimizeTest8()
    {
        IEvaluatable formula = FormulaParser.Parse("((((a->b)&(!c&d))~e)->a)");
        var disjunctive = CalculationTableSolver.Solve(formula, FormType.Disjunctive);
        var conjunctive = CalculationTableSolver.Solve(formula, FormType.Conjunctive);
        Assert.AreEqual(disjunctive.ToString(), "a|(!d&e)|(c&e)|(!c&d&!e)");
        Assert.AreEqual(conjunctive.ToString(), "(a|d|e)&(a|!c|e)&(a|c|!d|!e)");
    }
    
    [TestMethod]
    public void MinimizeTest9()
    {
        IEvaluatable formula = FormulaParser.Parse("a&b&c&d&e");
        var disjunctive = CalculationTableSolver.Solve(formula, FormType.Disjunctive);
        var conjunctive = CalculationTableSolver.Solve(formula, FormType.Conjunctive);
        Assert.AreEqual(disjunctive.ToString(), "(a&b&c&d&e)");
        Assert.AreEqual(conjunctive.ToString(), "a&b&c&d&e");
    }

    [TestMethod]
    public void MinimizeTest10()
    {
        IEvaluatable formula = FormulaParser.Parse("!((a->b)&(a->c)&(b|d))");
        var disjunctive = CalculationTableSolver.Solve(formula, FormType.Disjunctive);
        var conjunctive = CalculationTableSolver.Solve(formula, FormType.Conjunctive);
        Assert.AreEqual(disjunctive.ToString(), "(!b&!d)|(a&!b)|(a&!c)");
        Assert.AreEqual(conjunctive.ToString(), "(a|!d)&(a|!b)&(!b|!c)");
    }
}