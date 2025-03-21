using LogicalParser;

namespace LogicalParserTests;

[TestClass]
public class TableTests
{
    [TestMethod]
    public void FormsTest1()
    {
        string input = "a|b";
        Table table = new Table(input);
        Assert.AreEqual(table.DisjunctiveForm, "(!a&b)|(a&!b)|(a&b)");
        Assert.AreEqual(table.ConjunctiveForm, "(a|b)");
    }
    
    [TestMethod]
    public void FormsTest2()
    {
        string input = "(a&b)|c";
        Table table = new Table(input);
        Assert.AreEqual(table.DisjunctiveForm, "(!a&!b&c)|(!a&b&c)|(a&!b&c)|(a&b&!c)|(a&b&c)");
        Assert.AreEqual(table.ConjunctiveForm, "(a|b|c)&(a|!b|c)&(!a|b|c)");
    }

    [TestMethod]
    public void IndexForm1()
    {
        string input = "a&b&c&d";
        Table table = new Table(input);
        Assert.AreEqual(table.IndexForm, "0000000000000001");
    }
    
    [TestMethod]
    public void IndexForm2()
    {
        string input = "((a&b)|c)->d";
        Table table = new Table(input);
        Assert.AreEqual(table.IndexForm, "1101110111010101");
    }
    
    [TestMethod]
    public void IndexForm3()
    {
        string input = "((a&b)|c)->!(d&(e~f))";
        Table table = new Table(input);
        Assert.AreEqual(table.IndexForm, "1111111111110110111111111111011011111111111101101111011011110110");
    }
    
    [TestMethod]
    public void IndexForm4()
    {
        string input = "!a";
        Table table = new Table(input);
        Assert.AreEqual(table.IndexForm, "10");
    }
    
    [TestMethod]
    public void IndexForm5()
    {
        string input = "a";
        Table table = new Table(input);
        Assert.AreEqual(table.IndexForm, "01");
    }
    
    [TestMethod]
    public void IndexForm6()
    {
        string input = "a|b|c";
        Table table = new Table(input);
        Assert.AreEqual(table.IndexForm, "01111111");
    }
}