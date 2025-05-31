namespace Matrix;

public class Matrix
{
    public int Dimension { get; }
    
    private bool[,] Values { get; set; }

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
        
        for (int i = 0 + wordIndex; i < Dimension; i++)
        {
            Values[i, wordIndex] = values[i - wordIndex];
        }

        for (int i = 0; i < wordIndex; i++)
        {
            Values[i, wordIndex] = values[i + Dimension - wordIndex];
        }
    }
    
    public bool[] GetAddressColumn(int columnIndex)
    {
        if (columnIndex < 0 || columnIndex >= Dimension) throw new IndexOutOfRangeException();
        bool[] result = new bool[Dimension];
    
        for (int i = 0; i < Dimension; i++)
        {
            int row = (i + columnIndex) % Dimension;
            int col = i % Dimension;
            result[i] = Values[row, col];
        }
    
        return result;
    }

    public void SetAddressColumn(int columnIndex, bool[] values)
    {
        if (columnIndex < 0 || columnIndex >= Dimension) throw new IndexOutOfRangeException();
        if (values == null) throw new ArgumentNullException(nameof(values));
        if (values.Length != Dimension) throw new ArgumentException("Invalid values length");
    
        for (int i = 0; i < Dimension; i++)
        {
            int row = (i + columnIndex) % Dimension;
            int col = i % Dimension;
            Values[row, col] = values[i];
        }
    }

    public bool[] Conjunction(int firstArgumentIndex, int secondArgumentIndex)
    {
        bool[] result = new bool[Dimension];
        var arg1 = GetAddressColumn(firstArgumentIndex);
        var arg2 = GetAddressColumn(secondArgumentIndex);

        for (int i = 0; i < Dimension; i++)
        {
            result[i] = arg1[i] && arg2[i];
        }
        
        return result;
    }
    
    public bool[] ShefferNegation(int firstArgumentIndex, int secondArgumentIndex)
    {
        bool[] result = new bool[Dimension];
        var arg1 = GetAddressColumn(firstArgumentIndex);
        var arg2 = GetAddressColumn(secondArgumentIndex);

        for (int i = 0; i < Dimension; i++)
        {
            result[i] = !(arg1[i] && arg2[i]);
        }
        
        return result;
    }
    
    public bool[] FirstArgumentRepeat(int firstArgumentIndex, int secondArgumentIndex)
    {
        bool[] result = new bool[Dimension];
        var arg1 = GetAddressColumn(firstArgumentIndex);

        for (int i = 0; i < Dimension; i++)
        {
            result[i] = arg1[i];
        }
        
        return result;
    }
    
    public bool[] FirstArgumentNegation(int firstArgumentIndex, int secondArgumentIndex)
    {
        bool[] result = new bool[Dimension];
        var arg1 = GetAddressColumn(firstArgumentIndex);

        for (int i = 0; i < Dimension; i++)
        {
            result[i] = !arg1[i];
        }
        
        return result;
    }
    
    public (bool greater, bool less, bool equal) CompareWithArgument(int columnIndex, bool[] argument)
    {
        if (columnIndex < 0 || columnIndex >= Dimension) 
            throw new IndexOutOfRangeException();
        if (argument == null || argument.Length != Dimension)
            throw new ArgumentException("Invalid argument");
        
        var addressColumn = GetAddressColumn(columnIndex);

        bool g = false;
        bool l = false;
    
        for (int i = 0; i < Dimension; i++)
        {
            bool si = addressColumn[i];
            bool ai = argument[i];
        
            g = g || (!ai && si && !l);
            l = l || (ai && !si && !g);
        }
    
        return (g, l, !g && !l);
    }

    private int CompareWords(bool[] word1, bool[] word2)
    {
        for (int i = 0; i < word1.Length; i++)
        {
            if (word1[i] && !word2[i]) return 1;
            if (!word1[i] && word2[i]) return -1;
        }
        return 0;
    }
    
    public (bool[] word, int index)? FindCeil(bool[] argument)
    {
        List<(bool[] word, int index)> candidates = [];
    
        for (int j = 0; j < Dimension; j++)
        {
            var (greater, less, equal) = CompareWithArgument(j, argument);
            if (greater || equal)
            {
                candidates.Add((GetAddressColumn(j), j));
            }
        }
    
        if (candidates.Count == 0) return null;
    
        (bool[] word, int index) best = candidates[0];
        foreach (var candidate in candidates)
        {
            if (CompareWords(candidate.word, best.word) < 0)
            {
                best = candidate;
            }
        }
        return best;
    }
    
    public (bool[] word, int index)? FindFloor(bool[] argument)
    {
        List<(bool[] word, int index)> candidates = [];
    
        for (int j = 0; j < Dimension; j++)
        {
            var (greater, less, equal) = CompareWithArgument(j, argument);
            if (less || equal)
            {
                candidates.Add((GetAddressColumn(j), j));
            }
        }
    
        if (candidates.Count == 0) return null;
    
        (bool[] word, int index) best = candidates[0];
        foreach (var candidate in candidates)
        {
            if (CompareWords(candidate.word, best.word) > 0)
            {
                best = candidate;
            }
        }
        return best;
    }
    
    public List<(int index, bool[] summedWord)> SumWordsWithPrefix(bool[] prefix, int vSize = 3, int aSize = 4, int bSize = 4, int sSize = 5)
    {
        if (prefix == null) throw new ArgumentNullException(nameof(prefix));
        if (prefix.Length > Dimension) throw new ArgumentException(nameof(prefix));

        var results = new List<(int, bool[])>();
        
        for (int j = 0; j < Dimension; j++)
        {
            bool[] word = GetWord(j);
            
            bool matches = true;
            for (int i = 0; i < prefix.Length; i++)
            {
                if (word[i] != prefix[i])
                {
                    matches = false;
                    break;
                }
            }

            if (!matches) continue;
            
            bool[] summed = WordsArithmetics.Sum(word, vSize, aSize, bSize, sSize);

            SetWord(j, summed);
            results.Add((j, GetWord(j)));
        }

        return results;
    }
}