namespace CMM.Reflection
{
    using System;
    using System.Runtime.CompilerServices;

    public class Members
    {
        public Members(object o)
        {
            this.Properties = new CMM.Reflection.Properties(o);
        }

        public CMM.Reflection.Properties Properties { get; set; }
    }
}

