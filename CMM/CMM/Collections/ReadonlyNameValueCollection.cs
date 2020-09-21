namespace CMM.Collections
{
    using System;
    using System.Collections;
    using System.Collections.Specialized;
    using System.Runtime.Serialization;

    public class ReadonlyNameValueCollection : NameValueCollection
    {
        public ReadonlyNameValueCollection()
        {
        }

        public ReadonlyNameValueCollection(IEqualityComparer equalityComparer) : base(equalityComparer)
        {
        }

        public ReadonlyNameValueCollection(NameValueCollection col)
        {
            base.Add(col);
        }

        public ReadonlyNameValueCollection(int capacity) : base(capacity)
        {
        }

        public ReadonlyNameValueCollection(int capacity, IEqualityComparer equalityComparer) : base(capacity, equalityComparer)
        {
        }

        public ReadonlyNameValueCollection(int capacity, NameValueCollection col)
        {
            if (col == null)
            {
                throw new ArgumentNullException("col");
            }
            base.Add(col);
        }

        protected ReadonlyNameValueCollection(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        public void MakeReadOnly()
        {
            base.IsReadOnly = true;
        }

        public void MakeReadWrite()
        {
            base.IsReadOnly = false;
        }
    }
}

