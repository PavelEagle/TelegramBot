using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using System.Collections.Concurrent;
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
      var dialogDataFilePath = "BotData/dialog-data.txt";
      CurrentDialogBotData.DialogBotData = await DataService.LoadData<DialogBotData>(dialogDataFilePath);

      if (CurrentDialogBotData.DialogBotData.QuestionsData != null)
        CurrentDialogBotData.DialogBotData.Count = CurrentDialogBotData.DialogBotData.QuestionsData.Count;

      if (CurrentDialogBotData.DialogBotData.Count == 0)
      {
        CurrentDialogBotData.DialogBotData = new DialogBotData() { AnswerData = new ConcurrentDictionary<long, ConcurrentQueue<string>>(), QuestionsData = new ConcurrentDictionary<long, ConcurrentQueue<string>>()};
      }
      
      var chatSettingsFilePath = "BotData/chat-settings.txt";
      ChatSettings.ChatSettingsData = await DataService.LoadData<ConcurrentQueue<ChatSettingsBotData>>(chatSettingsFilePath);

      if (ChatSettings.ChatSettingsData == null)
      {
        ChatSettings.ChatSettingsData = new ConcurrentQueue<ChatSettingsBotData>();
      }
    }
  }
}
