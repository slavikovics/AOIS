using AOISLaboratoryWork1;

namespace AOISLaboratoryWork1Console;

static class Program
{
    private delegate void Delegate();
    
    private static readonly Dictionary<string, Delegate> Commands = new Dictionary<string, Delegate>();
    
    static Program()
    {
        Commands.Add("1", ConvertToDifferentCodes);
        Commands.Add("2", SumInAdditionalCode);
        Commands.Add("3", SubtractInAdditionalCode);
        Commands.Add("4", MultiplyInDirectCode);
        Commands.Add("5", DivideInDirectCode);
        Commands.Add("6", SumFloatingPointNumbers);
    }
    
    static void Main(string[] args)
    {
        while (true)
        {
            Console.WriteLine("1. Convert integer to different codes");
            Console.WriteLine("2. Sum in additional code");
            Console.WriteLine("3. Subtract in additional code");
            Console.WriteLine("4. Multiply in direct code");
            Console.WriteLine("5. Divide in direct code");
            Console.WriteLine("6. Sum in IEEE-754");
            Console.WriteLine("7. Exit program");
            
            string? response = Console.ReadLine();
            if (response != null && Commands.ContainsKey(response))
            {
                try
                {
                    Commands[response]();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
            else if (response == "7") return;
        }
    }

    static void ConvertToDifferentCodes()
    {
        Console.WriteLine();
        Console.WriteLine("Please, enter a number");
        int number = Convert.ToInt32(Console.ReadLine());
        
        string numberInDirectCode = DirectCode.ConvertIntegerToDirectCode(number);
        int convertedNumber = DirectCode.ConvertDirectCodeToInteger(numberInDirectCode);
        string numberInReversedCode = ReverseCode.ConvertIntegerToReverseCode(number);
        string numberInAdditionalCode = AdditionalCode.ConvertIntegerToAdditionalCode(number);
        
        Console.WriteLine($"Number in Direct Code: {numberInDirectCode}");
        Console.WriteLine($"Number in Reversed Code: {numberInReversedCode}");
        Console.WriteLine($"Number in Additional Code: {numberInAdditionalCode}");
        Console.WriteLine($"Converted back: {convertedNumber}");
        Console.WriteLine();
    }

    private static void SumInAdditionalCode()
    {
        Console.WriteLine();
        Console.WriteLine("Please, enter first number");
        int firstNumber = Convert.ToInt32(Console.ReadLine());
        
        Console.WriteLine("Please, enter second number");
        int secondNumber = Convert.ToInt32(Console.ReadLine());
        
        string firstNumberInAdditionalCode = AdditionalCode.ConvertIntegerToAdditionalCode(firstNumber);
        string secondNumberInAdditionalCode = AdditionalCode.ConvertIntegerToAdditionalCode(secondNumber);
        string result = AdditionalCode.Sum(firstNumber, secondNumber);
        int resultInInteger = AdditionalCode.ConvertAdditionalCodeToInteger(result);
        
        Console.WriteLine($"First in additional code: {firstNumberInAdditionalCode}");
        Console.WriteLine($"Second in additional code: {secondNumberInAdditionalCode}");
        Console.WriteLine($"Result: {result}");
        Console.WriteLine($"Result: {resultInInteger}");
        Console.WriteLine();
    }
    
    private static void SubtractInAdditionalCode()
    {
        Console.WriteLine();
        Console.WriteLine("Please, enter first number");
        int firstNumber = Convert.ToInt32(Console.ReadLine());
        
        Console.WriteLine("Please, enter second number");
        int secondNumber = Convert.ToInt32(Console.ReadLine());
        
        string firstNumberInAdditionalCode = AdditionalCode.ConvertIntegerToAdditionalCode(firstNumber);
        string secondNumberInAdditionalCode = AdditionalCode.ConvertIntegerToAdditionalCode(secondNumber);
        string result = AdditionalCode.Sum(firstNumber, -1 * secondNumber);
        int resultInInteger = AdditionalCode.ConvertAdditionalCodeToInteger(result);
        
        Console.WriteLine($"First in additional code: {firstNumberInAdditionalCode}");
        Console.WriteLine($"Second in additional code: {secondNumberInAdditionalCode}");
        Console.WriteLine($"Result: {result}");
        Console.WriteLine($"Result: {resultInInteger}");
        Console.WriteLine();
    }
    
    private static void MultiplyInDirectCode()
    {
        Console.WriteLine();
        Console.WriteLine("Please, enter first number");
        int firstNumber = Convert.ToInt32(Console.ReadLine());
        
        Console.WriteLine("Please, enter second number");
        int secondNumber = Convert.ToInt32(Console.ReadLine());
        
        string firstNumberInDirectCode = DirectCode.ConvertIntegerToDirectCode(firstNumber);
        string secondNumberInDirectCode = DirectCode.ConvertIntegerToDirectCode(secondNumber);
        string result = DirectCode.Multiplication(firstNumber, secondNumber);
        int resultInInteger = DirectCode.ConvertDirectCodeToInteger(result);
        
        Console.WriteLine($"First in direct code: {firstNumberInDirectCode}");
        Console.WriteLine($"Second in direct code: {secondNumberInDirectCode}");
        Console.WriteLine($"Result: {result}");
        Console.WriteLine($"Result: {resultInInteger}");
        Console.WriteLine();
    }
    
    private static void DivideInDirectCode()
    {
        Console.WriteLine();
        Console.WriteLine("Please, enter first number");
        int firstNumber = Convert.ToInt32(Console.ReadLine());
        
        Console.WriteLine("Please, enter second number");
        int secondNumber = Convert.ToInt32(Console.ReadLine());
        
        string firstNumberInDirectCode = DirectCode.ConvertIntegerToDirectCode(firstNumber);
        string secondNumberInDirectCode = DirectCode.ConvertIntegerToDirectCode(secondNumber);
        string result = DirectCode.Divide(firstNumber, secondNumber);
        double resultInDouble = DirectCode.ConvertDivisionResultToDouble(result);
        
        Console.WriteLine($"First in direct code: {firstNumberInDirectCode}");
        Console.WriteLine($"Second in direct code: {secondNumberInDirectCode}");
        Console.WriteLine($"Result: {result}");
        Console.WriteLine($"Result: {resultInDouble}");
        Console.WriteLine();
    }
    
    private static void SumFloatingPointNumbers()
    {
        Console.WriteLine();
        Console.WriteLine("Please, enter first number");
        double firstNumber = Convert.ToDouble(Console.ReadLine());
        
        Console.WriteLine("Please, enter second number");
        double secondNumber = Convert.ToDouble(Console.ReadLine());

        FloatingPointNumber fp1 = new FloatingPointNumber(firstNumber);
        FloatingPointNumber fp2 = new FloatingPointNumber(secondNumber);
        FloatingPointNumber fp3 = FloatingPointNumber.PositiveSum(fp1, fp2);
        
        double resultInDouble = fp3.ConvertToDecimal();
        
        Console.WriteLine($"First in IEEE-754: {fp1}");
        Console.WriteLine($"Second in IEEE-754: {fp2}");
        Console.WriteLine($"Result: {fp3}");
        Console.WriteLine($"Result: {resultInDouble}");
        Console.WriteLine();
    }
}