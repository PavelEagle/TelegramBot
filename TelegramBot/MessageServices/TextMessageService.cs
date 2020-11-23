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
        var str when str.ToLower().StartsWith(TextCommandList.Start) => new TextMessageService(new StartCommand(botService, message)),
        var str when str.ToLower().StartsWith(TextCommandList.Help) => new TextMessageService(new HelpTextCommand(botService, message)),
        var str when str.ToLower().StartsWith(TextCommandList.Weather) => new TextMessageService(new WeatherCommand(botService, message)),
        var str when str.ToLower().StartsWith(TextCommandList.Wiki) => new TextMessageService(new WikiSearchTextCommand(botService, message)),
        var str when str.ToLower().StartsWith(TextCommandList.YoutubeSearch) => new TextMessageService(new YoutubeSearchTextCommand(botService, message)),
        var str when str.ToLower().StartsWith(TextCommandList.Roll) => new TextMessageService(new RollTextCommand(botService, message)),
        var str when str.ToLower().StartsWith(TextCommandList.TextToSpeech) => new TextMessageService(new TextToSpeechCommand(botService, message)),
        _ => new TextMessageService(new DefaultTextCommand(botService, message))
      };
    }

    public static TextMessageService Create(IBotService botService, CallbackQuery callbackQuery)
    {
      var message = callbackQuery.Message;
      message.Text = callbackQuery.Data;
      return Create(botService, message);
    }

    public async Task ProcessMessage()
    {
      await _command.ProcessMessage();
    }
  }
}
