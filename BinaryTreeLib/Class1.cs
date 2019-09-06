using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BinaryTreeLib
{
    public sealed class BinaryTree<T> : ICollection<T>
    {
        /// <summary>
        /// Node of Tree
        /// </summary>
        /// <typeparam name="TValue"></typeparam>
        private class Node<TValue>
        {
            public TValue Value { get; set; }
            public bool IsVisited { get; set; }
            public Node<TValue> LeftValue { get; set; }
            public Node<TValue> RightValue { get; set; }
        }
        private Node<T> root;
        private int version;

        public IComparer<T> Comparer { get; }
        public int Size { get; private set; }

        public BinaryTree() : this(Comparer<T>.Default)
        {

        }

        public BinaryTree(IEnumerable<T> values) : this(Comparer<T>.Default)
        {
            AddRange(values);
        }

        public BinaryTree(IComparer<T> comparer)
        {
            if (comparer == null)
            {
                comparer = Comparer<T>.Default;
            }

            Comparer = comparer;
        }

        public BinaryTree(IEnumerable<T> values, IComparer<T> comparer) : this(comparer)
        {
            AddRange(values);
        }

        public IEnumerator<T> GetEnumerator()
        {
            if (root == null)
            {
                yield break;
            }

            Stack<Node<T>> tempValues = new Stack<Node<T>>();
            tempValues.Push(root);

            while (tempValues.Count > 0)
            {
                Node<T> currentNode = tempValues.Pop();
                yield return currentNode.Value;

                if (currentNode.RightValue != null)
                {
                    tempValues.Push(currentNode.RightValue);
                }

                if (currentNode.LeftValue != null)
                {
                    tempValues.Push(currentNode.LeftValue);
                }
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public IEnumerable<T> Inorder()
        {
            if (root == null)
            {
                yield break;
            }

            Stack<Node<T>> tempValues = new Stack<Node<T>>();
            tempValues.Push(root);

            while (tempValues.Count > 0)
            {
                Node<T> currentNode = tempValues.Peek();

                while (currentNode.LeftValue != null)
                {
                    if (currentNode.LeftValue.IsVisited)
                        break;
                    tempValues.Push(currentNode.LeftValue);
                    currentNode = currentNode.LeftValue;
                }

                Node<T> temp = tempValues.Pop();
                temp.IsVisited = true;
                yield return temp.Value;


                if (currentNode.RightValue != null)
                {
                    tempValues.Push(currentNode.RightValue);
                    currentNode = currentNode.RightValue;
                }
            }
            ClearFlags();
        }

        public IEnumerable<T> PostOrder()
        {
            if (root == null)
            {
                yield break;
            }

            Stack<Node<T>> tempValues = new Stack<Node<T>>();
            tempValues.Push(root);

            while (tempValues.Count > 0)
            {
                Node<T> currentNode = tempValues.Peek();

                while (currentNode.LeftValue != null)
                {
                    if (currentNode.LeftValue.IsVisited)
                        break;
                    tempValues.Push(currentNode.LeftValue);
                    currentNode = currentNode.LeftValue;
                }

                if (currentNode.RightValue != null && !currentNode.RightValue.IsVisited)
                {
                    tempValues.Push(currentNode.RightValue);
                    currentNode = currentNode.RightValue;
                }

                if (currentNode.LeftValue == null && currentNode.RightValue == null)
                {
                    Node<T> temp = tempValues.Pop();
                    temp.IsVisited = true;
                    yield return temp.Value;
                }
                else if (currentNode.LeftValue == null && currentNode.RightValue != null)
                {
                    if (currentNode.RightValue.IsVisited)
                    {
                        Node<T> temp = tempValues.Pop();
                        temp.IsVisited = true;
                        yield return temp.Value;
                    }
                }
                else if (currentNode.LeftValue != null && currentNode.RightValue == null)
                {
                    if (currentNode.LeftValue.IsVisited)
                    {
                        Node<T> temp = tempValues.Pop();
                        temp.IsVisited = true;
                        yield return temp.Value;
                    }
                }
                else if (currentNode.LeftValue.IsVisited && currentNode.RightValue.IsVisited)
                {
                    Node<T> temp = tempValues.Pop();
                    temp.IsVisited = true;
                    yield return temp.Value;
                }
            }
            ClearFlags();
        }

        public void Add(T item)
        {
            if (root == null)
            {
                root = new Node<T> { Value = item };
                Size++;
                return;
            }

            Node<T> temp = root;
            while (temp != null)
            {
                if (Comparer.Compare(item, temp.Value) > 0)
                {
                    if (temp.RightValue == null)
                    {
                        temp.RightValue = new Node<T> { Value = item };
                        break;
                    }
                    temp = temp.RightValue;
                }
                else if (Comparer.Compare(item, temp.Value) < 0)
                {
                    if (temp.LeftValue == null)
                    {
                        temp.LeftValue = new Node<T> { Value = item };
                        break;
                    }
                    temp = temp.LeftValue;
                }
                else
                {
                    break;
                }
            }
            Size++;
        }

        private void ClearFlags()
        {
            if (root == null)
            {
                return;
            }

            Stack<Node<T>> tempValues = new Stack<Node<T>>();
            tempValues.Push(root);

            while (tempValues.Count > 0)
            {
                Node<T> currentNode = tempValues.Pop();
                currentNode.IsVisited = false;

                if (currentNode.RightValue != null)
                {
                    tempValues.Push(currentNode.RightValue);
                }

                if (currentNode.LeftValue != null)
                {
                    tempValues.Push(currentNode.LeftValue);
                }
            }
        }

        public void AddRange(IEnumerable<T> values)
        {
            if (values == null)
            {
                throw new ArgumentNullException(nameof(values));
            }

            foreach (var item in values)
            {
                Add(item);
            }
        }

        public void Clear()
        {
            Size = 0;
            root = null;
        }

        public bool Contains(T item)
        {
            if (root == null)
            {
                return false;
            }

            Node<T> temp = root;
            while (temp != null)
            {
                if (Comparer.Compare(item, temp.Value) == 0)
                {
                    return true;
                }
                if (Comparer.Compare(item, temp.Value) > 0)
                {
                    temp = temp.RightValue;
                }
                else if (Comparer.Compare(item, temp.Value) < 0)
                {
                    temp = temp.LeftValue;
                }
            }

            return false;
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            throw new NotImplementedException();
        }

        public bool Remove(T item)
        {
            throw new NotImplementedException();
        }

        public int Count
        {
            get
            {
                return Size;
            }
        }

        public bool IsReadOnly
        {
            get
            {
                return false;
            }
        }
    }
}
