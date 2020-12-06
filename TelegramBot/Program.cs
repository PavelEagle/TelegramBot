using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using TelegramBot.Common;
using TelegramBot.Quartz;

namespace TelegramBot
{
  public class Program
  {
    public static async Task Main(string[] args)
    {
      await DataService.LoadAppData();

      var host = CreateHostBuilder(args).Build();

      using (var scope = host.Services.CreateScope())
      {
        var serviceProvider = scope.ServiceProvider;
        SaveScheduler.Start(serviceProvider);
      }

      host.Run();
    }

    public static IHostBuilder CreateHostBuilder(string[] args) =>
      Host.CreateDefaultBuilder(args)
        .ConfigureWebHostDefaults(webBuilder => { webBuilder.UseStartup<Startup>(); });
  }
}
