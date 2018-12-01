using ExpectedObjects;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;

namespace ZipSample.test
{
    [TestClass]
    public class IntersectTests
    {
        [TestMethod]
        public void intersect_integers()
        {
            var first = new List<int> { 1, 3, 3, 5 };
            var second = new List<int> { 5, 5, 3, 7, 9 };

            var expected = new List<int> { 3, 5 };

            var actual = MyIntersect(first, second).ToList();
            expected.ToExpectedObject().ShouldEqual(actual);
        }

        private IEnumerable<int> MyIntersect(IEnumerable<int> first, IEnumerable<int> second)
        {
            var firstEnumerator = first.GetEnumerator();
            var secondEnumerator = second.GetEnumerator();
            var hashSet = new HashSet<int>();
            var alreadyReturn = new HashSet<int>();
            while (firstEnumerator.MoveNext())
            {
                if (hashSet.Add(firstEnumerator.Current) == false 
                    && alreadyReturn.Add(firstEnumerator.Current))
                {
                    yield return firstEnumerator.Current;
                }
            }

            while (secondEnumerator.MoveNext())
            {
                if (hashSet.Add(secondEnumerator.Current) == false 
                    && alreadyReturn.Add(secondEnumerator.Current))
                {
                    yield return secondEnumerator.Current;
                }
            }
        }
    }
}