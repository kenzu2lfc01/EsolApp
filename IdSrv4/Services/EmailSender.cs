using IdSrv4.Models;
using IdSrv4.Services.ViewRender;
using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Options;
using MimeKit;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdSrv4.Services
{
    public class EmailSender : IEmailSender
    {
        private readonly EmailSettings _emailSettings;
        private readonly IHostingEnvironment _env;
        private readonly IViewRenderService _viewRender;

        public EmailSender(
            IOptions<EmailSettings> emailSettings,
            IViewRenderService viewRender,
            IHostingEnvironment env)
        {
            _emailSettings = emailSettings.Value;
            _env = env;
            _viewRender = viewRender;
        }
        public async Task<object> SendEmailAsync(string email, string subject, string message,string name)
        {
            try
            {
                var apiKey = Environment.GetEnvironmentVariable("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");
                var client = new SendGridClient(apiKey);  
                var from = new EmailAddress("nkoxken65@gmail.com", "Esol Nguyen");
                var to = new EmailAddress(email, name);
                var plainTextContent = "";
                var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, message);
                return await client.SendEmailAsync(msg);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(ex.Message);
            }
        }
    }
}
