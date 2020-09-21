namespace CMM.Dynamic.Calculator.Support
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Reflection;

    public class ExQueue<T> : IEnumerable<T>, IEnumerable
    {
        private List<T> queue;

        public ExQueue()
        {
            this.queue = null;
            this.queue = new List<T>();
        }

        public ExQueue(int Capacity)
        {
            this.queue = null;
            this.queue = new List<T>(Capacity);
        }

        public void Add(T item)
        {
            this.queue.Add(item);
        }

        public void Clear()
        {
            this.queue.Clear();
        }

        public T Dequeue()
        {
            if (this.queue.Count == 0)
            {
                return default(T);
            }
            T local = this.queue[0];
            this.queue.RemoveAt(0);
            return local;
        }

        public void Enqueue(T item)
        {
            this.Add(item);
        }

        public IEnumerator<T> GetEnumerator()
        {
            return new ExQueueEnumerator<T>(this.queue);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return new ExQueueEnumerator<T>(this.queue);
        }

        public int Count
        {
            get
            {
                return this.queue.Count;
            }
        }

        public bool IsEmpty
        {
            get
            {
                return (this.Count == 0);
            }
        }

        public T this[int index]
        {
            get
            {
                return this.queue[index];
            }
        }
    }
}

