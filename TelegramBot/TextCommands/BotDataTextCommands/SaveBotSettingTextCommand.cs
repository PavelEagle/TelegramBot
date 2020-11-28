using System.Threading.Tasks;
using Telegram.Bot.Types;
using TelegramBot.BotDialogData;
using TelegramBot.Services;

namespace TelegramBot.TextCommands
{
  public class SaveBotSettingTextCommand : ITextCommand
  {
    private readonly IBotService _botService;
    public SaveBotSettingTextCommand(IBotService botService)
    {
      _botService = botService;
    }

    public async Task ProcessMessage(Message message)
    {
      var path = "BotData/answers-data.txt";

      await DataService.SaveData(DialogBotData.QuestionsData, path);

      await _botService.Client.SendTextMessageAsync(message.Chat.Id, "Settings have been saved");
    }
  }
}
