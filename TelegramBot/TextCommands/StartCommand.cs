using System.Threading.Tasks;
using Telegram.Bot.Types;
using TelegramBot.Services;

namespace TelegramBot.TextCommands
{
  public class StartCommand: ITextCommand
  {
    private readonly Message _message;
    private readonly IBotService _botService;
    public StartCommand(IBotService botService, Message message)
    {
      _message = message;
      _botService = botService;
    }

    public async Task ProcessMessage()
    {
      await _botService.Client.SendTextMessageAsync(_message.Chat.Id, "Welcome! Type /help to show more info.");
    }
  }
}
