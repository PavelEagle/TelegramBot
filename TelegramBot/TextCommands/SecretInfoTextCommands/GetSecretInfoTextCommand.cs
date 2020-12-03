using System.Linq;
using System.Threading.Tasks;
using Telegram.Bot.Types;
using TelegramBot.BotDialogData;
using TelegramBot.Common;
using TelegramBot.Services;

namespace TelegramBot.TextCommands
{
  public class GetSecretInfoTextCommand : ITextCommand
  {
    private readonly IBotService _botService;
    public GetSecretInfoTextCommand(IBotService botService)
    {
      _botService = botService;
    }
    public async Task ProcessMessage(Message message)
    {
      var currentSettings = ChatSettings.ChatSettingsData.Where(x => x.ChatId == message.Chat.Id).Single();

      if (currentSettings.SecretInfoAccess)
      {
        var keyboardWithSecretAccess = KeyboardBuilder.CreateHelpMenuWithSecretAccess();
        await _botService.Client.SendTextMessageAsync(message.Chat.Id, "Test", replyMarkup: keyboardWithSecretAccess);
      }

      var keyboard = KeyboardBuilder.CreateHelpMenu();
      await _botService.Client.SendTextMessageAsync(message.Chat.Id, "Denied ", replyMarkup: keyboard);
    }
  }
}
