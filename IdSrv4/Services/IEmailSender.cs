using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdSrv4.Services
{
    public interface IEmailSender
    {
        Task<object> SendEmailAsync(string email, string subject, string message, string name);
    }
}
