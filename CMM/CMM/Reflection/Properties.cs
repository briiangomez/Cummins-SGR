namespace CMM.Reflection
{
    using System;
    using System.Reflection;
    using System.Runtime.CompilerServices;

    public class Properties
    {
        public Properties(object o)
        {
            this.Object = o;
        }

        public object this[string name]
        {
            get
            {
                return PropertyExtensions.GetPropery(this.Object, name, false);
            }
            set
            {
                PropertyExtensions.SetPropery(this.Object, name, value, true);
            }
        }

        public object Object { get; set; }
    }
}

