using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using EmailSenderProgram.Services;

namespace EmailSenderProgram.Mail
{
    public class ComebackMailSender : IMailSender
    {
        // Add vourcher code, to invite customer for coming again.
        private readonly string _voucherCode;

        // Here is the Name of the Mail type
        public string Name => "ComebackMail";

        /// <summary>
        /// Constructor of ComebackMailSender
        /// </summary>
        /// <param name="voucherCode">Voucher code used in email body</param>
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
                // call MailService to sendEmail
                customersWithoutOrders.ForEach(customer => MailService.SendEmail(
                        customer.Email,
                        "We miss you as a customer",
                        $@"Hi {customer.Email} <br>We miss you as a customer. Our shop is filled with nice products. Here is a voucher that gives you 50 kr to shop for. 
						    <br>Voucher: {_voucherCode}
						    <br><br>Best Regards,<br>EO Team"
                ));

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

