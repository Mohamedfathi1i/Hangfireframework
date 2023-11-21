using Hangfire;
using Hangfirev1.Handeler;
using Hangfirev1.Jobs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace Hangfirev1
{
    public class Program
    {
        static void Main(string[] args)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["HangfireConnection"].ConnectionString;

            GlobalConfiguration.Configuration.UseSqlServerStorage(connectionString);


            RecurringJob.AddOrUpdate("SyncPerformely", () => SyncPerformelyJob(), Cron.MinuteInterval(1));

            // Schedule AdminSyncJob to run every 1 minute
            RecurringJob.AddOrUpdate("AdminSync", () => AdminSyncJob(), Cron.MinuteInterval(1));

            using (var server = new BackgroundJobServer())
            {
                Console.WriteLine("Hangfire Server started. Press any key to exit...");
                Console.ReadKey();
            }
        }

        public static void SyncPerformelyJob()
        {
            FailExecutehandler.ExecuteWithRetry(() => JobsService.SyncPerformely());
        }

        public static void AdminSyncJob()
        {
            FailExecutehandler.ExecuteWithRetry(() => JobsService.AdminSync());
        }
    }

    public class FailExecutehandler
    {
        public static void ExecuteWithRetry(Action action)
        {
            // Your retry logic here
            try
            {
                action.Invoke();
            }
            catch (Exception ex)
            {
                // Handle the exception or retry the action as needed
                Console.WriteLine("Error: " + ex.Message);
            }
        }
    }

    public class JobsService
    {
        public static void SyncPerformely()
        {
            // Your SyncPerformely logic here
            Console.WriteLine("SyncPerformely job is running...");
        }

        public static void AdminSync()
        {
            // Your AdminSync logic here
            Console.WriteLine("AdminSync job is running...");
        }
    }
}