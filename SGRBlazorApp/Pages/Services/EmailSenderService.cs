using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using SGR.Models;
using SGRBlazorApp.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace SGRBlazorApp.Services
{
    public class EmailSenderService : IEmailSenderService
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly SmtpSettings _smtpSettings;

        private readonly IConfiguration _config;


        public EmailSenderService(IWebHostEnvironment webHostEnvironment, IOptions<SmtpSettings> smtpSettings, IConfiguration config)
        {
            _webHostEnvironment = webHostEnvironment;
            _smtpSettings = smtpSettings.Value;
            _config = config;
        }
       public void SendEmailAsync(MailRequest request)
        {
            try
            {
                string webRootPath = _webHostEnvironment.WebRootPath;
                MailAddress too2 = new MailAddress(request.Email,request.Nombre);
                MailAddress fro2 = new MailAddress(_smtpSettings.SenderEmail,_smtpSettings.SenderName);
                MailMessage mail2 = new MailMessage(fro2, too2);
                SmtpClient client2 = new SmtpClient();
                client2.DeliveryMethod = SmtpDeliveryMethod.Network;
                client2.UseDefaultCredentials = true;
                client2.Host = _smtpSettings.Server; ;
                client2.Credentials = new NetworkCredential(_smtpSettings.UserName, _smtpSettings.Password);
                string html2 = System.IO.File.ReadAllText(Path.Combine(_webHostEnvironment.ContentRootPath, "ok.html"));
                html2 = html2.Replace("{0}",request.Nombre);
                html2 = html2.Replace("{1}", request.Url);
                html2 = html2.Replace("{2}", DateTime.Now.Year.ToString());
                mail2.Body = html2;
                mail2.Subject = "FAST - Encuesta de Satisfacción";
                mail2.IsBodyHtml = true;
                client2.SendMailAsync(mail2);
                Logger.AddLine(String.Format("ENVIO DE MAIL CORRECTO - {0}-TO:{1}- FROM: -{2}", DateTime.Now.ToString("dd/MM/yyyy"), request.Email,fro2));
            }
            catch (Exception ex)
            {
                Logger.AddLine(String.Format("ERROR - ENVIO DE MAIL - {0}-{1}-{2}", DateTime.Now.ToString("dd/MM/yyyy"), ex.Message, ex.StackTrace));
            }
        }

        public void SendEmailFullAsync(MailRequest request)
        {
            try
            {
                string webRootPath = _webHostEnvironment.WebRootPath;
                MailAddress too2 = new MailAddress(request.Email, request.Nombre);
                MailAddress fro2 = new MailAddress(request.SenderEmail,request.SenderName);
                MailMessage mail2 = new MailMessage(fro2, too2);
                SmtpClient client2 = new SmtpClient();
                client2.DeliveryMethod = SmtpDeliveryMethod.Network;
                client2.UseDefaultCredentials = true;
                client2.Host = request.Server;
                client2.Credentials = new NetworkCredential(request.UserName,request.Password);
                client2.Port = request.Port;
                //string html2 = System.IO.File.ReadAllText(Path.Combine(_webHostEnvironment.ContentRootPath, "ok.html"));
                //html2 = html2.Replace("{0}", request.Nombre);
                //html2 = html2.Replace("{1}", request.Url);
                //html2 = html2.Replace("{2}", DateTime.Now.Year.ToString());
                mail2.Body = request.Body;
                mail2.Subject = request.Subject;
                //mail2.IsBodyHtml = true;
                client2.SendMailAsync(mail2);
                Logger.AddLine(String.Format("ENVIO DE MAIL CORRECTO - {0}-TO:{1}- FROM: -{2}", DateTime.Now.ToString("dd/MM/yyyy"), request.Email, fro2));
            }
            catch (Exception ex)
            {
                Logger.AddLine(String.Format("ERROR - ENVIO DE MAIL - {0}-{1}-{2}", DateTime.Now.ToString("dd/MM/yyyy"), ex.Message, ex.StackTrace));
            }
        }

        public void SendEmailAsyncEstado(MailRequest request)
        {
            try
            {
                string webRootPath = _webHostEnvironment.WebRootPath;
                MailAddress too2 = new MailAddress(request.Email, request.Nombre);
                MailAddress fro2 = new MailAddress(_smtpSettings.SenderEmail, _smtpSettings.SenderName);
                MailMessage mail2 = new MailMessage(fro2, too2);
                SmtpClient client2 = new SmtpClient();
                client2.DeliveryMethod = SmtpDeliveryMethod.Network;
                client2.UseDefaultCredentials = true;
                client2.Host = _smtpSettings.Server;
                client2.Credentials = new NetworkCredential(_smtpSettings.UserName, _smtpSettings.Password);
                string html2 = System.IO.File.ReadAllText(Path.Combine(_webHostEnvironment.ContentRootPath, "estado.html"));
                html2 = html2.Replace("{0}", request.NumeroInc);
                html2 = html2.Replace("{1}", request.EstadoInc);
                html2 = html2.Replace("{2}", DateTime.Now.Year.ToString());
                mail2.Body = html2;
                mail2.Subject = "FAST - Actualización de Estado de Reclamo";
                mail2.IsBodyHtml = true;
                client2.SendMailAsync(mail2);
                Logger.AddLine(String.Format("ENVIO DE MAIL CORRECTO - {0}-TO:{1}- FROM: -{2}", DateTime.Now.ToString("dd/MM/yyyy"), request.Email, fro2));
            }
            catch (Exception ex)
            {
                Logger.AddLine(String.Format("ERROR - ENVIO DE MAIL - {0}-{1}-{2}", DateTime.Now.ToString("dd/MM/yyyy"), ex.Message, ex.StackTrace));
            }
        }
    }
}
