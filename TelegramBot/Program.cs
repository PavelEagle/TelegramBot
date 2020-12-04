using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using TelegramBot.BotDialogData;

namespace TelegramBot
{
  public class Program
  {
    public async static Task Main(string[] args)
    {
      await AppInit();
      CreateHostBuilder(args).Build().Run();
    }

    public static IHostBuilder CreateHostBuilder(string[] args) =>
      Host.CreateDefaultBuilder(args)
        .ConfigureWebHostDefaults(webBuilder => { webBuilder.UseStartup<Startup>(); });

    private static async Task AppInit()
    {
      //  var answersDataFilePath = "BotData/answers-data.txt";
      //  DialogBotData.AnswerData = await DataService.LodaData<ConcurrentBag<AnswersData>>(answersDataFilePath);

      //  var questionsDataFilePath = "BotData/questions-data.txt";
      //  DialogBotData.QuestionsData = await DataService.LodaData<ConcurrentBag<QuestionsData>>(questionsDataFilePath);

      //  if (DialogBotData.QuestionsData!= null)
      //    DialogBotData.Count = DialogBotData.QuestionsData.Count;

      //  var chatSettingsFilePath = "BotData/chat-settings.txt";
      //  ChatSettings.ChatSettingsData = await DataService.LodaData<ConcurrentBag<ChatSettingsBotData>>(chatSettingsFilePath);
      //}
    }
  }
}
