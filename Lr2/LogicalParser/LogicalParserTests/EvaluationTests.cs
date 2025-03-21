using LogicalParser;

namespace LogicalParserTests;

[TestClass]
public class EvaluationTests
{
    [TestMethod]
    public void EvaluationTest1()
    {
        List<string> formulas = new List<string>();
        Dictionary<string, bool> variables = new Dictionary<string, bool>();
        variables.Add("a", true);
        variables.Add("b", false);
        variables.Add("c", true);
        
        IEvaluatable logicalFormula = FormulaParser.Parse("(a&b)|(a|b&c)", formulas);
        Assert.AreEqual(logicalFormula.Evaluate(variables), true);
    }
    
    [TestMethod]
    public void EvaluationTest2()
    {
        List<string> formulas = new List<string>();
        Dictionary<string, bool> variables = new Dictionary<string, bool>();
        variables.Add("a", false);
        variables.Add("b", false);
        variables.Add("c", false);
        
        IEvaluatable logicalFormula = FormulaParser.Parse("(a&b)|(a|b&c)", formulas);
        Assert.AreEqual(logicalFormula.Evaluate(variables), false);
    }
    
    [TestMethod]
    public void EvaluationTest3()
    {
        List<string> formulas = new List<string>();
        Dictionary<string, bool> variables = new Dictionary<string, bool>();
        variables.Add("a", false);
        variables.Add("b", true);
        variables.Add("c", false);
        
        IEvaluatable logicalFormula = FormulaParser.Parse("(a&b)|(a|b&c)", formulas);
        Assert.AreEqual(logicalFormula.Evaluate(variables), false);
    }
    
    [TestMethod]
    public void EvaluationTest4()
    {
        List<string> formulas = new List<string>();
        Dictionary<string, bool> variables = new Dictionary<string, bool>();
        variables.Add("a", true);
        variables.Add("b", false);
        variables.Add("c", false);
        
        IEvaluatable logicalFormula = FormulaParser.Parse("(a&b)|(a|b&c)", formulas);
        Assert.AreEqual(logicalFormula.Evaluate(variables), true);
    }
    
    [TestMethod]
    public void EvaluationTest5()
    {
        List<string> formulas = new List<string>();
        Dictionary<string, bool> variables = new Dictionary<string, bool>();
        variables.Add("a", false);
        variables.Add("b", false);
        variables.Add("c", false);
        
        IEvaluatable logicalFormula = FormulaParser.Parse("!a&!c&!b", formulas);
        Assert.AreEqual(logicalFormula.Evaluate(variables), true);
    }
}