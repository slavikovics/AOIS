using System;
using NUnit.Framework;
using Matrix;

namespace Matrix.Tests
{
    [TestFixture]
    public class WordsArithmeticsTests
    {
        [Test]
        public void ToBitString_NullValues_ThrowsArgumentNullException()
        {
            bool[] values = null;
            Assert.Throws<ArgumentNullException>(() => WordsArithmetics.ToBitString(values, 0, 1));
        }

        [Test]
        public void ToBitString_ValidValues_ReturnsCorrectString()
        {
            bool[] values = { true, false, true, true };
            string bitString = WordsArithmetics.ToBitString(values, 1, 2);
            Assert.That("01", Is.EqualTo(bitString));
        }

        [Test]
        public void ToBoolArray_NullString_ThrowsArgumentNullException()
        {
            string s = null;
            Assert.Throws<ArgumentNullException>(() => WordsArithmetics.ToBoolArray(s));
        }

        [Test]
        public void ToBoolArray_InvalidCharacter_ThrowsFormatException()
        {
            string s = "10A01";
            Assert.Throws<FormatException>(() => WordsArithmetics.ToBoolArray(s));
        }

        [Test]
        public void ToBoolArray_ValidString_ReturnsCorrectArray()
        {
            string s = "1010";
            bool[] expected = { true, false, true, false };
            bool[] result = WordsArithmetics.ToBoolArray(s);
            Assert.That(expected, Is.EqualTo(result));
        }

        [Test]
        public void Sum_NullWord_ThrowsArgumentNullException()
        {
            bool[] word = null;
            Assert.Throws<ArgumentNullException>(() => WordsArithmetics.Sum(word));
        }

        [Test]
        public void Sum_ValidWord_ReturnsModifiedArrayOfSameLength()
        {
            int vSize = 1, aSize = 2, bSize = 2, sSize = 3;
            int length = vSize + aSize + bSize + sSize;
            bool[] word = new bool[length];

            word[0] = true;
            word[1] = true; 
            word[2] = false;
            word[3] = false; 
            word[4] = true;
            for (int i = 5; i < length; i++) word[i] = false;

            bool[] result = WordsArithmetics.Sum(word, vSize, aSize, bSize, sSize);
            
            Assert.That(length, Is.EqualTo(result.Length));
            Assert.That(result[5], Is.False);
            Assert.That(result[6], Is.True);
            Assert.That(result[7], Is.True);
        }
    }
}
