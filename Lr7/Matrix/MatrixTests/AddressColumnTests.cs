using NUnit.Framework.Legacy;

namespace MatrixTests;

using Matrix;
using NUnit.Framework;
using System;

[TestFixture]
public class AddressColumnTests
{
    [Test]
    public void GetAddressColumn_InvalidIndex_ThrowsIndexOutOfRangeException()
    {
        var matrix = new Matrix(5);
        Assert.Throws<IndexOutOfRangeException>(() => matrix.GetAddressColumn(-1));
        Assert.Throws<IndexOutOfRangeException>(() => matrix.GetAddressColumn(5));
    }

    [Test]
    public void SetAddressColumn_InvalidIndex_ThrowsIndexOutOfRangeException()
    {
        var matrix = new Matrix(3);
        bool[] values = new bool[3];
        Assert.Throws<IndexOutOfRangeException>(() => matrix.SetAddressColumn(-1, values));
        Assert.Throws<IndexOutOfRangeException>(() => matrix.SetAddressColumn(3, values));
    }

    [Test]
    public void SetAddressColumn_NullArray_ThrowsArgumentNullException()
    {
        var matrix = new Matrix(4);
        Assert.Throws<ArgumentNullException>(() => matrix.SetAddressColumn(1, null!));
    }

    [Test]
    public void SetAddressColumn_WrongArrayLength_ThrowsArgumentException()
    {
        var matrix = new Matrix(4);
        bool[] invalidValues = new bool[3];
        Assert.Throws<ArgumentException>(() => matrix.SetAddressColumn(1, invalidValues));
    }

    [Test]
    public void GetAddressColumn_RetrievesDefaultFalseValues()
    {
        var matrix = new Matrix(4);
        bool[] column = matrix.GetAddressColumn(2);
        CollectionAssert.AreEqual(new bool[4], column);
    }

    [Test]
    public void SetAndGetAddressColumn_ReturnsCorrectlyShiftedValues()
    {
        int dimension = 4;
        var matrix = new Matrix(dimension);
        bool[] original = { true, false, true, false };

        matrix.SetAddressColumn(1, original);
        bool[] retrieved = matrix.GetAddressColumn(1);
        CollectionAssert.AreEqual(original, retrieved);
    }

    [Test]
    public void SetAddressColumn_DoesNotAffectOtherColumns()
    {
        int dimension = 3;
        var matrix = new Matrix(dimension);
        bool[] column1Values = { true, false, true };

        matrix.SetAddressColumn(1, column1Values);
        
        bool[] column0 = matrix.GetAddressColumn(0);
        bool[] column2 = matrix.GetAddressColumn(2);

        CollectionAssert.AreEqual(new bool[3], column0);
        CollectionAssert.AreEqual(new bool[3], column2);
    }

    [Test]
    public void SetAndGetAddressColumn_ConsistentForAllIndices()
    {
        int dimension = 5;
        var matrix = new Matrix(dimension);
        bool[] values = { true, false, true, false, true };

        for (int i = 0; i < dimension; i++)
        {
            matrix.SetAddressColumn(i, values);
            bool[] retrieved = matrix.GetAddressColumn(i);

            CollectionAssert.AreEqual(values, retrieved);
        }
    }

    [Test]
    public void AddressColumn_EdgeCaseForLastColumn()
    {
        int dimension = 4;
        var matrix = new Matrix(dimension);
        bool[] values = { true, true, false, false };

        matrix.SetAddressColumn(dimension - 1, values);
        bool[] retrieved = matrix.GetAddressColumn(dimension - 1);
        
        CollectionAssert.AreEqual(values, retrieved);
    }
}