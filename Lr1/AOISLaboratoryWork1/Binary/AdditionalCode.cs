namespace AOISLaboratoryWork1;

public static class AdditionalCode
{
    public static string ConvertIntegerToAdditionalCode(int input)
    {
        if (input >= 0) return DirectCode.ConvertIntegerToDirectCode(input);
        return Binary.Sum(ReverseCode.ConvertIntegerToReverseCode(input), 1);
    }

    public static int ConvertAdditionalCodeToInteger(string input)
    {
        if (input[0] == '0') return DirectCode.ConvertDirectCodeToInteger(input);
        return DirectCode.ConvertDirectCodeToInteger(Binary.Sum("1" + Binary.Invert(input.Substring(1)), DirectCode.ConvertIntegerToDirectCode(1)));
    }

    public static string ConvertDirectCodeToAdditionalCode(string directCode)
    { 
        return Binary.Sum(ReverseCode.ConvertDirectCodeToReverseCode(directCode), 1);
    }

    public static string Sum(string firstArgument, string secondArgument)
    {
        return Binary.Sum(firstArgument, secondArgument, true);
    }

    public static string Sum(int firstArgument, int secondArgument)
    {
        return Sum(AdditionalCode.ConvertIntegerToAdditionalCode(firstArgument), AdditionalCode.ConvertIntegerToAdditionalCode(secondArgument));
    }
}