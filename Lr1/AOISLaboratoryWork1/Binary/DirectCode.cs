using System.Numerics;
using System.Runtime.InteropServices.JavaScript;
using System.Text;

namespace AOISLaboratoryWork1;

public static class DirectCode
{
    public static string ConvertIntegerToDirectCode(int input)
    {
        char sign = '0';
        if (input < 0) sign = '1';
        
        return sign + Binary.FromUnsignedInt(input);
    }

    public static int ConvertDirectCodeToInteger(string input)
    {
        if (input[0] == '0') return Binary.ConvertBinaryToInteger(input.Substring(1));
        return Binary.ConvertBinaryToInteger(input.Substring(1)) * -1;
    }

    public static string Trim(string input)
    {
        int position = input.IndexOf("1", StringComparison.Ordinal);
        if (position < 0) return input;
        return input.Substring(position);
    }

    public static string FindOutSign(char firstSign, char secondSign)
    {
        if (firstSign == secondSign) return "0";
        return "1";
    }

    public static string Multiplication(string firstArgument, string secondArgument)
    {
        string result = DirectCode.ConvertIntegerToDirectCode(0);
        string offset = "";
        string sign = FindOutSign(firstArgument[0], secondArgument[0]);
        firstArgument = firstArgument.Substring(1);
        secondArgument = secondArgument.Substring(1);

        for (int i = secondArgument.Length - 1; i >= 0; i--)
        {
            char digit = secondArgument[i];
            
            if (digit == '0')
            {
                result = Binary.Sum(new string('0', firstArgument.Length) + offset, result);
                offset += "0";
                continue;
            }
            result = Binary.Sum(firstArgument + offset, result);
            offset += "0";
        }
        
        return sign + Trim(result);
    }

    public static string Multiplication(int firstArgument, int secondArgument)
    {
        return Multiplication(ConvertIntegerToDirectCode(firstArgument), ConvertIntegerToDirectCode(secondArgument));
    }

    public static int FindDigitsNumber(long number)
    {
        int result = 0;
        while (number >= 1)
        {
            number /= 10;
            result++;
        }
        
        return result;
    }
    
    public static double ConvertDivisionResultToDouble(string input, bool isSigned = false)
    {
        int multiplier = 1;
        if (isSigned && input[0] == '1')
        {
            multiplier = -1;
            input = input.Substring(1);
        }
        
        double result = 0;
        int index = input.IndexOf(".", StringComparison.Ordinal);
        input = input.Replace(".", "");
        int currentPosition = 0;
        
        for (int i = index - 1; i >= index - input.Length; i--)
        {
            result += Math.Pow(2, i) * Convert.ToInt32(input[currentPosition].ToString());
            currentPosition++;
        }
        //result = Math.Round(result, 5);

        return result * multiplier;
    }

    private static string MakeNegative(string input)
    {
        return "1" + input;
    }

    private static string MakePositive(string input)
    {
        return "0" + input;
    }

    private static bool IsNegative(string input)
    {
        if (input[0] == '1') return true;
        return false;
    }

    public static string FindSmallestForDivision(string firstArgument, string secondArgument)
    {
        string result = "";
        
        foreach (char digit in firstArgument)
        {
            result += digit;
            if (Binary.ConvertBinaryToInteger(result) >= Binary.ConvertBinaryToInteger(secondArgument)) return result;
        }

        return "";
    }

    public static string Divide(int firstArgument, int secondArgument)
    {
        string result;
        if (secondArgument == 0) throw new DivideByZeroException();
        if (firstArgument < 0 && secondArgument > 0 || firstArgument > 0 && secondArgument < 0) result = "1";
        else result = "0";
        return result + Divide(Binary.FromUnsignedInt(Math.Abs(firstArgument)), Binary.FromUnsignedInt(Math.Abs(secondArgument))); 
    }

    public static string Divide(string firstArgument, string secondArgument)
    {
        string firstSelection = FindSmallestForDivision(firstArgument, secondArgument);
        string remaining = Binary.Trim(AdditionalCode.Sum(MakePositive(firstSelection), AdditionalCode.ConvertDirectCodeToAdditionalCode(MakeNegative(secondArgument))).Substring(1));
        string numberEnding = firstArgument.Substring(firstSelection.Length);
        string result = "";
        if (firstSelection != "") result = "1";
        else
        {
            result = "0";
            remaining = firstArgument;
            numberEnding = "";
        }

        bool wasAccuracyAchieved = false;
        bool hasDot = false;
        
        while (!wasAccuracyAchieved)
        {
            result += DivisionBody(ref remaining, ref numberEnding, secondArgument, ref hasDot);
            if (hasDot && result.Substring(result.IndexOf(".", StringComparison.Ordinal) + 1).Length >= 20) wasAccuracyAchieved = true;
        }
        
        return result;
    }

    private static string DivisionBody(ref string remaining, ref string numberEnding, string secondArgument, ref bool hasDot)
    {
        string result = "";
        string number = remaining;
        
        if (numberEnding.Length == 0)
        {
            number += "0";
            if (!hasDot)
            {
                result += ".";
                hasDot = true;
            }
        }
        else
        {
            number += numberEnding[0];
            numberEnding = numberEnding.Substring(1);
        }

        string partialResult = AdditionalCode.Sum(MakePositive(number), AdditionalCode.ConvertDirectCodeToAdditionalCode(MakeNegative(secondArgument)));
        if (IsNegative(partialResult))
        {
            remaining = number;
            return result + "0";
        }

        remaining = partialResult;
        result += "1";
        
        return result;
    }
}