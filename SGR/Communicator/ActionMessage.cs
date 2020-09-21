using System;
using System.Text;

namespace SGR.Communicator
{
    public class ActionMessage
    {
        public ActionMessage(ActionMessageType messageType, string message, int? timeout = new int?()) : this(messageType.ToString(), message, null)
        {
        }

        public ActionMessage(ActionMessageType messageType, string message, TimeSpan timeout) : this(messageType.ToString(), message, new int?(timeout.Milliseconds))
        {
        }

        public ActionMessage(string messageType, string message, int? timeout = new int?())
        {
            this.MessageType = messageType;
            this.Message = message;
            this.Timeout = timeout;
        }

        private string EncodeDetailsMessage(string msg)
        {
            return this.QuoteEncode(this.DetailsMessage.Replace(@"\", @"\\").Replace("\t", "&nbsp;&nbsp;&nbsp;&nbsp;").Replace(Environment.NewLine, "<br />"));
        }

        public static ActionMessage Error(string message, Exception ex)
        {
            ActionMessage message2 = new ActionMessage(ActionMessageType.Error, message, null);
            if (ex != null)
            {
                StringBuilder builder = new StringBuilder().Append(ex.Message);
                if (!string.IsNullOrEmpty(ex.StackTrace))
                {
                    builder.Append("<br />").Append(ex.StackTrace);
                }
                message2.DetailsMessage = builder.ToString();
            }
            return message2;
        }

        public static ActionMessage Info(string message)
        {
            return new ActionMessage(ActionMessageType.Info, message, null);
        }

        private string QuoteEncode(string str)
        {
            return str.Replace("\"", "%34").Replace("'", "%39");
        }

        public string ToScript()
        {
            StringBuilder builder = new StringBuilder().AppendFormat("message.{0}(", this.MessageType.ToLower()).AppendLine().Append("\"").Append(this.Message.Replace("\"", "\\\"")).Append("\"");
            if (string.IsNullOrEmpty(this.DetailsMessage))
            {
                builder.AppendLine(",").Append("null");
            }
            else
            {
                builder.AppendLine(",").Append("\"").Append(this.EncodeDetailsMessage(this.DetailsMessage)).Append("\"");
            }
            if (this.Timeout.HasValue)
            {
                builder.AppendLine(",").Append(this.Timeout.Value);
            }
            builder.AppendLine(")");
            return builder.ToString();
        }

        public static ActionMessage Warning(string message, Exception ex)
        {
            ActionMessage message2 = new ActionMessage(ActionMessageType.Warning, message, null);
            if (ex != null)
            {
                message2.DetailsMessage = ex.Message;
            }
            return message2;
        }

        public string DetailsMessage { get; set; }

        public string Message { get; set; }

        public string MessageType { get; set; }

        public int? Timeout { get; set; }
    }
}

