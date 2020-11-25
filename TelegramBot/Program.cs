using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using System.IO;
using System.Threading.Tasks;
using TelegramBot.BotDialogData;

namespace TelegramBot
{
  public class Program
  {
    public async static Task Main(string[] args)
    {
      //File.WriteAllBytes("BotData/questions-data.txt", DataSerializer.Serialize(test));
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
      var answersDataFilePath = "BotData/answers-data.txt";
      var answersData = await File.ReadAllBytesAsync(answersDataFilePath);
      DialogBotData.AnswerData = DataSerializer.Deserialize<AnswersData>(answersData);

      var questionsDataFilePath = "BotData/questions-data.txt";
      var questionsData = await File.ReadAllBytesAsync(questionsDataFilePath);
      DialogBotData.QuestionsData = DataSerializer.Deserialize<QuestionsData>(questionsData);

      var chatSettingDataFilePath = "BotData/chat-setting.txt";
      var chatSettingData = await File.ReadAllBytesAsync(chatSettingDataFilePath);
      ChatSettings.ChatSettingsData = DataSerializer.Deserialize<ChatSettingsBotData>(chatSettingData);
    }
  }
}
