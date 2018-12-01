using ExpectedObjects;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ZipSample.test
{
    [TestClass]
    public class UnionTests
    {
        [TestMethod]
        public void Union_integers()
        {
            var first = new List<int> { 1, 3, 3, 5 };
            var second = new List<int> { 5, 3, 7, 9 };

            var expected = new List<int> { 1, 3, 5, 7, 9 };

            var actual = MyUnion<int>(first, second).ToList();
            expected.ToExpectedObject().ShouldEqual(actual);
        }
        [TestMethod]
        public void Union_Girls()
        {
            var first = new List<Girl>
            {
                new Girl(){Name="lulu",Age = 18},
                new Girl(){Name="lily",Age = 12},
            };
            var second = new List<Girl>
            {
                new Girl(){Name="leo",Age = 17},
                new Girl(){Name="lily",Age = 12},

            };

            var expected = new List<Girl>
            {
                new Girl(){Name="lulu",Age = 18},
                new Girl(){Name="lily",Age = 12},
                new Girl(){Name="leo",Age = 17},
            };

            var actual = MyUnion<Girl>(first, second,new MyComparer()).ToList();
            expected.ToExpectedObject().ShouldEqual(actual);
        }

        private IEnumerable<TSource> MyUnion<TSource>(IEnumerable<TSource> first, IEnumerable<TSource> second)
        {
            return MyUnion(first, second, EqualityComparer<TSource>.Default);
        }
        private IEnumerable<TSource> MyUnion<TSource>(IEnumerable<TSource> first, IEnumerable<TSource> second, IEqualityComparer<TSource> myComparer)
        {
            var firstEnumerator = first.GetEnumerator();
            var secondEnumerator = second.GetEnumerator();
            
            var result = new HashSet<TSource>(myComparer);
            while (firstEnumerator.MoveNext())
            {
                if (result.Add(firstEnumerator.Current))
                {
                    yield return firstEnumerator.Current;
                }
                
            }
            while (secondEnumerator.MoveNext())
            {
                if (result.Add(secondEnumerator.Current))
                {
                    yield return secondEnumerator.Current;
                }
            }
        }

        class MyComparer : IEqualityComparer<Girl>
        {
            public bool Equals(Girl x, Girl y)
            {
                return x.Name == y.Name && x.Age == y.Age;
            }

            public int GetHashCode(Girl obj)
            {
                return obj.Name.GetHashCode() + obj.Age.GetHashCode();
            }
        }
    }
}