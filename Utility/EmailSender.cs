using Microsoft.AspNetCore.Identity.UI.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Utility
{
    public class EmailSender : IEmailSender
    {
        public async Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            var FromMail = "";
            var FromPassword = "";

            var message = new MailMessage();
            message.From = new MailAddress(FromMail);
            message.Subject = subject;
            message.To.Add(email);
            message.Body = $"<html><body>{htmlMessage}</body></html>";
            message.IsBodyHtml = true;

            var smtpClint = new SmtpClient(host: "smtp.gmail.com")
            {
                Port = 587,
                Credentials = new NetworkCredential(FromMail, FromPassword),
                EnableSsl = true
            };
            smtpClint.Send(message);
        }
    }
}
