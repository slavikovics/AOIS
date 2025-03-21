using AOISLaboratoryWork1;

namespace AOISLaboratoryWork1Tests;

[TestClass]
public class FloatingPointnumberTests
{
    [TestMethod]
    public void FloatingPointNumberConvertTest()
    {
        FloatingPointNumber floatingPointNumber = new(85.125);
        Assert.AreEqual(floatingPointNumber.ToString(), "01000010101010100100000000000000");
        Assert.AreEqual(85.125, Math.Round(floatingPointNumber.ConvertToDecimal(), 3));
        
        floatingPointNumber = new(11.724);
        Assert.AreEqual(floatingPointNumber.ToString(), "01000001001110111001010110000001");
        Assert.AreEqual(11.724, Math.Round(floatingPointNumber.ConvertToDecimal(), 3));
        
        floatingPointNumber = new(99.64);
        Assert.AreEqual(floatingPointNumber.ToString(), "01000010110001110100011110101110");
        Assert.AreEqual(99.64, Math.Round(floatingPointNumber.ConvertToDecimal(), 2));
        
        floatingPointNumber = new(435675);
        Assert.AreEqual(floatingPointNumber.ToString(), "01001000110101001011101101100000");
        Assert.AreEqual(435675, Math.Round(floatingPointNumber.ConvertToDecimal(), 0));
        
        floatingPointNumber = new(0.0005);
        Assert.AreEqual(floatingPointNumber.ToString(), "00111010000000110001001001101111");
        Assert.AreEqual(0.0005, Math.Round(floatingPointNumber.ConvertToDecimal(), 4));
        
        floatingPointNumber = new(-85.125);
        Assert.AreEqual(floatingPointNumber.ToString(), "11000010101010100100000000000000");
        Assert.AreEqual(-85.125, Math.Round(floatingPointNumber.ConvertToDecimal(), 3));
        
        floatingPointNumber = new(0);
        Assert.AreEqual(floatingPointNumber.ToString(), "00000000000000000000000000000000");
        Assert.AreEqual(0, Math.Round(floatingPointNumber.ConvertToDecimal(), 0));
    }

    [TestMethod]
    public void PositiveSumTest()
    {
        FloatingPointNumber floatingPointNumber1 = new FloatingPointNumber(3);
        
        FloatingPointNumber floatingPointNumber2 = new FloatingPointNumber(4);
        
        FloatingPointNumber floatingPointNumber3 = new(0.356);
        
        FloatingPointNumber floatingPointNumber4 = new(8.5);
        
        FloatingPointNumber floatingPointNumber5 = new(9.3);
        
        FloatingPointNumber floatingPointNumber6 = new(128);

        FloatingPointNumber testResult = FloatingPointNumber.PositiveSum(floatingPointNumber1, floatingPointNumber2);
        double result = FloatingPointNumber.PositiveSum(floatingPointNumber1, floatingPointNumber2).ConvertToDecimal();
        Assert.AreEqual(7, Math.Round(result));
        
        result = FloatingPointNumber.PositiveSum(floatingPointNumber1, floatingPointNumber3).ConvertToDecimal();
        Assert.AreEqual(3.356, Math.Round(result, 3));
        
        result = FloatingPointNumber.PositiveSum(floatingPointNumber6, floatingPointNumber5).ConvertToDecimal();
        Assert.AreEqual(137.3, Math.Round(result,  1));
        
        result = FloatingPointNumber.PositiveSum(floatingPointNumber4, floatingPointNumber5).ConvertToDecimal();
        Assert.AreEqual(17.8, Math.Round(result,  1));
    }
}