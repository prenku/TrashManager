using Hangfire;
using System;

namespace TrashManager
{
    public class Hangfire
    {
        public Hangfire()
        {

        }
        public static void InitializeJobs()
        {
            RecurringJob.AddOrUpdate<Workers.SendNotificationsJob>(job => job.Execute(),
                Cron.MinuteInterval(5));
        }
    }
}
