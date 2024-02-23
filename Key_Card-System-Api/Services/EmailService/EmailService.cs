using System.Diagnostics;
using System.Net;
using System.Net.Mail;

namespace Key_Card_System_Api.Services.EmailService
{
    public class EmailService : IEmailService
    {
        private readonly string _smtpServer;
        private readonly int _smtpPort;
        private readonly string _smtpUsername;
        private readonly string _smtpPassword;

        public EmailService(string smtpServer, int smtpPort, string smtpUsername, string smtpPassword)
        {
            _smtpServer = smtpServer;
            _smtpPort = smtpPort;
            _smtpUsername = smtpUsername;
            _smtpPassword = smtpPassword;
        }

        public async Task SendEmailAsync(string toEmail, string subject, string body)
        {
            Debug.WriteLine($"Sending email to {toEmail} with subject {subject}");
            using var client = new SmtpClient(_smtpServer, _smtpPort);
            client.UseDefaultCredentials = false;
            client.Credentials = new NetworkCredential(_smtpUsername, _smtpPassword);
            client.EnableSsl = true;

            var htmlBody = $@"
                <html>
                <head>
                <style>
                /* Define your CSS styles here */
                body {{
                    font-family: Arial, sans-serif;
                    background-color: #f4f4f4;
                }}
                .container {{
                    max-width: 600px;
                    margin: 0 auto;
                    padding: 20px;
                    background-color: #fff;
                    border-radius: 5px;
                    box-shadow: 0 2px 5px rgba(0, 0, 0, 0.1);
                }}
                h1 {{
                    color: #333;
                }}
                p {{
                    color: #666;
                }}
            </style>
            </head>
            <body>
            <div class='container'>
            <h1>{subject}</h1>
            <p>{body}</p>
            </div>
            </body>
            </html>";

            var message = new MailMessage
            {
                From = new MailAddress(_smtpUsername),
                Subject = subject,
                Body = htmlBody,
                IsBodyHtml = true
            };
            message.To.Add(toEmail);

            try
            {
                await client.SendMailAsync(message);
            }
            catch (Exception ex)
            {
                throw new ApplicationException($"Failed to send email: {ex.Message}");
            }
        }
    }
}
