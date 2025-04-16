using System;
using System.Collections.Generic;
using EmailSenderProgram.Mail;
using EmailSenderProgram.Scheduler;

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
			// Since mails are now scheduled, moved all logic to Scheduler.
            Console.WriteLine("== Email Sender Program ==");
			// Run Scheduler
            EmailScheduler.Run();
			// End of program.
            Console.WriteLine("== Finished ==");

            Console.ReadKey();
		}
	}
}