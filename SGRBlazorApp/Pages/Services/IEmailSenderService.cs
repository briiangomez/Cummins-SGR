using SGRBlazorApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SGRBlazorApp.Services
{
    public interface IEmailSenderService
    {
        void SendEmailAsync(MailRequest request);

        void SendEmailFullAsync(MailRequest request);

        void SendEmailAsyncEstado(MailRequest request);
    }
}
