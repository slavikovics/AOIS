using LogicalMinimization;

namespace LogicalMinimizationTests;

[TestClass]
public sealed class KarnaughSelectionTests
{
    [TestMethod]
    public void IsSelectionValidTest()
    {
        MapValue[,] table = new MapValue[3, 3];
        table[0, 0] = new MapValue(true);
        table[2, 0] = new MapValue(true);
        table[2, 2] = new MapValue(true);
        table[0, 2] = new MapValue(true);

        KarnaughSelection karnaughSelection1 = new KarnaughSelection(2, 2, 3, 3, 2, 2);
        KarnaughSelection karnaughSelection2 = new KarnaughSelection(2, 0, 3, 3, 1, 2);
        KarnaughSelection karnaughSelection3 = new KarnaughSelection(2, 2, 3, 3, 1, 2);
        Assert.AreEqual(karnaughSelection1.IsValid(ref table), true);
        Assert.AreEqual(karnaughSelection2.IsValid(ref table), true);
        Assert.AreEqual(karnaughSelection3.IsValid(ref table), true);
        
        table[0, 0].Value = false;
        Assert.AreEqual(karnaughSelection1.IsValid(ref table), false);
        Assert.AreEqual(karnaughSelection2.IsValid(ref table), false);
        Assert.AreEqual(karnaughSelection3.IsValid(ref table), true);

        table[0, 2].Value = false;
        Assert.AreEqual(karnaughSelection3.IsValid(ref table), true);
        
        table[2, 0].Value = false;
        Assert.AreEqual(karnaughSelection3.IsValid(ref table), false);
    }

    [TestMethod]
    public void RightTest()
    {
        MapValue[,] table = new MapValue[3, 3];
        table[0, 0] = new MapValue(true);
        table[0, 1] = new MapValue(false);
        table[0, 2] = new MapValue(true);
        table[1, 0] = new MapValue(false);
        table[1, 1] = new MapValue(false);
        table[1, 2] = new MapValue(false);
        table[2, 0] = new MapValue(true);
        table[2, 1] = new MapValue(false);
        table[2, 2] = new MapValue(true);

        KarnaughSelection karnaughSelection = new KarnaughSelection(0, 0, 3, 3);
        
        var right = karnaughSelection.Right();
        Assert.AreEqual(right.IsValid(ref table), false);
        Assert.AreEqual(right.Square, 2);

        var left = karnaughSelection.Left();
        Assert.AreEqual(left.IsValid(ref table), true);
        Assert.AreEqual(left.Square, 2);
        var leftUp = left.Up();
        Assert.AreEqual(leftUp.IsValid(ref table), true);
        Assert.AreEqual(leftUp.Square, 4);

        var up = karnaughSelection.Up();
        Assert.AreEqual(up.IsValid(ref table), true);
        Assert.AreEqual(up.Square, 2);
        var upLeft = up.Left(); 
        Assert.AreEqual(upLeft.IsValid(ref table), true);
        Assert.AreEqual(upLeft.Square, 4);

        var down = karnaughSelection.Down();
        Assert.AreEqual(down.IsValid(ref table), false);
        Assert.AreEqual(down.Square, 2);
    }
}