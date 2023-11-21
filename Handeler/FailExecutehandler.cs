using Hangfire;
using Hangfirev1.Shared.EmailService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hangfirev1.Handeler
{
    public static class FailExecutehandler
    {
        public static void ExecuteWithRetry(Action job)
        {
            try
            {
                job();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Job failed with exception: {ex.Message}");
                SendEmail.SendEmailNotification(ex);

                // Schedule a retry after 30 minutes
                BackgroundJob.Schedule(() => job(), TimeSpan.FromMinutes(30));
            }
        }

    }
}
