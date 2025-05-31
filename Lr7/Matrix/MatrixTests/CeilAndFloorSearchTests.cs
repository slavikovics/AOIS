namespace MatrixTests;

using Matrix;
using NUnit.Framework;
using System;

[TestFixture]
public class NearestValueTests
{
    private Matrix matrix;
    private const int Dimension = 4;

    [SetUp]
    public void Setup()
    {
        matrix = new Matrix(Dimension);

        bool[] word0 = [false, false, false, false];
        bool[] word1 = [false, false, true, false];
        bool[] word2 = [false, true, false, false];
        bool[] word3 = [false, true, true, false];
        
        matrix.SetAddressColumn(0, word0);
        matrix.SetAddressColumn(1, word1);
        matrix.SetAddressColumn(2, word2);
        matrix.SetAddressColumn(3, word3);
    }

    [Test]
    public void FindCeil_ExactMatch_ReturnsSame()
    {
        bool[] argument = [false, false, true, false];
        
        var result = matrix.FindCeil(argument);

        Assert.Multiple(() =>
        {
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Value.word, Is.EqualTo(argument));
            Assert.That(result.Value.index, Is.EqualTo(1));
        });
    }

    [Test]
    public void FindCeil_NoExactMatch_ReturnsNextGreater()
    {
        bool[] argument = [false, false, false, true];
        bool[] expected = [false, false, true, false];
        
        var result = matrix.FindCeil(argument);
        
        Assert.Multiple(() =>
        {
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Value.word, Is.EqualTo(expected));
            Assert.That(result.Value.index, Is.EqualTo(1));
        });
    }

    [Test]
    public void FindCeil_ValueGreaterThanAll_ReturnsNull()
    {
        bool[] argument = [true, false, false, false];
        
        var result = matrix.FindCeil(argument);
        
        Assert.That(result, Is.Null);
    }

    [Test]
    public void FindCeil_WithDuplicateValues_ReturnsFirstMatch()
    {
        bool[] duplicate = [false, false, true, false];
        matrix.SetAddressColumn(3, duplicate);
        bool[] argument = [false, false, false, true];
        
        var result = matrix.FindCeil(argument);
        
        Assert.Multiple(() =>
        {
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Value.word, Is.EqualTo(duplicate));
            Assert.That(result.Value.index, Is.EqualTo(1));
        });
    }

    [Test]
    public void FindCeil_ShouldFindCorrectValue_WhenMultipleCandidates()
    {
        bool[] argument = [false, true, false, true];
        bool[] expected = [false, true, true, false];
        
        var result = matrix.FindCeil(argument);
        
        Assert.Multiple(() =>
        {
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Value.word, Is.EqualTo(expected));
            Assert.That(result.Value.index, Is.EqualTo(3));
        });
    }

    [Test]
    public void FindFloor_ExactMatch_ReturnsSame()
    {
        bool[] argument = [false, true, false, false];
        
        var result = matrix.FindFloor(argument);
        
        Assert.Multiple(() =>
        {
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Value.word, Is.EqualTo(argument));
            Assert.That(result.Value.index, Is.EqualTo(2));
        });
    }

    [Test]
    public void FindFloor_NoExactMatch_ReturnsPreviousLesser()
    {
        bool[] argument = [false, true, false, true];
        bool[] expected = [false, true, false, false];
        
        var result = matrix.FindFloor(argument);
        
        Assert.Multiple(() =>
        {
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Value.word, Is.EqualTo(expected));
            Assert.That(result.Value.index, Is.EqualTo(2));
        });
    }

    [Test]
    public void FindFloor_ValueBetweenMinAndMax_ReturnsCorrectFloor()
    {
        bool[] argument = [false, false, false, true];
        bool[] expected = [false, false, false, false];
        
        var result = matrix.FindFloor(argument);
        
        Assert.Multiple(() =>
        {
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Value.word, Is.EqualTo(expected));
            Assert.That(result.Value.index, Is.EqualTo(0));
        });
    }

    [Test]
    public void CompareFlags_EqualValues_ReturnsEqualFlags()
    {
        bool[] argument = [false, true, false, false];
        
        var result = matrix.CompareWithArgument(2, argument);
        
        Assert.Multiple(() =>
        {
            Assert.That(result.greater, Is.False);
            Assert.That(result.less, Is.False);
            Assert.That(result.equal, Is.True);
        });
    }

    [Test]
    public void CompareFlags_GreaterValue_ReturnsGreaterFlag()
    {
        bool[] argument = [false, true, false, false];
        
        var result = matrix.CompareWithArgument(3, argument);
        
        Assert.Multiple(() =>
        {
            Assert.That(result.greater, Is.True);
            Assert.That(result.less, Is.False);
            Assert.That(result.equal, Is.False);
        });
    }

    [Test]
    public void FindFloor_WithExactMinMatch_ReturnsMinValue()
    {
        bool[] argument = [false, false, false, false];
        
        var result = matrix.FindFloor(argument);
        
        Assert.Multiple(() =>
        {
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Value.word, Is.EqualTo(argument));
            Assert.That(result.Value.index, Is.EqualTo(0));
        });
    }

    [Test]
    public void CompareWithArgument_InvalidIndex_ThrowsException()
    {
        bool[] argument = [false, false, false, false];
        
        Assert.That(
            () => matrix.CompareWithArgument(-1, argument),
            Throws.TypeOf<IndexOutOfRangeException>());
        
        Assert.That(
            () => matrix.CompareWithArgument(Dimension, argument),
            Throws.TypeOf<IndexOutOfRangeException>());
    }
}