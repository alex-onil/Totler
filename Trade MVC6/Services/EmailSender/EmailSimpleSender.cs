using System;
using System.Diagnostics.Contracts;
using MailKit;
using MailKit.Security;
using MailKit.Net.Smtp;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using MimeKit;

namespace Trade_MVC6.Services.EmailSender
    {
    public class EmailSimpleSender : IEmailSender
        {
        private readonly string _smtpServerUrl;
        private readonly int _smtpServerPort;
        private readonly string _smtpRobotLogin;
        private readonly string _smtpRobotPass;
        private readonly string _smtpAdminTargetEmail;


        public string AdminEmail => _smtpAdminTargetEmail;

        public EmailSimpleSender(IConfigurationRoot config)
            {
            _smtpServerUrl = config["SmtpRobot:SmtpServerUrl"];
            _smtpServerPort = int.Parse(config["SmtpRobot:SmtpServerPort"]);
            _smtpRobotLogin = config["SmtpRobot:SmtpRobotLogin"];
            _smtpRobotPass = config["SmtpRobot:SmtpRobotPass"];
            _smtpAdminTargetEmail = config["SmtpRobot:SmtpAdminTargetEmail"];
            Contract.Ensures(!string.IsNullOrEmpty(_smtpServerUrl) &&
                             !string.IsNullOrEmpty(_smtpRobotLogin) &&
                             !string.IsNullOrEmpty(_smtpRobotPass) &&
                             !string.IsNullOrEmpty(_smtpAdminTargetEmail) &&
                             _smtpServerPort != default(int), "Ошибка конфигурации SMTP робота.");
            }
        public Task SendEmailAsync(string email, string subject, string message)
            {
            //SmtpClient client = new SmtpClient(_smtpServerUrl, _smtpServerPort)
            //    {
            //        Timeout = 90000,
            //        Credentials = new NetworkCredential(_smtpRobotLogin, _smtpRobotPass)
            //    };

            //MailAddress from = new MailAddress(_smtpRobotLogin, "Totler Robot Mail", System.Text.Encoding.UTF8);
            //MailAddress to = new MailAddress(email);

            //MailMessage mailmessage = new MailMessage(from, to)
            //    {
            //    Body = message
            //    };

            //mailmessage.Body += message;
            //mailmessage.BodyEncoding = System.Text.Encoding.UTF8;

            //mailmessage.Subject = subject;
            //mailmessage.SubjectEncoding = System.Text.Encoding.UTF8;

            //return Task.Run(() => client.Send(mailmessage));

            var mimeMessage = new MimeMessage();
            mimeMessage.From.Add(new MailboxAddress("Totler Robot Mail", _smtpRobotLogin));
            mimeMessage.To.Add(new MailboxAddress("Recipient", email));
            mimeMessage.Subject = subject;


            mimeMessage.Body = new TextPart("html")
                {
                Text = message
                };

            return Task.Run(() =>
            {
                using (var client = new SmtpClient()) //new ProtocolLogger("smtp.log")
                    {
                    client.Connect(_smtpServerUrl, _smtpServerPort, SecureSocketOptions.Auto);

                    client.Authenticate(_smtpRobotLogin, _smtpRobotPass);

                    client.Send(mimeMessage);

                    client.Disconnect(true);
                    }
            });
            }
        }
    }
