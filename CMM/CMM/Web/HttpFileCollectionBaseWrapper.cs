namespace CMM.Web
{
    using System;
    using System.Collections;
    using System.Collections.Specialized;
    using System.Reflection;
    using System.Web;

    public class HttpFileCollectionBaseWrapper : HttpFileCollectionBase
    {
        private HttpFileCollectionBase _collection;

        public HttpFileCollectionBaseWrapper(HttpFileCollectionBase httpFileCollection)
        {
            if (httpFileCollection == null)
            {
                throw new ArgumentNullException("httpFileCollection");
            }
            this._collection = httpFileCollection;
        }

        public override void CopyTo(Array dest, int index)
        {
            this._collection.CopyTo(dest, index);
        }

        public override HttpPostedFileBase Get(int index)
        {
            return this._collection.Get(index);
        }

        public override HttpPostedFileBase Get(string name)
        {
            return this._collection.Get(name);
        }

        public override IEnumerator GetEnumerator()
        {
            return this._collection.GetEnumerator();
        }

        public override string GetKey(int index)
        {
            return this._collection.GetKey(index);
        }

        public override string[] AllKeys
        {
            get
            {
                return this._collection.AllKeys;
            }
        }

        public override int Count
        {
            get
            {
                return this._collection.Count;
            }
        }

        public override bool IsSynchronized
        {
            get
            {
                return this._collection.IsSynchronized;
            }
        }

        public override HttpPostedFileBase this[int index]
        {
            get
            {
                return this._collection[index];
            }
        }

        public override HttpPostedFileBase this[string name]
        {
            get
            {
                return this._collection[name];
            }
        }

        public override NameObjectCollectionBase.KeysCollection Keys
        {
            get
            {
                return this._collection.Keys;
            }
        }

        public override object SyncRoot
        {
            get
            {
                return this._collection.SyncRoot;
            }
        }
    }
}

