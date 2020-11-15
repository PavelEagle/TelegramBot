using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;
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
      var inlineKeyboard = new InlineKeyboardMarkup(new[]
      {
        new[] {InlineKeyboardButton.WithUrl("google", "https://www.google.com/"),}
      });

      var helpAnswer = new StringBuilder();
      helpAnswer.Append("/weather {city} - get weather info\n");
      helpAnswer.Append("/search {query} - search videos on youtube\n");
      helpAnswer.Append("/wiki {query} - search article on wiki\n");
      helpAnswer.Append("/roll - 1-100 random number\n");
      await _botService.Client.SendTextMessageAsync(_message.Chat.Id, helpAnswer.ToString());
    }
  }
}
