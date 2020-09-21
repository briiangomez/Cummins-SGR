namespace CMM.IoC
{
    using System;
    using System.Runtime.CompilerServices;

    internal class ObjectKey
    {
        public ObjectKey(System.Type type, string name)
        {
            this.Type = type;
            this.Name = name;
        }

        public override bool Equals(object obj)
        {
            if (obj is ObjectKey)
            {
                ObjectKey key = obj as ObjectKey;
                return ((this.Type == key.Type) && (this.Name == key.Name));
            }
            return false;
        }

        public override int GetHashCode()
        {
            string str = (("ObjectKey.Type=" + this.Type) == null) ? "null" : this.Type.ToString();
            return (str + (((";ObjectKey.Name=" + this.Name) == null) ? "null" : this.Name)).GetHashCode();
        }

        public string Name { get; private set; }

        public System.Type Type { get; private set; }
    }
}

