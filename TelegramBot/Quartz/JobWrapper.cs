using System;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Quartz;

namespace TelegramBot.Quartz
{
    public class JobWrapper : IJob, IDisposable
    {
        private readonly IServiceScope _serviceScope;
        private readonly IJob _job;

        public JobWrapper(IServiceScopeFactory serviceScopeFactory, Type jobType)
        {
            _serviceScope = serviceScopeFactory.CreateScope();
            _job = ActivatorUtilities.CreateInstance(_serviceScope.ServiceProvider, jobType) as IJob;
        }

        public Task Execute(IJobExecutionContext context)
        {
            return _job.Execute(context);
        }

        public void Dispose()
        {
            _serviceScope.Dispose();
        }
    }
}