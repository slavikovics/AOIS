using AOISLaboratoryWork1;

namespace AOISLaboratoryWork1Tests;

[TestClass]
public sealed class BinaryTests
{
    [TestMethod]
    public void BinaryFromUnsignedIntTest()
    {
        Assert.AreEqual("111", Binary.FromUnsignedInt(7));
        Assert.AreEqual("10011100", Binary.FromUnsignedInt(156));
        Assert.AreEqual("10000000000", Binary.FromUnsignedInt(1024));
        Assert.AreEqual("111011110", Binary.FromUnsignedInt(478));
        Assert.AreEqual("111000", Binary.FromUnsignedInt(56));
    }

    [TestMethod]
    public void BinaryInvertTest()
    {
        Assert.AreEqual("111", Binary.Invert("000"));
        Assert.AreEqual("101", Binary.Invert("010"));
        Assert.AreEqual("10001", Binary.Invert("01110"));
        Assert.AreEqual("10001", Binary.Invert("01110"));
        Assert.AreEqual("10101", Binary.Invert("01010"));
    }

    [TestMethod]
    public void FitInBytesTest()
    {
        Assert.AreEqual("00000111", Binary.FitInBytes("111"));
        Assert.AreEqual("00001010", Binary.FitInBytes("1010"));
        Assert.AreEqual("00010000", Binary.FitInBytes("10000"));
        Assert.AreEqual("10010000", Binary.FitInBytes("10010000"));
        
        Assert.AreEqual("00001010", Binary.FitInBytes("1010", 8));
        Assert.AreEqual("00000010", Binary.FitInBytes("10", 8));
    }

    [TestMethod]
    public void SumTests()
    {
        string result = Binary.Sum(Binary.FromUnsignedInt(7), Binary.FromUnsignedInt(156));
        Assert.AreEqual("10100011", result);
        
        result = Binary.Sum(Binary.FromUnsignedInt(1024), Binary.FromUnsignedInt(478));
        Assert.AreEqual(result, Binary.FromUnsignedInt(1502));
        
        result = Binary.Sum(Binary.FromUnsignedInt(117), Binary.FromUnsignedInt(15));
        Assert.AreEqual(result, Binary.FromUnsignedInt(132));

        result = Binary.Sum(1, 2);
        Assert.AreEqual(result, Binary.FromUnsignedInt(3));

        result = Binary.Sum(0, 0);
        Assert.AreEqual(result, Binary.FromUnsignedInt(0));

        result = Binary.Sum(72357, 567);
        Assert.AreEqual(result, Binary.FromUnsignedInt(72924));

        result = Binary.Sum(0, 10);
        Assert.AreEqual(result, Binary.FromUnsignedInt(10));

        for (int i = 0; i < 100; i++)
        {
            for (int j = 0; j < 100; j++)
            {
                result = Binary.Sum(i, j);
                Assert.AreEqual(result, Binary.FromUnsignedInt(i + j));
            }
        }
    }

    [TestMethod]
    public void ConvertToIntegerTest()
    {
        Assert.AreEqual(Binary.ConvertBinaryToInteger("111"), 7);
        Assert.AreEqual(Binary.ConvertBinaryToInteger("10011100"), 156);
        Assert.AreEqual(Binary.ConvertBinaryToInteger("10000000000"), 1024);
        Assert.AreEqual(Binary.ConvertBinaryToInteger("111011110"), 478);
        Assert.AreEqual(Binary.ConvertBinaryToInteger("111000"), 56);
        Assert.AreEqual(Binary.ConvertBinaryToInteger("0111000"), 56);
        Assert.AreEqual(Binary.ConvertBinaryToInteger("000111000"), 56);
    }

    [TestMethod]
    public void FindFractionalPartTest()
    {
        Assert.AreEqual(Binary.FindFractionalPart(0.625, 3), "101");
        Assert.AreEqual(Binary.FindFractionalPart(0.111, 8), "00011100");
        Assert.AreEqual(Binary.FindFractionalPart(0.0, 9), "000000000");
    }
}