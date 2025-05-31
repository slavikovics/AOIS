using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using Matrix;
using NUnit.Framework.Legacy;

namespace Matrix.Tests
{
    [TestFixture]
    public class MatrixSumWordsWithPrefixTests
    {
        [Test]
        public void SumWordsWithPrefix_NullPrefix_ThrowsArgumentNullException()
        {
            var matrix = new Matrix(4);
            Assert.Throws<ArgumentNullException>(() => matrix.SumWordsWithPrefix(null));
        }

        [Test]
        public void SumWordsWithPrefix_PrefixTooLong_ThrowsArgumentException()
        {
            var matrix = new Matrix(4);
            bool[] prefix = new bool[5];
            Assert.Throws<ArgumentException>(() => matrix.SumWordsWithPrefix(prefix));
        }

        [Test]
        public void SumWordsWithPrefix_NoMatches_ReturnsEmptyList()
        {
            int dim = 4;
            var matrix = new Matrix(dim);

            for (int j = 0; j < dim; j++)
                matrix.SetAddressColumn(j, new bool[dim]);
            
            bool[] prefix = { true };
            var results = matrix.SumWordsWithPrefix(prefix);
            Assert.That(results, Is.Not.Null);
            Assert.That(results, Is.Empty);
        }

        [Test]
        public void SumWordsWithPrefix_WriteBack_UpdatesMatrix()
        {
            int dim = 4;
            var matrix = new Matrix(dim);
            
            bool[] original = [true, false, true, false];
            matrix.SetWord(2, original);

            bool[] prefix = [true, false];
            var results = matrix.SumWordsWithPrefix(prefix, 1, 1, 1,  1);
            
            Assert.That(1, Is.EqualTo(results.Count));
            var (idx, summed) = results[0];
            Assert.That(2, Is.EqualTo(idx));
            
            bool[] updated = matrix.GetWord(2);
            Assert.That(summed, Is.EqualTo(updated));
        }
    }
}
