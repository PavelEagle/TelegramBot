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
      var path = "BotData/chat-settings.txt";

      await DataService.SaveData(ChatSettings.ChatSettingsData, path);

      await _botService.Client.SendTextMessageAsync(message.Chat.Id, "Settings have been saved");
    }
  }
}
