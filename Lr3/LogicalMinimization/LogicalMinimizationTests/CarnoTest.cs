using LogicalMinimization;
using LogicalParser;

namespace LogicalMinimizationTests;

[TestClass]
public sealed class CarnoTest
{
    [TestMethod]
    public void SelectionTest()
    {
        IEvaluatable formula = FormulaParser.Parse("(p&!q&r)|(p&q&!r)|(p&q&r)");
        CarnoCard carnoCard = new CarnoCard(formula);
        var selection = CarnoCard.FindSelection(carnoCard.Table, 
            carnoCard.RowArguments.Count, carnoCard.ColumnArguments.Count, 1, 1);
        Assert.AreEqual(selection.Square, 2);
        
        
    }
}