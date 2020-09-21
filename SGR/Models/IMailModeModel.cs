namespace SGR.Communicator.Models
{
    using System;

    public interface IMailModeModel : IMailTemplateModel
    {
        string MailMode { get; set; }
    }
}

