﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using QueueNLib;
namespace Task2.Logic.Tests
{
    [TestFixture]
    public class QueueNTests
    {
        [TestCase(new[] { 1, 2, 3, 2, 4 })]
        [TestCase(new[] { 1 })]
        [TestCase(new int[0])]
        [Test]
        public void QueueNCtor_EnumerableInt_EnumerableQueueExpected
            (int[] expectedElements)
        {
            //act
            QueueN<int> q = new QueueN<int>(expectedElements, 2);
            //assert
            CollectionAssert.AreEquivalent(expectedElements, q);
        }

        public static IEnumerable<TestCaseData> ReferenceTestDataForCtor
        {
            get
            {
                object o = new[] { new Random(), null, new Random(5), };
                yield return new TestCaseData(o);
                o = new[] { "first", "second", null, "fourth" };
                yield return new TestCaseData(o);
            }
        }

        [TestCaseSource(nameof(ReferenceTestDataForCtor))]
        [Test]
        public void QueueNCtor_EnumerableObjects_EnumerableQueueExpected
            (object[] expectedElements)
        {
            //act
            QueueN<object> q = new QueueN<object>(expectedElements);
            //assert
            CollectionAssert.AreEquivalent(q, expectedElements);
        }

        [TestCase(0, typeof(ArgumentException))]
        [TestCase(-1, typeof(ArgumentException))]
        [Test]
        public void QueueNCtor_BadCapacity_ExceptionExpected(int capacity,
            Type expectedException)
        {
            Assert.Throws(expectedException, () => new QueueN<int>(capacity));
        }

        [Test]
        public void QueueNCtor_NullEnumerable_ExceptionExpected()
        {
            Assert.Throws(typeof(ArgumentNullException),
                () => new QueueN<int>(null));
        }

        [TestCase(new[] { 1, 2, 3 })]
        [TestCase(new[] { 1 })]
        [TestCase(new int[0])]
        [Test]
        public void Count_intArray_CountExpected(int[] data)
        {
            //act
            QueueN<int> q = new QueueN<int>(data);
            //assert
            Assert.AreEqual(data.Length, q.Count);
        }

        [TestCase(new[] { 1, 2, 2, 3 })]
        [TestCase(new[] { 2 })]
        [TestCase(new int[0])]
        [Test]
        public void Push_intArray_QueueWithElementsExpected
            (int[] data)
        {
            //arrange
            QueueN<int> q = new QueueN<int>(1);
            //act
            for (int i = 0; i < data.Length; i++)
                q.Push(data[i]);
            //assert
            CollectionAssert.AreEquivalent(data, q);
        }

        [TestCase(new int[0])]
        [TestCase(new[] { 1, 2, 3 })]
        [TestCase(new[] { 1 })]
        [Test]
        public void PushFrontPop_intArray_RightOrderOfElementsExpected
            (int[] data)
        {
            //arrange
            QueueN<int> q = new QueueN<int>(2);
            //act
            for (int i = 0; i < data.Length; i++)
                q.Push(data[i]);
            //assert
            for (int i = 0; i < data.Length; i++)
            {
                int actual = q.Front();
                Assert.AreEqual(data[i], actual);
                q.Pop();
            }
            Assert.AreEqual(true, q.Empty, "queue is not empty, but should be");
        }

        [Test]
        public void Pop_ExceptionExpected()
        {
            //arrange
            QueueN<int> q = new QueueN<int>(2);
            //assert
            Assert.Throws(typeof(InvalidOperationException), () => q.Pop());
        }

        [Test]
        public void Front_ExceptionExpected()
        {
            //arrange
            QueueN<int> q = new QueueN<int>();
            //assert
            Assert.Throws(typeof(InvalidOperationException), () => q.Front());
        }

        [TestCase(new[] { 1, 2, 3, 2 }, new[] { 0, 0, 0, 0 }, 0,
            new[] { 1, 2, 3, 2 })]
        [TestCase(new[] { 1, 2, 3, 2 }, new[] { 0, 0, 0, 0, 0, 0 }, 1,
            new[] { 0, 1, 2, 3, 2, 0 })]
        [TestCase(new[] { 1, 2, 3, 2 }, new[] { 0, 0, 0, 0, 0, 0 }, 2,
            new[] { 0, 0, 1, 2, 3, 2 })]
        [Test]
        public void CopyTo_ArrayExpected
            (int[] data, int[] destination, int index, int[] expectedArray)
        {
            //arrange
            QueueN<int> q = new QueueN<int>(1);
            for (int i = 0; i < data.Length; i++)
                q.Push(data[i]);
            //act
            q.CopyTo(destination, index);
            //assert
            CollectionAssert.AreEqual(expectedArray, destination);
        }

        [Test]
        public void Enumerator_CollectionWasChanged_ExceptionExpected()
        {
            QueueN<int> q = new QueueN<int>(1);
            for (int i = 0; i < 10; i++)
                q.Push(i);
            Assert.Throws(typeof(InvalidOperationException),
                () =>
                {
                    foreach (var el in q)
                    {
                        q.Pop();
                    }
                });
            Assert.Throws(typeof(InvalidOperationException),
                () =>
                {
                    foreach (var el in q)
                    {
                        q.Push(el);
                    }
                });
        }
    }
}