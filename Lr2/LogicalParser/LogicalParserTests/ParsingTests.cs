using LogicalParser;

namespace LogicalParserTests;

[TestClass]
public sealed class ParsingTests
{
    [TestMethod]
    public void FindingSignTest()
    {
        Assert.AreEqual(FormulaParser.FindMiddleSign("a&b"), 1);
        Assert.AreEqual(FormulaParser.FindMiddleSign("(a&b)"), 2);
        Assert.AreEqual(FormulaParser.FindMiddleSign("(a|b)"), 2);
        Assert.AreEqual(FormulaParser.FindMiddleSign("!a"), 0);
        Assert.AreEqual(FormulaParser.FindMiddleSign("(a&(b&c))"), 2);
        Assert.AreEqual(FormulaParser.FindMiddleSign("a->b"), 1);
        Assert.AreEqual(FormulaParser.FindMiddleSign("((a->b)&(a|b))"), 7);
        Assert.AreEqual(FormulaParser.FindMiddleSign("(((a->b)&(a|b)))"), 8);
    }

    [TestMethod]
    public void NormalizeBracketsTest()
    {
        Assert.AreEqual(FormulaParser.NormalizeBrackets("a&b"), "a&b");
        Assert.AreEqual(FormulaParser.NormalizeBrackets("(a&b"), "a&b");
        Assert.AreEqual(FormulaParser.NormalizeBrackets("a&b)"), "a&b");
        Assert.AreEqual(FormulaParser.NormalizeBrackets("((a&b)"), "(a&b)");
        Assert.AreEqual(FormulaParser.NormalizeBrackets("(a&b))"), "(a&b)");
        Assert.AreEqual(FormulaParser.NormalizeBrackets("a&b)"), "a&b");
    }

    [TestMethod]
    public void FindLeftSubFormulaTest()
    {
        Assert.AreEqual(FormulaParser.FindLeftSubFormula("((a->b)&(a|b))"), "(a->b)");
        Assert.AreEqual(FormulaParser.FindLeftSubFormula("a&b"), "a");
        Assert.AreEqual(FormulaParser.FindLeftSubFormula("!a"), "");
        Assert.AreEqual(FormulaParser.FindLeftSubFormula("((a->!b)&(a|b&c))"), "(a->!b)");
        Assert.AreEqual(FormulaParser.FindLeftSubFormula("a&b&c"), "a");
    }

    [TestMethod]
    public void FindRightSubFormulaTest()
    {
        Assert.AreEqual(FormulaParser.FindRightSubFormula("((a->b)&(a|b))"), "(a|b)");
        Assert.AreEqual(FormulaParser.FindRightSubFormula("a&b"), "b");
        Assert.AreEqual(FormulaParser.FindRightSubFormula("!a"), "a");
        Assert.AreEqual(FormulaParser.FindRightSubFormula("((a->!b)&(a|b&c))"), "(a|b&c)");
        Assert.AreEqual(FormulaParser.FindRightSubFormula("a&b&c"), "b&c");
    }
    
    [TestMethod]
    public void FindOperationWithLowestPriorityTest()
    {
        Assert.AreEqual(FormulaParser.FindMiddleSign("a&b|c~d"), 5);
        Assert.AreEqual(FormulaParser.FindMiddleSign("a&b|c"), 3);
        Assert.AreEqual(FormulaParser.FindMiddleSign("a|b|c|d"), 1);
        Assert.AreEqual(FormulaParser.FindMiddleSign("a&!a"), 1);
        Assert.AreEqual(FormulaParser.FindMiddleSign("!a&!b"), 2);
    }

    [TestMethod]
    public void ParseTest()
    {
        List<string> formulas = new List<string>();
        IEvaluatable logicalFormula = FormulaParser.Parse("(a&b)|(a|b&c)", formulas);
    }
}