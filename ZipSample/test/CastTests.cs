﻿using ExpectedObjects;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace ZipSample.test
{
    [TestClass]
    public class CastTests
    {
        [TestMethod]
        public void cast_integers()
        {
            var arrayList = new ArrayList { 2, 6 };
            var actual = MyCast<int>(arrayList).ToList();

            var expected = new List<int> { 2, 6 };
            expected.ToExpectedObject().ShouldEqual(actual);
        }

        [TestMethod]
        public void cast_string()
        {
            var arrayList = new ArrayList { "2", "6" };
            var actual = MyCast<string>(arrayList).ToList();

            var expected = new List<string> { "2", "6" };
            expected.ToExpectedObject().ShouldEqual(actual);
        }


        [TestMethod]
        //[ExpectedException(typeof(InvalidCastException))]
        public void cast_integers_with_cast_failed()
        {
            var arrayList = new ArrayList { 2, "4", 6 };
            Action action = () => MyCast<int>(arrayList).ToList();
            action.Should().Throw<InvalidCastException>();
        }

        private IEnumerable<TResult> MyCast<TResult>(IEnumerable arrayList)
        {
            var enumerator = arrayList.GetEnumerator();
            while (enumerator.MoveNext())
            {
                yield return (TResult)enumerator.Current;
            }
        }
    }
}