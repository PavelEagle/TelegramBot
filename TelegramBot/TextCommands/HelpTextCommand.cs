using System.Threading.Tasks;
using Telegram.Bot.Types;
using TelegramBot.Common;
using TelegramBot.Services;

namespace TelegramBot.TextCommands
{
  public class HelpTextCommand: ITextCommand
  {
    private readonly Message _message;
    private readonly IBotService _botService;
    public HelpTextCommand(IBotService botService, Message message)
    {
      _message = message;
      _botService = botService;
    }
    public async Task ProcessMessage()
    {
      await _botService.Client.SendTextMessageAsync(_message.Chat.Id, "Make your choice: ", replyMarkup: KeyboardBuilder.CreateHelpMenu());
    }
  }
}
