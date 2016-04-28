using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Trade_MVC6.Services.EmailSender
{
    public class EmailSenderOptions
    {
        public string SmtpServerUrl { get; set; }
        public int SmtpServerPort { get; set; }
        public string SmtpRobotLogin { get; set; }
        public string SmtpRobotPass { get; set; }
        public string SmtpAdminTargetEmail { get; set; }
        }
}
