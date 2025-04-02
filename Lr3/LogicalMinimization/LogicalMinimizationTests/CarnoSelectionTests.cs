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

        CarnoSelection carnoSelection1 = new CarnoSelection(2, 2, 3, 3, 2, 2);
        CarnoSelection carnoSelection2 = new CarnoSelection(2, 0, 3, 3, 1, 2);
        CarnoSelection carnoSelection3 = new CarnoSelection(2, 2, 3, 3, 1, 2);
        Assert.AreEqual(carnoSelection1.IsValid(table), true);
        Assert.AreEqual(carnoSelection2.IsValid(table), true);
        Assert.AreEqual(carnoSelection3.IsValid(table), true);
        
        table[0, 0] = false;
        Assert.AreEqual(carnoSelection1.IsValid(table), false);
        Assert.AreEqual(carnoSelection2.IsValid(table), false);
        Assert.AreEqual(carnoSelection3.IsValid(table), true);

        table[0, 2] = false;
        Assert.AreEqual(carnoSelection3.IsValid(table), true);
        
        table[2, 0] = false;
        Assert.AreEqual(carnoSelection3.IsValid(table), false);
    }
}