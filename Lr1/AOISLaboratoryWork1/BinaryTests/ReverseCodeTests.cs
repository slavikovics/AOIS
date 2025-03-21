using AOISLaboratoryWork1;

namespace AOISLaboratoryWork1Tests;

[TestClass]
public class ReverseCodeTests
{
    [TestMethod]
    public void ConvertIntegerToReverseCodeTest()
    {
        Assert.AreEqual(ReverseCode.ConvertIntegerToReverseCode(128), "010000000");
        Assert.AreEqual(ReverseCode.ConvertIntegerToReverseCode(1735), "011011000111");
        Assert.AreEqual(ReverseCode.ConvertIntegerToReverseCode(534), "01000010110");
        Assert.AreEqual(ReverseCode.ConvertIntegerToReverseCode(98), "01100010");
        
        Assert.AreEqual(ReverseCode.ConvertIntegerToReverseCode(-128), "101111111");
        Assert.AreEqual(ReverseCode.ConvertIntegerToReverseCode(-1735), "100100111000");
        Assert.AreEqual(ReverseCode.ConvertIntegerToReverseCode(-534), "10111101001");
        Assert.AreEqual(ReverseCode.ConvertIntegerToReverseCode(-98), "10011101");
    }

    [TestMethod]
    public void ConvertReverseCodeToIntegerTest()
    {
        for (int i = -10000; i < 10000; i++)
        {
            Assert.AreEqual(ReverseCode.ConvertReverseCodeToInteger(ReverseCode.ConvertIntegerToReverseCode(i)), i);
        }
    }
}