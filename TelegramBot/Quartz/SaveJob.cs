using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Quartz;
using TelegramBot.Common;

namespace TelegramBot.Quartz
{
  public class SaveJob: IJob
  {
    private readonly ILogger<SaveJob> _logger;
    public SaveJob(ILogger<SaveJob> logger)
    {
      _logger = logger;
    }
    public async Task Execute(IJobExecutionContext context)
    {
      await DataService.SaveChatSettings();
      _logger.LogInformation("Data has been saved successfully!");
    }
  }
}
