using HashTable;

namespace HashTableTests;

public class Tests
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void Test1()
    {
        HashTable<string, string> table = new(500);
        for (int i = 0; i < 500; i++)
        {
            table.Add(i.ToString(), i.ToString());
        }

        for (int i = 0; i < 500; i++)
        {
            Assert.That(table.Find(i.ToString()), Is.EqualTo(i.ToString()));
        }

        for (int i = 0; i < 250; i++)
        {
            table.Remove(i.ToString());
        }

        for (int i = 0; i < 250; i++)
        {
            try
            {
                table.Find(i.ToString());
            }
            catch (KeyNotFoundException e)
            {
                Assert.That(e.Message, Is.EqualTo("Could not find item in hash table"));
            }
        }

        for (int i = 250; i < 500; i++)
        {
            Assert.That(table.Find(i.ToString()), Is.EqualTo(i.ToString()));
        }
    }
}