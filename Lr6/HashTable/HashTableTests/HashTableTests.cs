using HashTable;

namespace HashTableTests;

[TestFixture]
public class HashTableTests
{
    [Test]
    public void Add_And_Find_SingleItem_ShouldWork()
    {
        var table = new HashTable<int, string>(10);
        table.Add(1, "One");
        var value = table.Find(1);
        
        Assert.That(value, Is.EqualTo("One"));
        Assert.That(table.Count, Is.EqualTo(1));
    }
    
    [Test]
    public void Add_And_Remove_And_Find_MultipleItems_ShouldWork()
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
            Assert.Throws<KeyNotFoundException>(() => table.Find(i.ToString()));
        }

        for (int i = 250; i < 500; i++)
        {
            Assert.That(table.Find(i.ToString()), Is.EqualTo(i.ToString()));
        }
    }

    [Test]
    public void Update_ExistingItem_ShouldChangeValue()
    {
        var table = new HashTable<int, string>(10);
        table.Add(1, "One");
        table.Update(1, "Updated");
        
        Assert.That(table.Find(1), Is.EqualTo("Updated"));
    }

    [Test]
    public void Remove_ExistingItem_ShouldMarkAsDeleted()
    {
        var table = new HashTable<int, string>(10);
        table.Add(1, "One");
        table.Remove(1);
        
        Assert.Throws<KeyNotFoundException>(() => table.Find(1));
        Assert.That(table.Count, Is.EqualTo(0));
    }

    [Test]
    public void Add_WithHashCollision_ShouldStoreBothItems()
    {
        var table = new HashTable<CollidingKey, string>(10);
        var key1 = new CollidingKey { Id = 1 };
        var key2 = new CollidingKey { Id = 2 };
        
        table.Add(key1, "First");
        table.Add(key2, "Second");
        
        Assert.Multiple(() =>
        {
            Assert.That(table.Find(key1), Is.EqualTo("First"));
            Assert.That(table.Find(key2), Is.EqualTo("Second"));
            Assert.That(table.Count, Is.EqualTo(2));
        });
    }

    [Test]
    public void Add_WhenTableIsFull_ShouldThrowException()
    {
        var table = new HashTable<int, string>(2);
        table.Add(1, "One");
        table.Add(2, "Two");
        
        Assert.Throws<Exception>(() => table.Add(3, "Three"));
    }

    [Test]
    public void Add_AfterRemoval_ShouldReuseDeletedSlot()
    {
        var table = new HashTable<int, string>(3);
        table.Add(1, "One");
        table.Add(2, "Two");
        table.Remove(1);
        table.Add(3, "Three");
        
        Assert.That(table.Find(3), Is.EqualTo("Three"));
        Assert.That(table.Count, Is.EqualTo(2));
    }

    [Test]
    public void Find_NonExistentKey_ShouldThrow()
    {
        var table = new HashTable<int, string>(10);
        Assert.Throws<KeyNotFoundException>(() => table.Find(999));
    }

    [Test]
    public void Add_DuplicateKey_ShouldThrow()
    {
        var table = new HashTable<int, string>(10);
        table.Add(1, "One");
        Assert.Throws<Exception>(() => table.Add(1, "Duplicate"));
    }

    [Test]
    public void OccupationRate_AfterAdditions_ShouldBeCorrect()
    {
        var table = new HashTable<int, string>(4);
        table.Add(1, "A");
        table.Add(2, "B");
        
        Assert.That(table.OccupationRate(), Is.EqualTo(0.5).Within(0.001));
    }

    [Test]
    public void Add_NullKey_ShouldThrow()
    {
        var table = new HashTable<string, string>(10);
        Assert.Throws<ArgumentNullException>(() => table.Add(null, "NullKey"));
    }

    [Test]
    public void Remove_ShouldNotBreakProbingSequence()
    {
        var table = new HashTable<int, string>(5);
        table.Add(1, "A");
        table.Add(6, "B");
        table.Remove(1);
        
        Assert.Multiple(() =>
        {
            Assert.That(table.Find(6), Is.EqualTo("B"));
            Assert.Throws<KeyNotFoundException>(() => table.Find(1));
        });
    }

    [Test]
    public void Initialize_WithZeroCapacity_ShouldThrow()
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => new HashTable<int, string>(0));
    }

    [Test]
    public void ToString_ShouldReturnValidRepresentation()
    {
        var table = new HashTable<int, string>(2);
        table.Add(1, "One");
        var result = table.ToString();
        
        Assert.Multiple(() =>
        {
            Assert.That(result, Does.Contain("1 -> One"));
            Assert.That(result, Does.Contain("Count: 1"));
        });
    }
}

public class CollidingKey
{
    public int Id { get; set; }
    public override int GetHashCode() => 1;
    public override bool Equals(object obj) => obj is CollidingKey other && Id == other.Id;
}