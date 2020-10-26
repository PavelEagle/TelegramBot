using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using TelegramBot.MessageTypes;

namespace TelegramBot.Services
{
  public class UpdateService : IUpdateService
  {
    private readonly IBotService _botService;
    private readonly ILogger<UpdateService> _logger;
    private IMessageService _messageType;

    public UpdateService(IBotService botService, ILogger<UpdateService> logger)
    {
      _botService = botService;
      _logger = logger;
    }

    public async Task EchoAsync(Update update)
    {
      if (update.Type != UpdateType.Message)
        return;

      var message = update.Message;

      _logger.LogInformation("Received Message from {0}", message.Chat.Id);

      switch (message.Type)
      {
        case MessageType.Text:
          _messageType = TextMessageService.Create(_botService, message);
          break;

        case MessageType.Photo:
          _messageType = new PhotoMessageService(_botService, message);
          break;

        default:
          _messageType = new UnknownTypeService(_botService, message);
          break;

      }
      await _messageType.ProcessMessage();
    }
  }
}
