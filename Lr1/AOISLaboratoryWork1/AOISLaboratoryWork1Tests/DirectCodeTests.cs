using AOISLaboratoryWork1;

namespace AOISLaboratoryWork1Tests;

[TestClass]
public sealed class DirectCodeTests
{
    [TestMethod]
    public void ConvertIntegerToDirectCodeTest()
    {
        Assert.AreEqual(DirectCode.ConvertIntegerToDirectCode(45), "0101101");
        Assert.AreEqual(DirectCode.ConvertIntegerToDirectCode(127), "01111111");
        Assert.AreEqual(DirectCode.ConvertIntegerToDirectCode(1024), "010000000000");
        Assert.AreEqual(DirectCode.ConvertIntegerToDirectCode(950), "01110110110");
    }

    [TestMethod]
    public void ConvertDirectCodeToIntegerTest()
    {
        for (int i = -10000; i <= 10000; i++)
        {
            Assert.AreEqual(DirectCode.ConvertDirectCodeToInteger(DirectCode.ConvertIntegerToDirectCode(i)), i);
        }
    }

    [TestMethod]
    public void MultiplicationTest()
    {
        Assert.AreEqual(DirectCode.Multiplication("10001010", "10001010"), "01100100");
        
        for (int i = -100; i <= 100; i++)
        {
            for (int j = -100; j <= 100; j++)
            {
                Assert.AreEqual(DirectCode.ConvertDirectCodeToInteger(DirectCode.Multiplication(i, j)), i * j);
            }
        }
    }

    [TestMethod]
    public void FindDigitsNumberTest()
    {
        Assert.AreEqual(DirectCode.FindDigitsNumber(100), 3);
        Assert.AreEqual(DirectCode.FindDigitsNumber(1), 1);
        Assert.AreEqual(DirectCode.FindDigitsNumber(567), 3);
        Assert.AreEqual(DirectCode.FindDigitsNumber(4444), 4);
    }

    [TestMethod]
    public void FindSmallestForDivisionTest()
    {
        Assert.AreEqual(DirectCode.FindSmallestForDivision("1010", "10"), "10");
        Assert.AreEqual(DirectCode.FindSmallestForDivision("1010", "101"), "101");
    }
    
    [TestMethod]
    public void DivisionTest()
    {
        string result = DirectCode.Divide(Binary.FromUnsignedInt(8), Binary.FromUnsignedInt(2));
        Assert.AreEqual(Math.Round(DirectCode.ConvertDivisionResultToDouble(result), 5), (double)4.00000);
        
        result = DirectCode.Divide(Binary.FromUnsignedInt(56), Binary.FromUnsignedInt(24));
        Assert.AreEqual(Math.Round(DirectCode.ConvertDivisionResultToDouble(result), 5), (double)2.33333);

        result = DirectCode.Divide(Binary.FromUnsignedInt(196), Binary.FromUnsignedInt(3));
        Assert.AreEqual(Math.Round(DirectCode.ConvertDivisionResultToDouble(result), 5), (double)65.33333);
        
        result = DirectCode.Divide(Binary.FromUnsignedInt(10), Binary.FromUnsignedInt(3));
        Assert.AreEqual(Math.Round(DirectCode.ConvertDivisionResultToDouble(result), 5), (double)3.33333);
        
        result = DirectCode.Divide(Binary.FromUnsignedInt(6), Binary.FromUnsignedInt(4));
        Assert.AreEqual(Math.Round(DirectCode.ConvertDivisionResultToDouble(result), 5), (double)1.50000);
        
        result = DirectCode.Divide(Binary.FromUnsignedInt(15), Binary.FromUnsignedInt(2));
        Assert.AreEqual(Math.Round(DirectCode.ConvertDivisionResultToDouble(result), 5), (double)7.50000);
        
        result = DirectCode.Divide(Binary.FromUnsignedInt(1), Binary.FromUnsignedInt(3));
        Assert.AreEqual(Math.Round(DirectCode.ConvertDivisionResultToDouble(result), 5), (double)0.33333);
        
        result = DirectCode.Divide(Binary.FromUnsignedInt(1), Binary.FromUnsignedInt(125));
        Assert.AreEqual(Math.Round(DirectCode.ConvertDivisionResultToDouble(result), 5), (double)0.00800);

        result = DirectCode.Divide(10, -3);
        Assert.AreEqual(Math.Round(DirectCode.ConvertDivisionResultToDouble(result, true), 5), (double)-3.33333);

        try
        {
            result = DirectCode.Divide(10, 0);
            Assert.Fail();
        }
        catch (DivideByZeroException)
        {
        }
    }
}