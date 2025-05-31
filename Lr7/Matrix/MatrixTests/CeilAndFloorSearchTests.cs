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
        
        // Инициализация тестовых данных
        bool[] word0 = [false, false, false, false]; // 0
        bool[] word1 = [false, false, true, false];  // 2
        bool[] word2 = [false, true, false, false];  // 4
        bool[] word3 = [false, true, true, false];   // 6
        
        matrix.SetAddressColumn(0, word0);
        matrix.SetAddressColumn(1, word1);
        matrix.SetAddressColumn(2, word2);
        matrix.SetAddressColumn(3, word3);
    }

        [Test]
    public void FindCeil_ExactMatch_ReturnsSame()
    {
        // Arrange
        bool[] argument = [false, false, true, false]; // 2
        
        // Act
        var result = matrix.FindCeil(argument);
        
        // Assert
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
        // Arrange
        bool[] argument = [false, false, false, true]; // 1
        bool[] expected = [false, false, true, false]; // 2
        
        // Act
        var result = matrix.FindCeil(argument);
        
        // Assert
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
        // Arrange
        bool[] argument = [true, false, false, false]; // 8 (больше всех)
        
        // Act
        var result = matrix.FindCeil(argument);
        
        // Assert
        Assert.That(result, Is.Null);
    }

    [Test]
    public void FindCeil_WithDuplicateValues_ReturnsFirstMatch()
    {
        // Arrange - добавим дубликат значения 2
        bool[] duplicate = [false, false, true, false];
        matrix.SetAddressColumn(3, duplicate);
        bool[] argument = [false, false, false, true]; // 1
        
        // Act
        var result = matrix.FindCeil(argument);
        
        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Value.word, Is.EqualTo(duplicate));
            Assert.That(result.Value.index, Is.EqualTo(1)); // Первое вхождение
        });
    }

    [Test]
    public void FindCeil_ShouldFindCorrectValue_WhenMultipleCandidates()
    {
        // Arrange
        bool[] argument = [false, true, false, true]; // 5
        bool[] expected = [false, true, true, false]; // 6
        
        // Act
        var result = matrix.FindCeil(argument);
        
        // Assert
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
        // Arrange
        bool[] argument = [false, true, false, false];
        
        // Act
        var result = matrix.FindFloor(argument);
        
        // Assert
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
        // Arrange
        bool[] argument = [false, true, false, true];
        bool[] expected = [false, true, false, false];
        
        // Act
        var result = matrix.FindFloor(argument);
        
        // Assert
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
        // Arrange
        bool[] argument = [false, false, false, true];
        bool[] expected = [false, false, false, false];
        
        // Act
        var result = matrix.FindFloor(argument);
        
        // Assert
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
        // Arrange
        bool[] argument = [false, true, false, false];
        
        // Act
        var result = matrix.CompareWithArgument(2, argument);
        
        // Assert
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
        // Arrange
        bool[] argument = [false, true, false, false];

        // Act
        var result = matrix.CompareWithArgument(3, argument);

        // Assert
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
        // Arrange
        bool[] argument = [false, false, false, false];
        
        // Act
        var result = matrix.FindFloor(argument);
        
        // Assert
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
        // Arrange
        bool[] argument = [false, false, false, false];
        
        // Act & Assert
        Assert.That(
            () => matrix.CompareWithArgument(-1, argument),
            Throws.TypeOf<IndexOutOfRangeException>());
        
        Assert.That(
            () => matrix.CompareWithArgument(Dimension, argument),
            Throws.TypeOf<IndexOutOfRangeException>());
    }
}