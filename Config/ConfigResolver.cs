using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmailSenderProgram.Config
{
    /// <summary>
    /// Handles all params from App.config for more reusability in code.
    /// </summary>
    public static class ConfigResolver
    {
        public static string MailSender => ConfigurationManager.AppSettings["mail_sender"];
        public static string MailPassword => ConfigurationManager.AppSettings["mail_password"];
        public static string MailHost => ConfigurationManager.AppSettings["mail_host"];
        public static int MailPort => Convert.ToInt16(ConfigurationManager.AppSettings["mail_port"]);
        public static bool MailIsSSL => GetBool("mail_is_ssl", defaultValue: true);


        /// <summary>
        /// Parse received key value to bool.
        /// </summary>
        /// <param name="key"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        private static bool GetBool(string key, bool defaultValue)
        {
            var value = ConfigurationManager.AppSettings[key];
            return bool.TryParse(value, out var result) ? result : defaultValue;
        }

        /// <summary>
        /// Check is program instance is in debug mode or not.
        /// </summary>
        /// <returns></returns>
        public static bool IsDebug()
        {
#if DEBUG
            return true;
#else
            return false;
#endif
        }
    }
}
