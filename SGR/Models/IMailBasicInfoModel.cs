namespace SGR.Communicator.Models
{
    using System;

    public interface IMailBasicInfoModel
    {
        string From { get; set; }

        string FromName { get; set; }

        string ReplyTo { get; set; }
    }
}

