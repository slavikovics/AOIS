using LogicalMinimization;

namespace LogicalMinimizationTests;

[TestClass]
public sealed class ExpressionTests
{
    [TestMethod]
    public void StickTogetherTest1()
    {
        List<Variable> variables1 = [new Variable("a", true), 
                                     new Variable("b", false), 
                                     new Variable("c", true)];
        
        List<Variable> variables2 = [new Variable("a", true), 
                                     new Variable("b", true), 
                                     new Variable("c", true)];

        Expression expression1 = new Expression(variables1);
        Expression expression2 = new Expression(variables2);

        Expression expressionRes = expression1.StickTogether(expression2);
        Assert.AreEqual(expressionRes.ToString(), "ac");
    }
    
    [TestMethod]
    public void StickTogetherTest2()
    {
        List<Variable> variables1 = [new Variable("a", false), 
                                     new Variable("b", false), 
                                     new Variable("c", false)];
        
        List<Variable> variables2 = [new Variable("a", false), 
                                     new Variable("b", false), 
                                     new Variable("c", true)];

        Expression expression1 = new Expression(variables1);
        Expression expression2 = new Expression(variables2);

        Expression expressionRes = expression1.StickTogether(expression2);
        Assert.AreEqual(expressionRes.ToString(), "!a!b");
    }
    
    [TestMethod]
    public void StickTogetherTest3()
    {
        List<Variable> variables1 = [new Variable("a", false), 
                                     new Variable("b", false), 
                                     new Variable("c", false)];
        
        List<Variable> variables2 = [new Variable("a", false), 
                                     new Variable("b", false)];

        Expression expression1 = new Expression(variables1);
        Expression expression2 = new Expression(variables2);

        try
        {
            Expression expressionRes = expression1.StickTogether(expression2);
            Assert.Fail();
        }
        catch (ArgumentException e)
        {
            Assert.AreEqual(e.Message, "Cannot compare expressions with different variables sets.");
        }
    }
}