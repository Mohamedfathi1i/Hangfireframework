using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Hangfirev1.Shared.EmailService
{
    public static class SendEmail
    {
        public static void SendEmailNotification(Exception ex)
        {
            // Add your email sending logic here
            try
            {
                SmtpClient client = new SmtpClient("your-smtp-server.com")
                {
                    Port = 587,
                    Credentials = new System.Net.NetworkCredential("your-email@example.com", "your-email-password"),
                    EnableSsl = true,
                };

                MailMessage mail = new MailMessage("your-email@example.com", "recipient@example.com")
                {
                    Subject = "Job Failed",
                    Body = "Job execution failed. Exception: " + ex.Message,
                    IsBodyHtml = false,
                };

                client.Send(mail);
            }
            catch (Exception emailEx)
            {
                Console.WriteLine($"Email sending failed: {emailEx.Message}");
            }
        }
    }
}
