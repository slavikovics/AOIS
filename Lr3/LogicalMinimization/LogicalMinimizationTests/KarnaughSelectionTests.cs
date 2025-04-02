using LogicalMinimization;

namespace LogicalMinimizationTests;

[TestClass]
public sealed class KarnaughSelectionTests
{
    [TestMethod]
    public void IsSelectionValidTest()
    {
        bool[,] table = new bool[3, 3];
        table[0, 0] = true;
        table[2, 0] = true;
        table[2, 2] = true;
        table[0, 2] = true;

        KarnaughSelection karnaughSelection1 = new KarnaughSelection(2, 2, 3, 3, 2, 2);
        KarnaughSelection karnaughSelection2 = new KarnaughSelection(2, 0, 3, 3, 1, 2);
        KarnaughSelection karnaughSelection3 = new KarnaughSelection(2, 2, 3, 3, 1, 2);
        Assert.AreEqual(karnaughSelection1.IsValid(table), true);
        Assert.AreEqual(karnaughSelection2.IsValid(table), true);
        Assert.AreEqual(karnaughSelection3.IsValid(table), true);
        
        table[0, 0] = false;
        Assert.AreEqual(karnaughSelection1.IsValid(table), false);
        Assert.AreEqual(karnaughSelection2.IsValid(table), false);
        Assert.AreEqual(karnaughSelection3.IsValid(table), true);

        table[0, 2] = false;
        Assert.AreEqual(karnaughSelection3.IsValid(table), true);
        
        table[2, 0] = false;
        Assert.AreEqual(karnaughSelection3.IsValid(table), false);
    }
}