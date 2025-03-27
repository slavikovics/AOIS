using LogicalMinimization;

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
    }
}