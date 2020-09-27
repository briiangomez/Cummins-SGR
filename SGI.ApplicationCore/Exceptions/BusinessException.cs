using System;
namespace SGI.ApplicationCore.Exceptions
{
    public class BusinessException : ApplicationException
    {
        public int Code { get; set; }

        public BusinessException(int code, string message) : base(message)
        {
            Code = code;
        }

        public BusinessException(int code, string message, Exception innerException) : base(message, innerException)
        {
            Code = code;
        }

    }
}
