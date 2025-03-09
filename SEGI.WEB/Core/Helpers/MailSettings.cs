using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SEGI.Core.Helpers
{
    public class MailSettings
    {
        public string Email { get; set; }
        public string DisplayName { get; set; }
        public string Password { get; set; }
        public string Host { get; set; }
        public int Port { get; set; }
        public string Server { get; set; }
        public string SenderName { get; set; }
        public string UserName { get; set; }
        public bool UseSsl { get; set; }
        public bool UseStartTls { get; set; }
    }
}
