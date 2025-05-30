namespace Matrix;

public class Matrix
{
    public int Dimension { get; }
    
    private bool[,] Values { get; }

    public Matrix(int dimension)
    {
        Dimension = dimension;
        Values = new bool[Dimension, Dimension];
    }

    public Matrix()
    {
        Dimension = 16;
        Values = new bool[Dimension, Dimension];
    }

    public bool[] GetWord(int wordIndex)
    {
        if (wordIndex < 0 || wordIndex >= Dimension) throw new IndexOutOfRangeException();
        
        bool[] result = new bool[Dimension];
        for (int i = 0 + wordIndex; i < Dimension; i++)
        {
            result[i] = Values[i, wordIndex];
        }

        for (int i = 0; i < wordIndex; i++)
        {
            result[i] = Values[i, wordIndex];
        }

        return result;
    }

    public void SetWord(int wordIndex, bool[] values)
    {
        if (wordIndex < 0 || wordIndex >= Dimension) throw new IndexOutOfRangeException();
        if (values is null) throw new ArgumentNullException(nameof(values));
        if (values.Length != Dimension) throw new ArgumentException(nameof(values));
        
        bool[] result = new bool[Dimension];
        for (int i = 0 + wordIndex; i < Dimension; i++)
        {
            Values[i, wordIndex] = values[i - wordIndex];
        }

        for (int i = 0; i < wordIndex; i++)
        {
            Values[i, wordIndex] = values[i + Dimension - wordIndex];
        }
    }
}