using LogicalMinimization;

namespace LogicalMinimizationTests;

[TestClass]
public sealed class KarnaughSelectionTests
{
    [TestMethod]
    public void IsSelectionValidTest()
    {
        bool[,] table = new bool[3, 3];
        table[0, 0] = true; // 101 
        table[2, 0] = true; // 000
        table[2, 2] = true; // 101
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

    [TestMethod]
    public void RightTest()
    {
        bool[,] table = new bool[3, 3];
        table[0, 0] = true; // 101 
        table[2, 0] = true; // 000
        table[2, 2] = true; // 101
        table[0, 2] = true;

        KarnaughSelection karnaughSelection = new KarnaughSelection(0, 0, 3, 3);
        
        var right = karnaughSelection.Right();
        Assert.AreEqual(right.IsValid(table), false);
        Assert.AreEqual(right.Square, 2);

        var left = karnaughSelection.Left();
        Assert.AreEqual(left.IsValid(table), true);
        Assert.AreEqual(left.Square, 2);
        var leftUp = left.Up();
        Assert.AreEqual(leftUp.IsValid(table), true);
        Assert.AreEqual(leftUp.Square, 4);

        var up = karnaughSelection.Up();
        Assert.AreEqual(up.IsValid(table), true);
        Assert.AreEqual(up.Square, 2);
        var upLeft = up.Left();
        Assert.AreEqual(upLeft.IsValid(table), true);
        Assert.AreEqual(upLeft.Square, 4);

        var down = karnaughSelection.Down();
        Assert.AreEqual(down.IsValid(table), false);
        Assert.AreEqual(down.Square, 2);
    }
}