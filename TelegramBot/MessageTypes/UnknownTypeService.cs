using System.Threading.Tasks;
using Telegram.Bot.Types;
using TelegramBot.Services;

namespace TelegramBot.MessageTypes
{
  public class UnknownTypeService: IMessageTypeService
  {
    private readonly IBotService _botService;
    private readonly Message _message;
    public UnknownTypeService(IBotService botService, Message message)
    {
      _botService = botService;
      _message = message;
    }
    public async Task ProcessMessage()
    {
      await _botService.Client.SendTextMessageAsync(_message.Chat.Id, "Dude, i don't know how do this");
    }
  }
}
