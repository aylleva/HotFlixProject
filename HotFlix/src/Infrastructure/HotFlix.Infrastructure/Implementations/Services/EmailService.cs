using HotFlix.Application.Abstraction.Services;
using HotFlix.Domain.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace HotFlix.Infrastructure.Implementations.Services
{
    internal class EmailService : IEmailService
    {
        private readonly IConfiguration _configuration;

        public EmailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public void SendEmail(AppUser user, string randomcode)
        {
           
            SmtpClient smpt= new SmtpClient() {
                Host = _configuration["Email:Host"],
                Port = 587,
                EnableSsl = true,
                Credentials=new NetworkCredential(_configuration["Email:LoginEmail"], _configuration["Email:Password"])
            
            };
            MailAddress from = new MailAddress(_configuration["Email:LoginEmail"], "HotFlix");
            MailAddress to = new MailAddress(user.Email);

            MailMessage message = new MailMessage(from, to) { 
            Subject="Verification Code",
            Body=$"This is your Verification Code:{randomcode}"
            };

            smpt.Send(message);
        }

        public async Task SendEmailAsync(string email, string subject, string body, bool isHTML)
        {
            SmtpClient smtp = new SmtpClient(_configuration["Email:Host"],587);
            smtp.EnableSsl = true;
            smtp.Credentials = new NetworkCredential(_configuration["Email:LoginEmail"], _configuration["Email:Password"]);

            MailAddress from = new MailAddress(_configuration["Email:LoginEmail"], "HotFlix");
            MailAddress to = new MailAddress(email);

            MailMessage message = new MailMessage(from, to);
            message.Subject = subject;
            message.IsBodyHtml = isHTML;
            message.Body = body;

            await smtp.SendMailAsync(message);
        }
    }
}
