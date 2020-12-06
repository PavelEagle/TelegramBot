using System;
using Microsoft.Extensions.DependencyInjection;
using Quartz;
using Quartz.Spi;

namespace TelegramBot.Quartz
{
  public class JobFactory: IJobFactory
  {
    private readonly IServiceScopeFactory _provider;

    public JobFactory(IServiceScopeFactory provider)
    {
      _provider = provider;
    }

    public IJob NewJob(TriggerFiredBundle bundle, IScheduler scheduler)
    {
      return new JobWrapper(_provider, bundle.JobDetail.JobType);
    }

    public void ReturnJob(IJob job)
    {
      (job as IDisposable)?.Dispose();
    }
  }
}
