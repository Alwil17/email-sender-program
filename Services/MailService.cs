using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace EmailSenderProgram.Services
{
    public static class MailService
    {
        private const string DefaultFrom = "info@EO.com";

        public static void SendEmail(string to, string subject, string body)
        {
#if DEBUG
            //Don't send mails in debug mode, just write the emails in console
            Console.WriteLine($"[DEBUG] Email to: {to}");
            Console.WriteLine($"Subject: {subject}");
            Console.WriteLine($"Body: {body}");
#else
            // Created MailMessage with all mail components
            var mail = new MailMessage
            {
                From = new MailAddress(DefaultFrom),
                Subject = subject,
                Body = body,
                IsBodyHtml = true
            };

            // Add mail recipient
            mail.To.Add(to);
            // Create new smtp client
            var smtp = new SmtpClient("SMTP_HOST");
            // send mail
            smtp.Send(mail);
#endif
        }
    }
}
