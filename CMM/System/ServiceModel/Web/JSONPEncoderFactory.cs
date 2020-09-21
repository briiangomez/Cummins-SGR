namespace System.ServiceModel.Web
{
    using System;
    using System.IO;
    using System.Runtime.Serialization.Json;
    using System.ServiceModel.Channels;
    using System.Xml;

    public class JSONPEncoderFactory : MessageEncoderFactory
    {
        private JSONPEncoder encoder = new JSONPEncoder();

        public override MessageEncoder Encoder
        {
            get
            {
                return this.encoder;
            }
        }

        public override System.ServiceModel.Channels.MessageVersion MessageVersion
        {
            get
            {
                return this.encoder.MessageVersion;
            }
        }

        private class JSONPEncoder : MessageEncoder
        {
            private MessageEncoder encoder;

            public JSONPEncoder()
            {
                WebMessageEncodingBindingElement element = new WebMessageEncodingBindingElement();
                this.encoder = element.CreateMessageEncoderFactory().Encoder;
            }

            public override bool IsContentTypeSupported(string contentType)
            {
                return this.encoder.IsContentTypeSupported(contentType);
            }

            public override Message ReadMessage(ArraySegment<byte> buffer, BufferManager bufferManager, string contentType)
            {
                return this.encoder.ReadMessage(buffer, bufferManager, contentType);
            }

            public override Message ReadMessage(Stream stream, int maxSizeOfHeaders, string contentType)
            {
                return this.encoder.ReadMessage(stream, maxSizeOfHeaders, contentType);
            }

            public override void WriteMessage(Message message, Stream stream)
            {
                string methodName = null;
                if (message.Properties.ContainsKey("Microsoft.ServiceModel.Samples.JSONPMessageProperty"))
                {
                    methodName = ((JSONPMessageProperty) message.Properties["Microsoft.ServiceModel.Samples.JSONPMessageProperty"]).MethodName;
                }
                if (methodName == null)
                {
                    this.encoder.WriteMessage(message, stream);
                }
                else
                {
                    this.WriteToStream(stream, methodName + "( ");
                    this.encoder.WriteMessage(message, stream);
                    this.WriteToStream(stream, " );");
                }
            }

            public override ArraySegment<byte> WriteMessage(Message message, int maxMessageSize, BufferManager bufferManager, int messageOffset)
            {
                MemoryStream stream = new MemoryStream();
                StreamWriter writer = new StreamWriter(stream);
                string methodName = null;
                if (message.Properties.ContainsKey("Microsoft.ServiceModel.Samples.JSONPMessageProperty"))
                {
                    methodName = ((JSONPMessageProperty) message.Properties["Microsoft.ServiceModel.Samples.JSONPMessageProperty"]).MethodName;
                }
                if (methodName != null)
                {
                    writer.Write(methodName + "( ");
                    writer.Flush();
                }
                XmlWriter writer2 = JsonReaderWriterFactory.CreateJsonWriter(stream);
                message.WriteMessage(writer2);
                writer2.Flush();
                if (methodName != null)
                {
                    writer.Write(" );");
                    writer.Flush();
                }
                byte[] sourceArray = stream.GetBuffer();
                int position = (int) stream.Position;
                int bufferSize = position + messageOffset;
                byte[] destinationArray = bufferManager.TakeBuffer(bufferSize);
                Array.Copy(sourceArray, 0, destinationArray, messageOffset, position);
                ArraySegment<byte> segment = new ArraySegment<byte>(destinationArray, messageOffset, position);
                writer2.Close();
                return segment;
            }

            public void WriteToStream(Stream stream, string content)
            {
                using (StreamWriter writer = new StreamWriter(stream))
                {
                    writer.Write(content);
                }
            }

            public override string ContentType
            {
                get
                {
                    return this.encoder.ContentType;
                }
            }

            public override string MediaType
            {
                get
                {
                    return this.encoder.MediaType;
                }
            }

            public override System.ServiceModel.Channels.MessageVersion MessageVersion
            {
                get
                {
                    return this.encoder.MessageVersion;
                }
            }
        }
    }
}

