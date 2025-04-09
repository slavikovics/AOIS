using LogicalMinimization;
using LogicalParser;

namespace LogicalMinimizationTests;

[TestClass]
public sealed class FormTests
{
    [TestMethod]
    public void StickTest()
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
        form.Stick();
        form.Stick();
        
        Assert.AreEqual(form.Expressions.Count, 2);
    }

    [TestMethod]
    public void StickEverythingTest()
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
        Assert.AreEqual(form.Expressions.Count, 2);
    }

    [TestMethod]
    public void DisjunctionTests()
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
        var value = form.ToString();
        Assert.AreEqual(value, "a|(b&c)");
        
        List<Variable> variables6 = [new Variable("a", true), 
            new Variable("c", true)];
        Expression expression6 = new Expression(variables6);
        
        form.Expressions.Add(expression6);
        
        value = form.ToString();
        Assert.AreEqual(value, "a|(b&c)|(a&c)");
        form.RemoveUnnecessary();
        
        value = form.ToString();
        Assert.AreEqual(value, "a|(b&c)");
    }

    [TestMethod]
    public void ConjunctionTest()
    {
        List<Variable> variables1 = [new Variable("a", true), 
            new Variable("b", true), 
            new Variable("c", true)];
        Expression expression1 = new Expression(variables1);
        
        List<Variable> variables2 = [new Variable("a", true), 
            new Variable("b", false), 
            new Variable("c", true)];
        Expression expression2 = new Expression(variables2);

        List<Expression> expressions = [expression1, expression2];
        Form form = new Form(expressions, FormType.Conjunctive);
        
        form.StickEverything();
        var value = form.ToString();
        Assert.AreEqual(value, "(a|c)");
        
        List<Variable> variables6 = [new Variable("a", true), 
            new Variable("b", true),
            new Variable("c", true)];
        Expression expression6 = new Expression(variables6);
        
        form.Expressions.Add(expression6);
        
        value = form.ToString();
        Assert.AreEqual(value, "(a|c)&(a|b|c)");
        form.RemoveUnnecessary();
        
        value = form.ToString();
        Assert.AreEqual(value, "(a|c)");
    }
    
        [TestMethod]
    public void MinimizeTest6()
    {
        IEvaluatable formula = FormulaParser.Parse("((((a|b)&(!c&d))->e)~a)");
        var disjunctive = CalculationSolver.Solve(formula, FormType.Disjunctive);
        var conjunctive = CalculationSolver.Solve(formula, FormType.Conjunctive);
        Assert.AreEqual(disjunctive.ToString(), "(a&!d)|(a&e)|(a&c)|(!a&b&!c&d&!e)");
        Assert.AreEqual(conjunctive.ToString(), "(a|d)&(a|!e)&(a|!c)&(!a|c|!d|e)");
    }
    
    [TestMethod]
    public void MinimizeTest7()
    {
        IEvaluatable formula = FormulaParser.Parse("a|b|c|d|e");
        var disjunctive = CalculationSolver.Solve(formula, FormType.Disjunctive);
        var conjunctive = CalculationSolver.Solve(formula, FormType.Conjunctive);
        Assert.AreEqual(disjunctive.ToString(), "e|d|c|b|a");
        Assert.AreEqual(conjunctive.ToString(), "(a|b|c|d|e)");
    }

    [TestMethod]
    public void MinimizeTest8()
    {
        IEvaluatable formula = FormulaParser.Parse("((((a->b)&(!c&d))~e)->a)");
        var disjunctive = CalculationSolver.Solve(formula, FormType.Disjunctive);
        var conjunctive = CalculationSolver.Solve(formula, FormType.Conjunctive);
        Assert.AreEqual(disjunctive.ToString(), "(!d&e)|(c&e)|(!c&d&!e)");
        Assert.AreEqual(conjunctive.ToString(), "(a|d|e)&(a|!c|e)&(a|c|!d|!e)");
    }
    
    [TestMethod]
    public void MinimizeTest9()
    {
        IEvaluatable formula = FormulaParser.Parse("a&b&c&d&e");
        var disjunctive = CalculationSolver.Solve(formula, FormType.Disjunctive);
        var conjunctive = CalculationSolver.Solve(formula, FormType.Conjunctive);
        Assert.AreEqual(disjunctive.ToString(), "(a&b&c&d&e)");
        Assert.AreEqual(conjunctive.ToString(), "a&b&c&d&e");
    }

    [TestMethod]
    public void MinimizeTest10()
    {
        IEvaluatable formula = FormulaParser.Parse("!((a->b)&(a->c)&(b|d))");
        var disjunctive = CalculationSolver.Solve(formula, FormType.Disjunctive);
        var conjunctive = CalculationSolver.Solve(formula, FormType.Conjunctive);
        Assert.AreEqual(disjunctive.ToString(), "(!b&!d)|(a&!b)|(a&!c)");
        Assert.AreEqual(conjunctive.ToString(), "(a|!d)&(a|!b)&(!b|!c)");
    }
}