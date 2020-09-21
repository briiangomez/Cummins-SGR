namespace System.ServiceModel.Web
{
    using System;
    using System.Runtime.CompilerServices;
    using System.ServiceModel.Channels;

    public class JSONPMessageProperty : IMessageProperty
    {
        public const string Name = "Microsoft.ServiceModel.Samples.JSONPMessageProperty";

        public JSONPMessageProperty()
        {
        }

        internal JSONPMessageProperty(JSONPMessageProperty other)
        {
            this.MethodName = other.MethodName;
        }

        public IMessageProperty CreateCopy()
        {
            return new JSONPMessageProperty(this);
        }

        public string MethodName { get; set; }
    }
}

