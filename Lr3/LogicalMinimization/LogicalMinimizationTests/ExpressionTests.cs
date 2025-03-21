using LogicalMinimization;

namespace LogicalMinimizationTests;

[TestClass]
public sealed class ExpressionTests
{
    [TestMethod]
    public void StickTogetherTest()
    {
        List<Variable> variables1 = [new Variable("a", true), 
                                     new Variable("b", false), 
                                     new Variable("c", true)];
        
        List<Variable> variables2 = [new Variable("a", true), 
                                     new Variable("b", true), 
                                     new Variable("c", true)];
        
        
        
    }
}