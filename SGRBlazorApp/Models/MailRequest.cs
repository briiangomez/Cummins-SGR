using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SGRBlazorApp.Models
{
    public class MailRequest
    {
        public string Email { get; set; }

        public string Nombre { get; set; }

        public string Subject { get; set; }

        public string Body { get; set; }

        public string Url { get; set; }

        public string EstadoInc { get; set; }

        public string NumeroInc { get; set; }

        public string SenderName { get; set; }

        public string SenderEmail { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }

        public string Server { get; set; }

        public int Port { get; set; }
    }
}
