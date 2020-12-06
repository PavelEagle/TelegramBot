using System;
using Microsoft.Extensions.DependencyInjection;
using Quartz;
using Quartz.Impl;

namespace TelegramBot.Quartz
{
  public static class SaveScheduler
  {
    public static async void Start(IServiceProvider serviceProvider) 
    {
        var scheduler = await StdSchedulerFactory.GetDefaultScheduler();
        scheduler.JobFactory = serviceProvider.GetService<JobFactory>();
        await scheduler.Start();

        var job = JobBuilder.Create<SaveJob>()
          .WithIdentity("saveJob", "group1")
          .Build();

        // trigger the job to run now, and then repeat every 10 minutes
        var trigger = TriggerBuilder.Create()
          .WithIdentity("saveJobTrigger", "group1")
          .StartNow()
          .WithSimpleSchedule(x => x
            .WithIntervalInMinutes(10)
            .RepeatForever())
          .Build();

        await scheduler.ScheduleJob(job, trigger);
    }
  }
}
