﻿namespace CMM.Web
{
    using System;
    using System.Collections;
    using System.Collections.Specialized;
    using System.Reflection;
    using System.Web;

    public class HttpApplicationStateBaseWrapper : HttpApplicationStateBase
    {
        private HttpApplicationStateBase _application;

        public HttpApplicationStateBaseWrapper(HttpApplicationStateBase httpApplicationState)
        {
            if (httpApplicationState == null)
            {
                throw new ArgumentNullException("httpApplicationState");
            }
            this._application = httpApplicationState;
        }

        public override void Add(string name, object value)
        {
            this._application.Add(name, value);
        }

        public override void Clear()
        {
            this._application.Clear();
        }

        public override void CopyTo(Array array, int index)
        {
            ((ICollection) this._application).CopyTo(array, index);
        }

        public override object Get(int index)
        {
            return this._application.Get(index);
        }

        public override object Get(string name)
        {
            return this._application.Get(name);
        }

        public override IEnumerator GetEnumerator()
        {
            return this._application.GetEnumerator();
        }

        public override string GetKey(int index)
        {
            return this._application.GetKey(index);
        }

        public override void Lock()
        {
            this._application.Lock();
        }

        public override void Remove(string name)
        {
            this._application.Remove(name);
        }

        public override void RemoveAll()
        {
            this._application.RemoveAll();
        }

        public override void RemoveAt(int index)
        {
            this._application.RemoveAt(index);
        }

        public override void Set(string name, object value)
        {
            this._application.Set(name, value);
        }

        public override void UnLock()
        {
            this._application.UnLock();
        }

        public override string[] AllKeys
        {
            get
            {
                return this._application.AllKeys;
            }
        }

        public override HttpApplicationStateBase Contents
        {
            get
            {
                return this;
            }
        }

        public override int Count
        {
            get
            {
                return this._application.Count;
            }
        }

        public override bool IsSynchronized
        {
            get
            {
                return this._application.IsSynchronized;
            }
        }

        public override object this[string name]
        {
            get
            {
                return this._application[name];
            }
            set
            {
                this._application[name] = value;
            }
        }

        public override object this[int index]
        {
            get
            {
                return this._application[index];
            }
        }

        public override NameObjectCollectionBase.KeysCollection Keys
        {
            get
            {
                return this._application.Keys;
            }
        }

        public override HttpStaticObjectsCollectionBase StaticObjects
        {
            get
            {
                return new HttpStaticObjectsCollectionBaseWrapper(this._application.StaticObjects);
            }
        }

        public override object SyncRoot
        {
            get
            {
                return this._application.SyncRoot;
            }
        }
    }
}

