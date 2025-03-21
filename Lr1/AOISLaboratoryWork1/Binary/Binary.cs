using System;
using System.Runtime.InteropServices.JavaScript;
using Microsoft.VisualBasic;

namespace AOISLaboratoryWork1;

public static class Binary
{
    public static string FromUnsignedInt(int input)
    {
        input = Math.Abs(input);
        string result = "";

        while (input >= 2)
        {
            result = input % 2 + result;
            input /= 2;
        }

        return input + result;
    }

    public static string Invert(string input)
    {
        string result = "";
        
        foreach (char digit in input)
        {
            if (digit == '1') result += '0';
            else result += '1';
        }
        
        return result;
    }

    public static string FindFractionalPart(double fractionalPart, int maxCount = 32)
    {
        if (fractionalPart >= 1) throw new ArgumentException("Argument must represent a fractional part.");
        string result = "";

        while (maxCount > 0)
        {
            fractionalPart *= 2;
            if (fractionalPart >= 1 || (fractionalPart >= 0.5 && maxCount == 1))
            {
                result += '1';
                fractionalPart -= 1;
            }
            else result += '0';

            maxCount--;
        }
        
        return result;
    }

    public static string FitInBytes(string input, bool isInverted = false)
    {
        int numberOfZeroes = 8 - input.Length % 8;
        if (numberOfZeroes == 8) return input;
        if (isInverted) return new string('1', numberOfZeroes) + input;
        return new string('0', numberOfZeroes) + input;
    }

    public static string FitInBytes(string input, int lengthInBits, bool isInverted = false)
    {
        if (isInverted) return new string(input[0], lengthInBits - input.Length) + input;
        return new string('0', lengthInBits - input.Length) + input;
    }

    public static string Sum(string firstArgument, string secondArgument, bool isInverted = false)
    {
        string result = "";
        if (firstArgument.Length != secondArgument.Length || isInverted)
        {
            int finalLength = Math.Max(firstArgument.Length, secondArgument.Length);
            if (isInverted) finalLength++;
            firstArgument = FitInBytes(firstArgument, finalLength, isInverted);
            secondArgument = FitInBytes(secondArgument, finalLength, isInverted);
        }

        int memorizedOne = 0;
        for (int i = firstArgument.Length - 1; i >= 0; i--)
        {
            int iterationResult = Convert.ToInt32(firstArgument.Substring(i, 1)) + Convert.ToInt32(secondArgument.Substring(i, 1)) + memorizedOne;
            switch (iterationResult)
            {
                case 0: memorizedOne = 0;
                    result = "0" + result;
                    break;
                case 1: memorizedOne = 0;
                    result = "1" + result;
                    break;
                case 2: memorizedOne = 1;
                    result = "0" + result;
                    break;
                case 3: memorizedOne = 1;
                    result = "1" + result;
                    break;
            }
        }
        if (memorizedOne == 1 && !isInverted) result = "1" + result;

        return result;
    }

    public static string Trim(string input)
    {
        if (input.IndexOf("1", StringComparison.Ordinal) == -1) return "0";
        return input.Substring(input.IndexOf("1", StringComparison.Ordinal));
    }

    public static string Sum(int firstArgument, int secondArgument)
    {
        return Sum(Binary.FromUnsignedInt(firstArgument), Binary.FromUnsignedInt(secondArgument));
    }

    public static string Sum(string firstArgument, int secondArgument)
    {
        return Sum(firstArgument, Binary.FromUnsignedInt(secondArgument));
    }

    public static int ConvertBinaryToInteger(string binary)
    {
        int length = binary.Length;
        int result = 0;
        
        for (int i = length - 1; i >= 0; i--)
        {
            result += (int)Math.Pow(2, i) * Convert.ToInt32(binary.Substring(length - i - 1, 1));
        }
        
        return result;
    }
}