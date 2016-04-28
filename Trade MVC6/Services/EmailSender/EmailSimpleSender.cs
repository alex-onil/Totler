using System;
using System.Diagnostics.Contracts;
using MailKit;
using MailKit.Security;
using MailKit.Net.Smtp;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.OptionsModel;
using MimeKit;
using TotlerCore.BLL.Interfaces;

namespace Trade_MVC6.Services.EmailSender
    {
    public class EmailSimpleSender : IEmailSender
    {
        private readonly EmailSenderOptions _config;

        public EmailSimpleSender(IOptions<EmailSenderOptions> optionsAccessor)
            {
            Contract.Ensures(!string.IsNullOrEmpty(optionsAccessor.Value.SmtpServerUrl) &&
                             !string.IsNullOrEmpty(optionsAccessor.Value.SmtpRobotLogin) &&
                             !string.IsNullOrEmpty(optionsAccessor.Value.SmtpRobotPass) &&
                             !string.IsNullOrEmpty(optionsAccessor.Value.SmtpAdminTargetEmail) &&
                             optionsAccessor.Value.SmtpServerPort != default(int), "Ошибка конфигурации SMTP робота.");

            _config = optionsAccessor.Value;
            }
        public Task SendEmailAsync(string email, string subject, string message)
            {
            var mimeMessage = new MimeMessage();
            mimeMessage.From.Add(new MailboxAddress("Totler Robot Mail", _config.SmtpRobotLogin));
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
                    client.Connect(_config.SmtpServerUrl, _config.SmtpServerPort, SecureSocketOptions.Auto);

                    client.Authenticate(_config.SmtpRobotLogin, _config.SmtpRobotPass);

                    client.Send(mimeMessage);

                    client.Disconnect(true);
                    }
            });
            }
        }
    }
