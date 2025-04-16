using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using EmailSenderProgram.Config;

namespace EmailSenderProgram.Services
{
    public static class MailService
    {

        public static void SendEmail(string to, string subject, string body)
        {
            if (ConfigResolver.IsDebug())
            {
                //Don't send mails in debug mode, just write the emails in console
                Console.WriteLine($"[DEBUG] Email to: {to}");
                Console.WriteLine($"Subject: {subject}");
                Console.WriteLine($"Body: {body}");
            }
            else
            {
                // Created MailMessage with all mail components
                var mail = new MailMessage
                {
                    From = new MailAddress(ConfigResolver.MailSender),
                    Subject = subject,
                    Body = body,
                    IsBodyHtml = true
                };

                // Add mail recipient
                mail.To.Add(to);
                // Create new smtp client
                var smtp = new SmtpClient(ConfigResolver.MailHost)
                {
                    Port = ConfigResolver.MailPort,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential(ConfigResolver.MailSender, ConfigResolver.MailPassword),
                    EnableSsl = ConfigResolver.MailIsSSL,
                };
                // send mail
                smtp.Send(mail);
            }
        }
    }
}
