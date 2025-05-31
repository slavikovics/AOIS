using NUnit.Framework.Legacy;

namespace MatrixTests;

using Matrix;
using NUnit.Framework;

[TestFixture]
public class LogicalOperationsTests
{
    private Matrix _matrix;
    private const int Dimension = 4;
    private bool[] _column0;
    private bool[] _column1;
    private bool[] _column2;
    private bool[] _column3;

    [SetUp]
    public void Setup()
    {
        _matrix = new Matrix(Dimension);
        
        _column0 = [ true, false, true, false ];
        _column1 = [ false, true, true, false ];
        _column2 = [ true, true, false, false ];
        _column3 = [ false, false, false, false ];
        
        _matrix.SetAddressColumn(0, _column0);
        _matrix.SetAddressColumn(1, _column1);
        _matrix.SetAddressColumn(2, _column2);
        _matrix.SetAddressColumn(3, _column3);
    }

    [Test]
    public void Conjunction_ValidIndices_ReturnsCorrectResult()
    {
        bool[] result = _matrix.Conjunction(0, 1);
        
        bool[] expected =
        [
            _column0[0] && _column1[0], 
            _column0[1] && _column1[1], 
            _column0[2] && _column1[2], 
            _column0[3] && _column1[3]
        ];
        CollectionAssert.AreEqual(expected, result);
    }

    [Test]
    public void Conjunction_InvalidIndices_ThrowsException()
    {
        Assert.Throws<IndexOutOfRangeException>(() => _matrix.Conjunction(-1, 0));
        Assert.Throws<IndexOutOfRangeException>(() => _matrix.Conjunction(0, Dimension));
    }

    [Test]
    public void ShefferNegation_ValidIndices_ReturnsCorrectResult()
    {
        bool[] result = _matrix.ShefferNegation(0, 2);
        
        bool[] expected =
        [
            !(_column0[0] && _column2[0]), 
            !(_column0[1] && _column2[1]), 
            !(_column0[2] && _column2[2]), 
            !(_column0[3] && _column2[3])
        ];
        CollectionAssert.AreEqual(expected, result);
    }

    [Test]
    public void FirstArgumentRepeat_ValidIndices_ReturnsCorrectResult()
    {
        bool[] result = _matrix.FirstArgumentRepeat(1, 2);
        
        CollectionAssert.AreEqual(_matrix.GetAddressColumn(1), result);
    }

    [Test]
    public void FirstArgumentNegation_ValidIndices_ReturnsCorrectResult()
    {
        bool[] result = _matrix.FirstArgumentNegation(2, 0);
        
        bool[] expected =
        [
            !_column2[0], 
            !_column2[1], 
            !_column2[2], 
            !_column2[3]
        ];
        CollectionAssert.AreEqual(expected, result);
    }

    [Test]
    public void Operations_WithSameArguments_ConsistentBehavior()
    {
        bool[] allTrue = [true, true, true, true];
        bool[] allFalse = [false, false, false, false];
        _matrix.SetAddressColumn(3, allTrue);
        
        CollectionAssert.AreEqual(allTrue, _matrix.Conjunction(3, 3));
        CollectionAssert.AreEqual(allFalse, _matrix.ShefferNegation(3, 3));
        CollectionAssert.AreEqual(allTrue, _matrix.FirstArgumentRepeat(3, 0));
        CollectionAssert.AreEqual(allFalse, _matrix.FirstArgumentNegation(3, 0));
    }

    [Test]
    public void Operations_EdgeCases_HandledCorrectly()
    {
        bool[] mixed1 = [true, false, true, false];
        bool[] mixed2 = [false, true, false, true];
        _matrix.SetAddressColumn(3, mixed1);
        _matrix.SetAddressColumn(2, mixed2);
        
        bool[] expectedConjunction = [false, false, false, false];
        CollectionAssert.AreEqual(expectedConjunction, _matrix.Conjunction(3, 2));
        
        bool[] expectedSheffer = [true, true, true, true];
        CollectionAssert.AreEqual(expectedSheffer, _matrix.ShefferNegation(3, 2));
        
        CollectionAssert.AreEqual(mixed1, _matrix.FirstArgumentRepeat(3, 2));
        
        bool[] expectedNegation = [false, true, false, true];
        CollectionAssert.AreEqual(expectedNegation, _matrix.FirstArgumentNegation(3, 2));
    }
}