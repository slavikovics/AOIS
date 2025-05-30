using NUnit.Framework.Legacy;

namespace MatrixTests;

using Matrix;
using NUnit.Framework;
using System;

[TestFixture]
public class Tests
{
    [Test]
    public void DefaultConstructor_Creates16x16Matrix()
    {
        var matrix = new Matrix();
        Assert.That(matrix.Dimension, Is.EqualTo(16));
    }

    [Test]
    public void Constructor_WithDimension_CreatesCorrectSizeMatrix()
    {
        int dimension = 5;
        var matrix = new Matrix(dimension);
        Assert.That(matrix.Dimension, Is.EqualTo(dimension));
    }

    [Test]
    public void GetWord_InvalidIndex_ThrowsIndexOutOfRangeException()
    {
        var matrix = new Matrix(10);
        Assert.Throws<IndexOutOfRangeException>(() => matrix.GetWord(-1));
        Assert.Throws<IndexOutOfRangeException>(() => matrix.GetWord(10));
    }

    [Test]
    public void SetWord_InvalidIndex_ThrowsIndexOutOfRangeException()
    {
        var matrix = new Matrix(8);
        bool[] values = new bool[8];
        Assert.Throws<IndexOutOfRangeException>(() => matrix.SetWord(-1, values));
        Assert.Throws<IndexOutOfRangeException>(() => matrix.SetWord(8, values));
    }

    [Test]
    public void SetWord_NullArray_ThrowsArgumentNullException()
    {
        var matrix = new Matrix(4);
        Assert.Throws<ArgumentNullException>(() => matrix.SetWord(1, null!));
    }

    [Test]
    public void SetWord_WrongArrayLength_ThrowsArgumentException()
    {
        var matrix = new Matrix(4);
        bool[] invalidValues = new bool[3];
        Assert.Throws<ArgumentException>(() => matrix.SetWord(1, invalidValues));
    }

    [Test]
    public void GetWord_RetrievesDefaultFalseValues()
    {
        var matrix = new Matrix(5);
        var word = matrix.GetWord(3);
        CollectionAssert.AreEqual(new bool[5], word);
    }

    [Test]
    public void SetAndGetWord_ReturnsCorrectlyShiftedValues()
    {
        int dimension = 4;
        var matrix = new Matrix(dimension);
        bool[] original = { true, false, true, false };

        matrix.SetWord(2, original);
        bool[] retrieved = matrix.GetWord(2);

        // Проверка циклического сдвига влево на (dimension - index) = 2
        bool[] expected = { true, false, true, false }; // Ожидается: [2,3,0,1]
        Assert.Multiple(() =>
        {
            Assert.That(retrieved[0], Is.EqualTo(original[2])); // [0] = original[2]
            Assert.That(retrieved[1], Is.EqualTo(original[3])); // [1] = original[3]
            Assert.That(retrieved[2], Is.EqualTo(original[0])); // [2] = original[0]
            Assert.That(retrieved[3], Is.EqualTo(original[1])); // [3] = original[1]
        });
    }

    [Test]
    public void SetWord_DoesNotAffectOtherColumns()
    {
        int dimension = 3;
        var matrix = new Matrix(dimension);
        bool[] wordForColumn1 = { true, false, true };

        matrix.SetWord(1, wordForColumn1);
        bool[] column0 = matrix.GetWord(0);
        bool[] column2 = matrix.GetWord(2);

        CollectionAssert.AreEqual(new bool[3], column0);
        CollectionAssert.AreEqual(new bool[3], column2);
    }

    [Test]
    public void SetAndGetWord_WithDifferentIndices_ConsistentBehavior()
    {
        int dimension = 5;
        var matrix = new Matrix(dimension);
        bool[] values = { true, false, true, false, true };

        for (int i = 0; i < dimension; i++)
        {
            matrix.SetWord(i, values);
            bool[] retrieved = matrix.GetWord(i);
            int shift = dimension - i;
            for (int j = 0; j < dimension; j++)
            {
                int expectedIndex = (j + shift) % dimension;
                Assert.That(retrieved[j], Is.EqualTo(values[expectedIndex]));
            }
        }
    }
}