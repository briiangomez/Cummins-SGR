namespace CMM.Dynamic.Calculator.Support
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    public class ExStack<T> : IEnumerable<T>, IEnumerable
    {
        private List<T> list;

        public ExStack()
        {
            this.list = null;
            this.list = new List<T>();
        }

        public ExStack(int Capacity)
        {
            this.list = null;
            this.list = new List<T>(Capacity);
        }

        public IEnumerator<T> GetEnumerator()
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public T Peek()
        {
            if (this.Count == 0)
            {
                return default(T);
            }
            return this.list[this.Count - 1];
        }

        public T[] Peek(int PeekCount)
        {
            if (this.Count == 0)
            {
                return null;
            }
            if (PeekCount > this.Count)
            {
                PeekCount = this.Count;
            }
            T[] localArray = new T[PeekCount];
            for (int i = 0; i < PeekCount; i++)
            {
                localArray[i] = this.list[(this.Count - 1) - i];
            }
            return localArray;
        }

        public T[] PeekAll()
        {
            return this.Peek(this.Count);
        }

        public T Pop()
        {
            if (this.Count == 0)
            {
                return default(T);
            }
            T local = this.list[this.Count - 1];
            this.list.RemoveAt(this.Count - 1);
            return local;
        }

        public T[] Pop(int PopCount)
        {
            if (this.Count == 0)
            {
                return null;
            }
            if (PopCount > this.Count)
            {
                PopCount = this.Count;
            }
            T[] localArray = new T[PopCount];
            for (int i = 0; i < PopCount; i++)
            {
                localArray[i] = this.Pop();
            }
            return localArray;
        }

        public T[] PopAll()
        {
            return this.Pop(this.Count);
        }

        public ExStack<T> PopStack(int PopCount)
        {
            ExStack<T> stack = new ExStack<T>(PopCount);
            for (int i = 0; i < PopCount; i++)
            {
                stack.Push(this.Pop());
            }
            stack.Reverse();
            return stack;
        }

        public void Push(ExStack<T> Stack)
        {
            Stack.Reverse();
            for (int i = 0; i < Stack.Count; i++)
            {
                this.Push(Stack.Pop());
            }
        }

        public void Push(T[] Items)
        {
            for (int i = 0; i < Items.Length; i++)
            {
                this.Push(Items[i]);
            }
        }

        public void Push(T Item)
        {
            this.list.Add(Item);
        }

        public void RemoveItem(int index)
        {
            if ((index >= 0) && (index < this.Count))
            {
                this.list.RemoveAt(index);
            }
        }

        public void Reverse()
        {
            this.list.Reverse();
        }

        public int Search(string Item)
        {
            int num = 0;
            bool flag = false;
            for (int i = 0; i < this.Count; i++)
            {
                num++;
                T local = this.list[(this.Count - 1) - i];
                if (local.ToString() == Item)
                {
                    flag = true;
                    break;
                }
            }
            if (flag)
            {
                return num;
            }
            return 0;
        }

        public int Search(T Item)
        {
            int num = 0;
            bool flag = false;
            for (int i = 0; i < this.Count; i++)
            {
                num++;
                T local = this.list[(this.Count - 1) - i];
                if (local.Equals(Item))
                {
                    flag = true;
                    break;
                }
            }
            if (flag)
            {
                return num;
            }
            return 0;
        }

        public void Swap()
        {
            if (this.Count >= 2)
            {
                T item = this.Pop();
                T local2 = this.Pop();
                this.Push(item);
                this.Push(local2);
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public int Count
        {
            get
            {
                return this.list.Count;
            }
        }

        public bool IsEmpty
        {
            get
            {
                return (this.Count == 0);
            }
        }
    }
}

