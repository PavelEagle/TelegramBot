using System.Threading.Tasks;
using Telegram.Bot.Types;
using TelegramBot.Commands;
using TelegramBot.Services;

namespace TelegramBot.MessageTypes
{
  public class TextMessageService: IMessageTypeService
  {
    private readonly ITextCommand _command;

    private TextMessageService(ITextCommand command)
    {
      _command = command;
    }

    public static TextMessageService Create(IBotService botService, Message message)
    {
      return message.Text switch
      {
        CommandList.Start => new TextMessageService(new StartCommand(botService, message)),
        CommandList.Weather => new TextMessageService(new WeatherCommand(botService, message)),
        _ => new TextMessageService(new DefaultTextCommand(botService, message))
      };
    }

    public async Task ProcessMessage()
    {
      await _command.ProcessMessage();
    }
  }
}
