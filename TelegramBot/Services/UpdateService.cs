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
      if (update.Type != UpdateType.Message && update.Type != UpdateType.CallbackQuery)
        return;

      if (update.Type == UpdateType.CallbackQuery)
      {
        _logger.LogInformation("Received Callback from {0}", update.CallbackQuery.Message.Chat.Id);
        _messageType = TextMessageService.Create(_botService, update.CallbackQuery);
      }
      else
      {
        var message = update.Message;
        _logger.LogInformation("Received Message from {0}", message.Chat.Id);

        _messageType = message.Type switch
        {
          MessageType.Text => TextMessageService.Create(_botService, message),
          MessageType.Photo => new PhotoMessageService(_botService, message),
          _ => new UnknownTypeService(_botService, message)
        };
      }
      
      await _messageType.ProcessMessage();
    }
  }
}
