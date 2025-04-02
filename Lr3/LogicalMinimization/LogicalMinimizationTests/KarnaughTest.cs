using LogicalMinimization;
using LogicalParser;

namespace LogicalMinimizationTests;

[TestClass]
public sealed class KarnaughTest
{
    [TestMethod]
    public void SelectionTest()
    {
        IEvaluatable formula1 = FormulaParser.Parse("(p&!q&r)|(p&q&!r)|(p&q&r)");
        KarnaughMap karnaughMap1 = new KarnaughMap(formula1);
        var selection1 = KarnaughMap.FindSelection(karnaughMap1.Table, 
            karnaughMap1.RowArguments.Count, karnaughMap1.ColumnArguments.Count, 1, 1);
        Assert.AreEqual(selection1.Square, 2);
        
        IEvaluatable formula2 = FormulaParser.Parse("(a&b)|((c->b)~d)");
        KarnaughMap karnaughMap2 = new KarnaughMap(formula2);
        var selection2 = KarnaughMap.FindSelection(karnaughMap2.Table, 
            karnaughMap2.RowArguments.Count, karnaughMap2.ColumnArguments.Count, 0, 1);
        Assert.AreEqual(selection2.Square, 4);
    }
}