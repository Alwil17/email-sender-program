using System;
using System.Collections.Generic;
using EmailSenderProgram.Mail;

namespace EmailSenderProgram
{
	internal class Program
	{
		/// <summary>
		/// This application is run everyday
		/// </summary>
		/// <param name="args"></param>
		private static void Main(string[] args)
		{
			// List all mail senders we have created.
            var mailers = new List<IMailSender>
			{
				new WelcomeMailSender(),
				new ComebackMailSender("EOComebackToUs")
			};
			// Assuming all mailq will be sent
            bool allSuccess = true;

            // loop through list of MailSenders
            foreach (var mailer in mailers)
            {
				// Prepare to send mail
                Console.WriteLine($"Sending: {mailer.Name}");
				// Send mail and get the result
                bool success = mailer.Send();
				// change state of allSuccess
                allSuccess &= success;
            }

            if (allSuccess)
            {
                //Check if the sending went OK
                Console.WriteLine("All mails sent successfully.");
            }
            else
            {
                // Check if the sending was not going well...
                Console.WriteLine("Some mails failed to send.");
            }

            Console.ReadKey();
		}
	}
}