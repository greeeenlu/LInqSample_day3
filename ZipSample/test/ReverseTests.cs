using ExpectedObjects;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;

namespace ZipSample.test
{
    [TestClass]
    public class ReverseTests
    {
        [TestMethod]
        public void reverse_string()
        {
            var source = new string[] { "Apple", "Banana", "Cat" };

            var actual = MyReverse(source).ToList();
            var expected = new List<string> { "Cat", "Banana", "Apple" };

            expected.ToExpectedObject().ShouldEqual(actual);
        }

        private IEnumerable<TSource> MyReverse<TSource>(IEnumerable<TSource> source)
        {
            var enumerator = source.GetEnumerator();
            var stack = new Stack<TSource>();
            while (enumerator.MoveNext())
            {
                stack.Push(enumerator.Current);
            }

            while (stack.Count > 0)
            {
                yield return stack.Pop();
            }
        }
    }
}