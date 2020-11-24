using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using TelegramBot.BotDialogData;

namespace TelegramBot
{
  public class Program
  {
    public async static Task Main(string[] args)
    {
      var test = new List<DialogData>();
      //var test = new Dictionary<string, List<string>>();

      for (var i = 0; i < 100; i++)
      {
        test.Add(new DialogData("test" + i, new List<string>() { "test1 answer", "test2 answer" }));
      }

      File.WriteAllBytes("BotData/dialogdata.txt", DataSerializer.Serialize(test));
      await AppInit();
      CreateHostBuilder(args).Build().Run();
    }

    public static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .ConfigureWebHostDefaults(webBuilder =>
            {
              webBuilder.UseStartup<Startup>();
            });

    private static async Task AppInit()
    {
      var data = await File.ReadAllBytesAsync("BotData/dialogdata.txt");
      DialogBotData.DialogData = DataSerializer.Deserialize(data);
    }
  }
}
