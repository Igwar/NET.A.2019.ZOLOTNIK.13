using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using BinaryTreeLib;
using BookLib;
using Tree = BinaryTreeLib.BinaryTree<int>;
using TreeS = BinaryTreeLib.BinaryTree<string>;
using TreeB = BinaryTreeLib.BinaryTree<BookLib.Book>;


namespace BinaryTreeLib
{
    [TestFixture]
    public class BinaryTreeTests
    {
        private static readonly string[] ArrayStrings = { "g", "i", "d", "c", "e", "j", "h", "k", "a", "b", "f" };
        private static readonly string[] ArrayStringsComparator = { "gbbbbbb", "ibbbbbbbb", "dbbb", "cbb", "ebbbb", "jbbbbbbbbb", "hbbbbbbb", "kbbbbbbbbbb", "a", "bb", "fbbbbb" };
        private static readonly Book[] BooksExpected = { new Book("", "", 50, 34), new Book("", "", 40, 34), new Book("", "", 20, 34), new Book("", "", 60, 34) };
        private static readonly Book[] BooksIn = { new Book("", "", 50, 34), new Book("", "", 40, 34), new Book("", "", 60, 34), new Book("", "", 20, 34) };
        private static readonly Book[] BooksExpectedComparator = { new Book("", "", 50, 50), new Book("", "", 50, 40), new Book("", "", 50, 20), new Book("", "", 50, 60) };
        private static readonly Book[] BooksInComparator = { new Book("", "", 50, 50), new Book("", "", 50, 60), new Book("", "", 50, 40), new Book("", "", 50, 20) };
        private static readonly Point[] PointsExpected = { new Point(50, 50), new Point(40, 40), new Point(20, 20), new Point(60, 60) };
        private static readonly Point[] PointsIn = { new Point(50, 50), new Point(60, 60), new Point(40, 40), new Point(20, 20) };

        [Test]
        [TestCase(new[] { 50, 60, 40, 20, 45, 100, 55, 120, 10, 15, 47 }, ExpectedResult = new[] { 50, 40, 20, 10, 15, 45, 47, 60, 55, 100, 120 })]
        public int[] GetEnumeratorTestPreOrder_TestValuesForEnumeration_AllShouldPass(int[] array)
        {
            Tree tree = new Tree(array);
            int[] actual = new int[tree.Count];
            int i = 0;
            foreach (var item in tree)
            {
                actual[i++] = item;
            }
            return actual;
        }

        [Test]
        [TestCase(new[] { 50, 60, 40, 20, 45, 100, 55, 120, 10, 15, 47 }, ExpectedResult = new[] { 10, 15, 20, 40, 45, 47, 50, 55, 60, 100, 120 })]
        public int[] GetEnumeratorTestInOrder_TestValuesForEnumeration_AllShouldPass(int[] array)
        {
            Tree tree = new Tree(array);
            int[] actual = new int[tree.Count];
            int i = 0;
            foreach (var item in tree.Inorder())
            {
                actual[i++] = item;
            }
            return actual;
        }

        [Test]
        [TestCase(new[] { 50, 60, 40, 20, 45, 100, 55, 120, 10, 15, 47 }, ExpectedResult = new[] { 15, 10, 20, 47, 45, 40, 55, 120, 100, 60, 50 })]
        public int[] GetEnumeratorTestPostOrder_TestValuesForEnumeration_AllShouldPass(int[] array)
        {
            Tree tree = new Tree(array);
            int[] actual = new int[tree.Count];
            int i = 0;
            foreach (var item in tree.PostOrder())
            {
                actual[i++] = item;
            }
            return actual;
        }

        [Test]
        [TestCase(new[] { 50, 60, 40, 20, 45, 100, 55, 120, 10, 15, 47 }, ExpectedResult = new[] { 50, 60, 100, 120, 55, 40, 45, 47, 20, 10, 15 })]
        public int[] GetEnumeratorTestPreOrder_TestValuesWithCustomComparator_AllShouldPass(int[] array)
        {
            Tree tree = new Tree(array, new CompareByValue());
            int[] actual = new int[tree.Count];
            int i = 0;
            foreach (var item in tree)
            {
                actual[i++] = item;
            }
            return actual;
        }

        [Test]
        [TestCase(ExpectedResult = new[] { "g", "d", "c", "a", "b", "e", "f", "i", "h", "j", "k" })]
        public string[] GetEnumeratorTestPreOrder_StringTestValues_AllShouldPass()
        {
            TreeS tree = new TreeS(ArrayStrings);
            string[] actual = new string[tree.Count];
            int i = 0;
            foreach (var item in tree)
            {
                actual[i++] = item;
            }
            return actual;
        }

        [Test]
        [TestCase(ExpectedResult = new[] { "gbbbbbb", "dbbb", "cbb", "a", "bb", "ebbbb", "fbbbbb", "ibbbbbbbb", "hbbbbbbb", "jbbbbbbbbb", "kbbbbbbbbbb" })]
        public string[] GetEnumeratorTestPreOrder_StringTestValuesWithCustomComparator_AllShouldPass()
        {
            TreeS tree = new TreeS(ArrayStringsComparator, new CompareStrings());
            string[] actual = new string[tree.Count];
            int i = 0;
            foreach (var item in tree)
            {
                actual[i++] = item;
            }
            return actual;
        }

        [Test]
        public void GetEnumeratorTestPreOrder_BookTestValues_AllShouldPass()
        {
            TreeB tree = new TreeB(BooksIn);
            Book[] actual = new Book[tree.Count];
            int i = 0;
            foreach (var item in tree)
            {
                actual[i++] = item;
            }
            Assert.AreEqual(BooksExpected, actual);
        }

        [Test]
        public void GetEnumeratorTestPreOrder_BookTestValuesWithCustomComparator_AllShouldPass()
        {
            TreeB tree = new TreeB(BooksInComparator, new SortByYear());
            Book[] actual = new Book[tree.Count];
            int i = 0;
            foreach (var item in tree)
            {
                actual[i++] = item;
            }
            Assert.AreEqual(BooksExpectedComparator, actual);
        }

        [Test]
        public void GetEnumeratorTestPreOrder_PointTestValues_AllShouldPass()
        {
            TreeP tree = new TreeP(PointsIn, new ComparePoints());
            Point[] actual = new Point[tree.Count];
            int i = 0;
            foreach (var item in tree)
            {
                actual[i++] = item;
            }
            Assert.AreEqual(PointsExpected, actual);
        }
    }
}
