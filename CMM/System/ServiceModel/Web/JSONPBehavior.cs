namespace System.ServiceModel.Web
{
    using System;
    using System.Runtime.CompilerServices;
    using System.ServiceModel;
    using System.ServiceModel.Channels;
    using System.ServiceModel.Description;
    using System.ServiceModel.Dispatcher;

    public class JSONPBehavior : Attribute, IOperationBehavior
    {
        public const string DefaultCallback = "callback";

        public JSONPBehavior() : this("callback")
        {
        }

        public JSONPBehavior(string callback)
        {
            this.Callback = callback;
        }

        public void AddBindingParameters(OperationDescription operationDescription, BindingParameterCollection bindingParameters)
        {
        }

        public void ApplyClientBehavior(OperationDescription operationDescription, ClientOperation clientOperation)
        {
            clientOperation.ParameterInspectors.Add(new Inspector(this.Callback));
        }

        public void ApplyDispatchBehavior(OperationDescription operationDescription, DispatchOperation dispatchOperation)
        {
            dispatchOperation.ParameterInspectors.Add(new Inspector(this.Callback));
        }

        public void Validate(OperationDescription operationDescription)
        {
        }

        public string Callback { get; set; }

        private class Inspector : IParameterInspector
        {
            private string callback;

            public Inspector(string callback)
            {
                this.callback = callback;
            }

            public void AfterCall(string operationName, object[] outputs, object returnValue, object correlationState)
            {
            }

            public object BeforeCall(string operationName, object[] inputs)
            {
                string str = WebOperationContext.Current.IncomingRequest.UriTemplateMatch.QueryParameters[this.callback];
                if (str != null)
                {
                    JSONPMessageProperty property2 = new JSONPMessageProperty {
                        MethodName = str
                    };
                    JSONPMessageProperty property = property2;
                    OperationContext.Current.OutgoingMessageProperties.Add("Microsoft.ServiceModel.Samples.JSONPMessageProperty", property);
                }
                return null;
            }
        }
    }
}

