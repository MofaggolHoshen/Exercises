using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Configuration;

namespace LogInTest.Services
{
    public class EmailService : IEmailSender
    {
        private IConfiguration _configuration;

        public EmailService(IConfiguration configuration)
        {
            this._configuration = configuration;
        }
        public async Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            var emailMessage = new MailMessage();
            emailMessage.From = new MailAddress(_configuration["Smtp:SenderEmail"]);
            emailMessage.To.Add(email);
            emailMessage.Subject = subject;
            emailMessage.Body = htmlMessage;
            emailMessage.IsBodyHtml = true;

            await getSmtpClient().SendMailAsync(emailMessage);

        }

        private SmtpClient getSmtpClient()
        {
            SmtpClient smtpClient = new SmtpClient();
            if (!string.IsNullOrWhiteSpace(_configuration["Smtp:UserName"]) && !string.IsNullOrWhiteSpace(_configuration["Smtp:Password"]))
            {
                var credential = new NetworkCredential
                {
                    UserName = _configuration["Smtp:UserName"],
                    Password = _configuration["Smtp:Password"]
                };

                smtpClient.Credentials = credential;
            }

            smtpClient.Host = _configuration["Smtp:Host"];

            int port = int.Parse(_configuration["Smtp:Port"]);

            if (port > 0)
            {
                smtpClient.Port = port;
            }

            smtpClient.EnableSsl = bool.Parse(_configuration["Smtp:IsEmailSendEnable"]);

            return smtpClient;
        }

    }
}
