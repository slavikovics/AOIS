using System.Reflection.Emit;
using System.Text;
using AOISLaboratoryWork1;

namespace Matrix;

public static class WordsArithmetics
{
    public static bool[] Sum(bool[] word, int vSize = 3, int aSize = 4, int bSize = 4, int sSize = 5)
    {
        var first = ToBitString(word, vSize, aSize);
        var second = ToBitString(word, vSize + aSize, bSize);
        var sum = Binary.Sum(first, second);
        
        sum = Binary.FitInBytes(sum, sSize);
        var sumResult = ToBoolArray(sum);
        int index = 0;

        for (int i = vSize + aSize + bSize; i < word.Length; i++)
        {
            word[i] = sumResult[index];
            index++;
        }

        return word;
    }

    public static string ToBitString(bool[] values, int start, int count)
    {
        if (values == null)
            throw new ArgumentNullException(nameof(values));

        var sb = new StringBuilder(values.Length);
        for (int  i = start; i < start + count; i++)
        {
            sb.Append(values[i] ? '1' : '0');
        }

        return sb.ToString();
    }
    
    public static bool[] ToBoolArray(string bitString)
    {
        if (bitString == null)
            throw new ArgumentNullException(nameof(bitString));
        
        var result = new bool[bitString.Length];
        for (int i = 0; i < bitString.Length; i++)
        {
            char c = bitString[i];
            switch (c)
            {
                case '0':
                    result[i] = false;
                    break;
                case '1':
                    result[i] = true;
                    break;
                default:
                    throw new FormatException();
            }
        }
        return result;
    }
}