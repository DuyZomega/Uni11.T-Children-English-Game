using CEG_BAL.Services.Interfaces;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Configuration;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
// using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using static Org.BouncyCastle.Math.EC.ECCurve;

namespace CEG_BAL.Services.Implements
{
    public class EmailService : IEmailService
    {
        /// <summary>
        /// Gửi Email
        /// </summary>
        /// <param name="_fromEmail">Địa chỉ email gửi</param>
        /// <param name="_toEmail">Địa chỉ email nhận</param>
        /// <param name="_subject">Chủ đề của email</param>
        /// <param name="_body">Nội dung (hỗ trợ HTML) của email</param>
        /// <param name="_stmpUser"></param>
        /// <returns>Task<bool> với giá trị trả về là true khi gửi thành công, false khi thất bại</returns>
        public async Task<bool> SendEmailAsync(
            string _fromSenderName,
            string _fromEmail,
            string _toReceiverName,
            string _toEmail, 
            string _subject, 
            string _body,
            IConfiguration _config,
            string _stmpUser,
            string? _stmpAppPassword = "lyqzvolmaiwrtojt"
            )
        {
            // Tạo nội dung Email
            try
            {
                // Create the email message SenderName
                var email = new MimeMessage();
                email.From.Add(new MailboxAddress(_fromSenderName ?? _config.GetSection("Gmail:SenderName").Value, _fromEmail));
                email.To.Add(new MailboxAddress(_toReceiverName ?? "Your Name", _toEmail));
                email.Subject = _subject;

                // Set the body of the email
                var bodyBuilder = new BodyBuilder
                {
                    HtmlBody = _body, // HTML email content
                    TextBody = "Plain text version of the email." // Fallback plain text content
                };
                email.Body = bodyBuilder.ToMessageBody();

                // Configure and connect to the SMTP server
                using (var smtpClient = new SmtpClient())
                {
                    await smtpClient.ConnectAsync(
                        _config.GetSection("Gmail:Host").Value ?? "smtp.gmail.com", 
                        int.Parse(_config.GetSection("Gmail:Port").Value),
                        Boolean.Parse(_config.GetSection("Gmail:SMTP:starttls:enable").Value) ?
                        MailKit.Security.SecureSocketOptions.StartTls : MailKit.Security.SecureSocketOptions.None
                    );

                    // Authenticate with the SMTP server
                    await smtpClient.AuthenticateAsync(_fromEmail, _stmpAppPassword);

                    // Send the email
                    await smtpClient.SendAsync(email);

                    // Disconnect from the server
                    await smtpClient.DisconnectAsync(true);

                    return true;
                }
            }
            catch (Exception ex)
            {
                // Log the error for debugging (replace with a proper logging mechanism in production)
                Console.WriteLine($"\nError sending email: {ex.Message}\n");
                return false;
            }
        }
        /// <summary>
        /// Gửi email sử dụng máy chủ SMTP Google (smtp.gmail.com)
        /// </summary>
        /// <param name="_from">Địa chỉ email gửi</param>
        /// <param name="_to">Địa chỉ email nhận</param>
        /// <param name="_subject">Chủ đề của email</param>
        /// <param name="_body">Nội dung (hỗ trợ HTML) của email</param>
        /// <param name="_gmailsend">Username hoặc Địa chỉ Email của tài khoản người gửi</param>
        /// <param name="_gmailpassword">Password của tài khoản người gửi, để null do đã có app password</param>
        /// <returns>Task</returns>
        public async Task<bool> SendEmailGoogleSmtpAsync(
            string _from, 
            string _to, 
            string _subject, 
            string _body, 
            string _gmailsend, 
            string? _gmailpassword = "lyqzvolmaiwrtojt"
            )
        {
            /*using (MailMessage message = new MailMessage(_from, _to, _subject, _body)
            {
                BodyEncoding = Encoding.UTF8,
                SubjectEncoding = Encoding.UTF8,
                IsBodyHtml = true
            })
            {
                message.ReplyToList.Add(new MailAddress(_from));
                message.Sender = new MailAddress(_from);

                // Tạo SmtpClient kết nối đến smtp.gmail.com
                using (SmtpClient client = new("smtp.gmail.com", 587))
                {
                    client.Credentials = new NetworkCredential(_gmailsend, _gmailpassword);
                    client.EnableSsl = true;
                    *//*return await SendEmailAsync(_from, _to, _subject, _body, client);*//*
                    try
                    {
                        await client.SendMailAsync(message);
                        return true;
                    }
                    catch (Exception ex)
                    {
                        // Log or handle exception as needed
                        Console.WriteLine($"Email sending failed: {ex.Message}");
                        return false;
                    }
                }
            }*/
            return false;
        }
        /// <summary>
        /// Gửi Email sử dụng máy chủ SMTP cài đặt localhost
        /// </summary>
        public async Task<bool> SendEmailLocalSmtp(string _from, string _to, string _subject, string _body)
        {
            /*using (SmtpClient client = new SmtpClient("localhost"))
            {
                return await SendEmailAsync(_from, _to, _subject, _body, client);
            }*/
            return false;
        }
    }
}
