using LogicalMinimization;

namespace LogicalMinimizationTests;

[TestClass]
public sealed class CarnoSelectionTests
{
    [TestMethod]
    public void IsSelectionValidTest()
    {
        bool[,] table = new bool[3, 3];
        table[0, 0] = true;
        table[2, 0] = true;
        table[2, 2] = true;
        table[0, 2] = true;

        CarnoSelection carnoSelection = new CarnoSelection(2, 2, 3, 3, 2, 2);
        Assert.AreEqual(carnoSelection.IsValid(table), true);
        
        table[0, 0] = false;
        Assert.AreEqual(carnoSelection.IsValid(table), false);
    }
}