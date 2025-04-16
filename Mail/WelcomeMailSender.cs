using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace EmailSenderProgram.Mail
{
    /// <summary>
    /// Welcome Mail Sender logic.
    /// </summary>
    public class WelcomeMailSender : IMailSender
    {
        // Here is the Name of the Mail type
        public string Name => "Welcome Mail";

        /// <summary>
		/// Send Welcome mail
		/// </summary>
		/// <returns></returns>
        public bool Send()
        {
            try
            {
                //List all customers
                var customers = DataLayer.ListCustomers();
                // Get directly new Customers to reduce complexity of loop and number of exceutions.
                var newCustomers = customers
                    .Where(c => c.CreatedDateTime > DateTime.Now.AddDays(-1))
                    .ToList();

                //loop through list of new customers
                foreach (var customer in newCustomers)
                {
#if DEBUG
                    //Don't send mails in debug mode, just write the emails in console
                    Console.WriteLine($"[DEBUG] Would send welcome email to: {customer.Email}");
#else
                    // Created MailMessage with all mail components
                    var mail = new MailMessage
                    {
                        From = new MailAddress("info@EO.com"),
                        Subject = "Welcome as a new customer at EO!",
                        Body = $"Hi {customer.Email} <br>We would like to welcome you as customer on our site!<br><br>Best Regards,<br>EO Team",
                        IsBodyHtml = true
                    };

                    // Add customer as recipient
                    mail.To.Add(customer.Email);
                    // Create new smtp client
                    var smtp = new SmtpClient("SMTP_HOST");
                    // send mail
                    smtp.Send(mail);
#endif
                }
                //All mails are sent! Success!
                return true;
            }
            catch (Exception ex)
            {
                //Something went wrong :(
                Console.WriteLine($"[ERROR] {Name} failed: {ex.Message}");
                return false;
            }
        }
    }
}
