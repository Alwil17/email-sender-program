using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmailSenderProgram.Mail
{
    /// <summary>
    /// Define methods and property that all mail type should use.
    /// </summary>
    public interface IMailSender
    {
        // The name of the Mail type (Welcome, Comeback or anything else)
        string Name { get; }

        // Need to send a mail in implementation.
        bool Send();
    }
}
