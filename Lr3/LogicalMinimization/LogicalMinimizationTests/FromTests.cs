using LogicalMinimization;

namespace LogicalMinimizationTests;

[TestClass]
public sealed class FromTests
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
    public void ToStringTests()
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
        Assert.AreEqual(value, "(a)|(b&c)");
    }
}