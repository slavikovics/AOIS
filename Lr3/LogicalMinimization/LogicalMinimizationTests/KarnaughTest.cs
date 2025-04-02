using LogicalMinimization;
using LogicalParser;

namespace LogicalMinimizationTests;

[TestClass]
public sealed class KarnaughTest
{
    [TestMethod]
    public void SelectionTest()
    {
        
        IEvaluatable formula2 = FormulaParser.Parse("(a&b)|((c->b)~d)");
        KarnaughMap karnaughMap2 = new KarnaughMap(formula2);
        var selection2 = KarnaughMap.FindSelection(karnaughMap2.Table, 
            karnaughMap2.RowArguments.Count, karnaughMap2.ColumnArguments.Count, 1, 1);
        Assert.AreEqual(selection2.Square, 4);
    }
}