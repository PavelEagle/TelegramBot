using System.Threading.Tasks;
using Telegram.Bot.Types;
using TelegramBot.Common;
using TelegramBot.Services;

namespace TelegramBot.TextCommands
{
  public class HelpTextCommand: ITextCommand
  {
    private readonly IBotService _botService;
    public HelpTextCommand(IBotService botService)
    {
      _botService = botService;
    }
    public async Task ProcessMessage(Message message)
    {
      await _botService.Client.SendTextMessageAsync(message.Chat.Id, "Make your choice: ", replyMarkup: KeyboardBuilder.CreateHelpMenu());
    }
  }
}
