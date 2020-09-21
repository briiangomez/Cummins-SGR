namespace CMM.Web.Css
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;

    public class Declaration : ICollection<Property>, IEnumerable<Property>, IEnumerable
    {
        private ISet<Property> _set;

        public void Add(Property item)
        {
            if (this.Set.Contains(item))
            {
                this.Set.Remove(item);
            }
            this.Set.Add(item);
        }

        public void AddRange(IEnumerable<Property> items)
        {
            foreach (Property property in items)
            {
                this.Add(property);
            }
        }

        public void Clear()
        {
            this.Set.Clear();
        }

        public bool Contains(Property item)
        {
            return this.Set.Contains(item);
        }

        public void CopyTo(Property[] array, int arrayIndex)
        {
            this.Set.CopyTo(array, arrayIndex);
        }

        public IEnumerator<Property> GetEnumerator()
        {
            return this.Set.GetEnumerator();
        }

        public static Declaration Parse(string str)
        {
            Declaration declaration = new Declaration();
            foreach (string str2 in str.Trim().Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries))
            {
                Property property;
                if (Property.TryParse(str2, out property) && !(property.IsInitial || property.IsBrowserGenerated))
                {
                    declaration.Add(property);
                }
            }
            return declaration;
        }

        public bool Remove(Property item)
        {
            return this.Set.Remove(item);
        }

        public void Remove(string name)
        {
            Property item = this.Set.FirstOrDefault<Property>(o => o.Name == name);
            if (item != null)
            {
                this.Set.Remove(item);
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        public override string ToString()
        {
            return string.Join(";", (IEnumerable<string>) (from o in this.Set select o.ToString()));
        }

        public int Count
        {
            get
            {
                return this.Set.Count;
            }
        }

        public bool IsReadOnly
        {
            get
            {
                return this.Set.IsReadOnly;
            }
        }

        protected ISet<Property> Set
        {
            get
            {
                if (this._set == null)
                {
                    this._set = new HashSet<Property>();
                }
                return this._set;
            }
        }
    }
}

