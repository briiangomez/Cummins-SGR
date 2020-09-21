namespace System.ServiceModel.Web
{
    using System;
    using System.ServiceModel.Channels;
    using System.ServiceModel.Configuration;

    public class JsonpBindingExtension : BindingElementExtensionElement
    {
        protected override BindingElement CreateBindingElement()
        {
            return new JSONPBindingElement();
        }

        public override System.Type BindingElementType
        {
            get
            {
                return typeof(JSONPBindingElement);
            }
        }
    }
}

