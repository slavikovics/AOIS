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
            karnaughMap2.RowArguments.Count, karnaughMap2.ColumnArguments.Count, 1, 1);
        Assert.AreEqual(selection2.Square, 4);
        
        var selection3 = KarnaughMap.FindSelection(karnaughMap2.Table, 
            karnaughMap2.RowArguments.Count, karnaughMap2.ColumnArguments.Count, 3, 0);
        Assert.AreEqual(selection3.Square, 2);
        
        var selection4 = KarnaughMap.FindSelection(karnaughMap2.Table, 
            karnaughMap2.RowArguments.Count, karnaughMap2.ColumnArguments.Count, 3, 0);
        Assert.AreEqual(selection4.Square, 2);
        
        var selection5 = KarnaughMap.FindSelection(karnaughMap2.Table, 
            karnaughMap2.RowArguments.Count, karnaughMap2.ColumnArguments.Count, 0, 2);
        Assert.AreEqual(selection5.Square, 4);
        
        var selection6 = KarnaughMap.FindSelection(karnaughMap2.Table, 
            karnaughMap2.RowArguments.Count, karnaughMap2.ColumnArguments.Count, 2, 1);
        Assert.AreEqual(selection6.Square, 4);
    }

    [TestMethod]
    public void FindAllSelectionsTest()
    {
        IEvaluatable formula = FormulaParser.Parse("(a&b)|((c->b)~d)");
        KarnaughMap karnaughMap = new KarnaughMap(formula);
        var selections = karnaughMap.FindAllSelections();
        Assert.AreEqual(selections.Count, 4);
    }

    [TestMethod]
    public void MinimizeTest1()
    {
        IEvaluatable formula = FormulaParser.Parse("(a&b)|((c->b)~d)");
        KarnaughMap karnaughMap = new KarnaughMap(formula);
        var result = karnaughMap.Minimize();
        var resp = result.ToString();
        Assert.AreEqual(resp, "(!b&c&!d)|(b&d)|(!c&d)|(a&b)");
    }
    
    [TestMethod]
    public void MinimizeTest2()
    {
        IEvaluatable formula = FormulaParser.Parse("a|(b|c)");
        KarnaughMap karnaughMap = new KarnaughMap(formula);
        var result = karnaughMap.Minimize();
        var resp = result.ToString();
        Assert.AreEqual(resp, "b|c|a");
    }
    
    [TestMethod]
    public void MinimizeTest3()
    {
        IEvaluatable formula = FormulaParser.Parse("((a->b)|c)");
        KarnaughMap karnaughMap = new KarnaughMap(formula);
        var result = karnaughMap.Minimize();
        var resp = result.ToString();
        Assert.AreEqual(resp, "b|c|(!a)");
    }
    
    [TestMethod]
    public void MinimizeTest4()
    {
        IEvaluatable formula = FormulaParser.Parse("((a->b)|(a~c))");
        KarnaughMap karnaughMap = new KarnaughMap(formula);
        var result = karnaughMap.Minimize();
        var resp = result.ToString();
        Assert.AreEqual(resp, "b|c|(!a)");
    }
}