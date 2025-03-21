using LogicalParser;

namespace LogicalParserTests;

[TestClass]
public class OptionsTests
{
    [TestMethod]
    public void ArgumentsTests()
    {
        List<string> arguments = ["a", "b", "c"];
        List<string> options = OptionsBuilder.BuildArguments(arguments);
        Assert.AreEqual(options.Count, 8);
        
        arguments = ["a", "b"];
        options = OptionsBuilder.BuildArguments(arguments);
        Assert.AreEqual(options.Count, 4);
        
        arguments = ["a"];
        options = OptionsBuilder.BuildArguments(arguments);
        Assert.AreEqual(options.Count, 2);
    }

    [TestMethod]
    public void BuildOptionsTests()
    {
        List<string> arguments = ["a", "b"];
        List<string> options = OptionsBuilder.BuildArguments(arguments);
        List<Dictionary<string, bool>> results = OptionsBuilder.BuildOptions(options, arguments);

        foreach (var dict in results)
        {
            foreach (var argument in arguments)
            {
                if (!dict.ContainsKey(argument)) Assert.Fail();
            }
        }
    }
}