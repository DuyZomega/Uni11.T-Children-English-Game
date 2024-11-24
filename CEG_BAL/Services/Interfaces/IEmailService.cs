using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace CEG_BAL.Services.Interfaces
{
    public interface IEmailService
    {
        Task<bool> SendEmailAsync(
            string _fromSenderName,
            string _fromEmail,
            string _toReceiverName,
            string _toEmail, 
            string _subject, 
            string _body, 
            IConfiguration _config,
            string _stmpUser,
            string? _stmpAppPassword
        );
        Task<bool> SendEmailLocalSmtp(string _from, string _to, string _subject, string _body);
        Task<bool> SendEmailGoogleSmtpAsync(string _from, string _to, string _subject, string _body, string _gmailsend, string? _gmailpassword);
    }
}
