using AOISLaboratoryWork1;

namespace AOISLaboratoryWork1Tests;

[TestClass]
public sealed class AdditionalCodeTests
{
    [TestMethod]
    public void ConvertIntegerToAdditionalCodeTest()
    {
        Assert.AreEqual(AdditionalCode.ConvertIntegerToAdditionalCode(123), "01111011");
        Assert.AreEqual(AdditionalCode.ConvertIntegerToAdditionalCode(-123), "10000101");
        Assert.AreEqual(AdditionalCode.ConvertIntegerToAdditionalCode(4563), "01000111010011");
        Assert.AreEqual(AdditionalCode.ConvertIntegerToAdditionalCode(-4563), "10111000101101");
    }

    [TestMethod]
    public void ConvertAdditionalCodeToIntegerTest()
    {
        for (int i = -10000; i < 10000; i++)
        {
            Assert.AreEqual(AdditionalCode.ConvertAdditionalCodeToInteger(AdditionalCode.ConvertIntegerToAdditionalCode(i)), i);
        }
    }

    [TestMethod]
    public void AdditionalCodeSumTest()
    {
        Assert.AreEqual(AdditionalCode.ConvertAdditionalCodeToInteger(AdditionalCode.Sum(4, 4)), 8);
        
        for (int i = -100; i < 100; i++)
        {
            for (int j = -100; j < 100; j++)
            {
                string firstArgument = AdditionalCode.ConvertIntegerToAdditionalCode(i);
                string secondArgument = AdditionalCode.ConvertIntegerToAdditionalCode(j);
                string result = AdditionalCode.Sum(firstArgument, secondArgument);
                
                Assert.AreEqual(AdditionalCode.ConvertAdditionalCodeToInteger(result), i + j);
            }
        }
    }

    [TestMethod]
    public void AdditionalCodeOtherTests()
    {
        AdditionalCode.Sum("10", AdditionalCode.ConvertDirectCodeToAdditionalCode("110"));
    }
}