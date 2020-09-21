namespace CMM.Dynamic.Calculator.Support
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    public class ExStackEnumerator<T> : IEnumerator<T>, IDisposable, IEnumerator
    {
        private List<T> items;
        private int location;

        public ExStackEnumerator(List<T> Items)
        {
            this.items = null;
            this.items = Items;
            this.location = -1;
        }

        public void Dispose()
        {
        }

        public bool MoveNext()
        {
            this.location++;
            return (this.location < this.items.Count);
        }

        public void Reset()
        {
            this.location = -1;
        }

        public T Current
        {
            get
            {
                if ((this.location <= 0) && (this.location >= this.items.Count))
                {
                    throw new InvalidOperationException("The enumerator is out of bounds");
                }
                return this.items[this.location];
            }
        }

        object IEnumerator.Current
        {
            get
            {
                if ((this.location <= 0) && (this.location >= this.items.Count))
                {
                    throw new InvalidOperationException("The enumerator is out of bounds");
                }
                return this.items[this.location];
            }
        }
    }
}

