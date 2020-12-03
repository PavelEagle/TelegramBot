using System.Linq;
using System.Threading.Tasks;
using Telegram.Bot.Types;
using TelegramBot.BotDialogData;
using TelegramBot.Common;
using TelegramBot.Services;

namespace TelegramBot.TextCommands
{
  public class SetSecretAccessTextCommand : ITextCommand
  {
    private readonly IBotService _botService;
    public SetSecretAccessTextCommand(IBotService botService)
    {
      _botService = botService;
    }
    public async Task ProcessMessage(Message message)
    {
      var currentSettings = ChatSettings.ChatSettingsData.Where(x => x.ChatId == message.Chat.Id).Single();

      if (currentSettings.AccountName == "PavelEagle")
      {
        var Adminkeyboard = KeyboardBuilder.CreateAdminHelpMenu();
        await _botService.Client.SendTextMessageAsync(message.Chat.Id, "Test", replyMarkup: Adminkeyboard);
      }

      var keyboard = KeyboardBuilder.CreateHelpMenu();
      await _botService.Client.SendTextMessageAsync(message.Chat.Id, "Denied", replyMarkup: keyboard);
    }
  }
}
