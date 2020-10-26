using System.Threading.Tasks;
using Telegram.Bot.Types;
using TelegramBot.Commands;
using TelegramBot.Services;
using TelegramBot.TextCommands;

namespace TelegramBot.MessageTypes
{
  public class TextMessageService: IMessageService
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
        var str when str.StartsWith(TextCommandList.Start) => new TextMessageService(new StartCommand(botService, message)),
        var str when str.StartsWith(TextCommandList.Weather) => new TextMessageService(new WeatherCommand(botService, message)),
        var str when str.StartsWith(TextCommandList.News) => new TextMessageService(new NewsCommand(botService, message)),
        _ => new TextMessageService(new DefaultTextCommand(botService, message))
      };
    }

    public async Task ProcessMessage()
    {
      await _command.ProcessMessage();
    }
  }
}
