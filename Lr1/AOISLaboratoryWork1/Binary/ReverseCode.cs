namespace AOISLaboratoryWork1;

public static class ReverseCode
{
    public static string ConvertIntegerToReverseCode(int input)
    {
        return ConvertDirectCodeToReverseCode(DirectCode.ConvertIntegerToDirectCode(input));
    }

    public static string ConvertDirectCodeToReverseCode(string input)
    {
        if (input[0] == '0') return input;
        return input[0] + Binary.Invert(input.Substring(1));
    }

    public static int ConvertReverseCodeToInteger(string input)
    {
        if (input[0] == '0') return DirectCode.ConvertDirectCodeToInteger(input);
        return DirectCode.ConvertDirectCodeToInteger(input[0] + Binary.Invert(input.Substring(1)));
    }
}