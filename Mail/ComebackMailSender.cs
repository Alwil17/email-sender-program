using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace EmailSenderProgram.Mail
{
    public class ComebackMailSender : IMailSender
    {
        // Add vourcher code, to invite customer for coming again.
        private readonly string _voucherCode;

        // Here is the Name of the Mail type
        public string Name => "Comeback Mail";

        /// <summary>
		/// Constructor of ComebackMailSender
		/// </summary>
        public ComebackMailSender(string voucherCode)
        {
            _voucherCode = voucherCode;
        }

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
                // List all orders
                var orders = DataLayer.ListOrders();
                // Get directly Customers without orders to reduce complexity of loop and number of exceutions.
                var customersWithoutOrders = customers
                    .Where(c => !orders.Any(o => o.CustomerEmail == c.Email))
                    .ToList();

                //loop through list of Customers without orders
                foreach (var customer in customersWithoutOrders)
                {
#if DEBUG
                    //Don't send mails in debug mode, just write the emails in console
                    Console.WriteLine($"[DEBUG] Would send comeback email to: {customer.Email}");
#else
                    // Created MailMessage with all mail components
                    var mail = new MailMessage
                    {
                        From = new MailAddress("info@EO.com"),
                        Subject = "We miss you as a customer",
                        Body = $@"Hi {customer.Email} br>We miss you as a customer. Our shop is filled with nice products. Here is a voucher that gives you 50 kr to shop for. 
						         <br>Voucher: {_voucherCode}
						         <br><br>Best Regards,<br>EO Team",
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
}
