namespace AOISLaboratoryWork1;

public class FloatingPointNumber
{
    public string Sign { get; private set; }
    
    public string Exponent { get; private set; }
    
    public string Mantissa { get; private set; }
    
    private const int MantissaLength = 23;

    private const int Bias = 127;

    public FloatingPointNumber(double value)
    {
        Sign = value < 0 ? "1" : "0";
        FindMantissa(value);
    }

    public FloatingPointNumber(string sign, string exponent, string mantissa)
    {
        Sign = sign;
        Exponent = exponent;
        Mantissa = mantissa;
    }

    private void FindMantissa(double value)
    {
        value = Math.Abs(value);
        string result = FindLeftPart(value);
        int startDotIndex = result.Length;
        result += FindRightPart(value, 255);
        int newDotIndex = result.IndexOf("1", StringComparison.Ordinal) + 1;
        int power = startDotIndex - newDotIndex;
        result = result.Substring(0, startDotIndex);
        result += FindRightPart(value, 23 - power);
        result = result.Substring(result.IndexOf("1", StringComparison.Ordinal) + 1, 23);
        Mantissa = result;
        Exponent = Binary.FitInBytes(Binary.FromUnsignedInt(power + Bias), 8);
        if (value == 0) Exponent = Binary.FitInBytes(Binary.FromUnsignedInt(0), 8);
    }

    public double ConvertToDecimal()
    {
        int multiplier = 1;
        if (Sign == "1") multiplier = -1;
        int power = Binary.ConvertBinaryToInteger(Exponent) - Bias;
        
        double result = Math.Pow(2, power) * DirectCode.ConvertDivisionResultToDouble("1." + Mantissa);

        return result * multiplier;
    }

    private static string FindLeftPart(double value)
    {
        int unsignedInteger = (int)Math.Floor(value);
        return Binary.Trim(Binary.FitInBytes(Binary.FromUnsignedInt(unsignedInteger)));
    }

    private static string FindRightPart(double value, int length)
    {
        int unsignedInteger = (int)Math.Floor(value);
        return Binary.FindFractionalPart(value - unsignedInteger, length);
    }

    public static FloatingPointNumber PositiveSum(FloatingPointNumber firstNumber, FloatingPointNumber secondNumber)
    {
        int firstExponent = Binary.ConvertBinaryToInteger(firstNumber.Exponent);
        int secondExponent = Binary.ConvertBinaryToInteger(secondNumber.Exponent);
        string firstMantissa = "1" + firstNumber.Mantissa;
        string secondMantissa = "1" + secondNumber.Mantissa;

        if (firstExponent < secondExponent)
        {
            firstMantissa = new string('0', secondExponent - firstExponent) + firstMantissa.Substring(0, MantissaLength + 1 - (secondExponent - firstExponent));
        }

        if (secondExponent < firstExponent)
        {
            secondMantissa = new string('0', firstExponent - secondExponent) + secondMantissa.Substring(0, MantissaLength + 1 - (firstExponent - secondExponent));
        }
        
        string resultMantissa = Binary.Sum(firstMantissa, secondMantissa);
        resultMantissa = resultMantissa.Substring(1);

        int resultExponent = Math.Max(firstExponent, secondExponent);
        if (resultMantissa.Length > MantissaLength)
        {
            resultExponent++;
            resultMantissa = resultMantissa.Substring(0, MantissaLength);
        }
        
        return new FloatingPointNumber("0", Binary.FitInBytes(Binary.FromUnsignedInt(resultExponent), 8), resultMantissa);
    }

    public override string ToString()
    {
        return Sign + Exponent + Mantissa;
    }
}