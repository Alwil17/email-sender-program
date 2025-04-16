using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmailSenderProgram.Mail;

namespace EmailSenderProgram.Scheduler
{
    public static class Scheduler
    {
        public static void Run()
        {
            // Create empty List for all mail senders we need to schedule.
            var senders = new List<IMailSender>();

            // Always send welcome mail
            senders.Add(new WelcomeMailSender());

#if DEBUG
            // Debug mode, always send Comeback mail
            Console.WriteLine("[DEBUG] Sending Comeback Mail (forced)");
            senders.Add(new ComebackMailSender("EOComebackToUs"));
#else
            // In production, send comeback mail only on Sunday according to Comments in old Program.cs
            if (DateTime.Now.DayOfWeek.Equals(DayOfWeek.Sunday))
            {
                Console.WriteLine("Today is Sunday - Sending Comeback Mail");
                senders.Add(new ComebackMailSender("EOComebackToUs"));
            }
#endif
            // loop through all senders.
            // No need to check size of List since WelcomeMail is always scheduled.
            foreach (var sender in senders)
            {
                Console.WriteLine($"Sending: {sender.Name}");
                // send mail
                var success = sender.Send();

                if (success)
                    // Check if mail was successfully sent
                    Console.WriteLine($"{sender.Name} sent successfully.");
                else
                    // or not.
                    Console.WriteLine($"{sender.Name} failed to send.");
            }
        }
    }
}
