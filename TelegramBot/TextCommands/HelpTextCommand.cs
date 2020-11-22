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
        new[] {InlineKeyboardButton.WithCallbackData("Get weather info", "/weather") },
        new[] {InlineKeyboardButton.WithCallbackData("Search videos on youtube", "/search") },
        new[] {InlineKeyboardButton.WithCallbackData("Search article on wiki", "/wiki") },
        new[] {InlineKeyboardButton.WithCallbackData("Roll random number from 1 to 100", "/roll") }
      });

      await _botService.Client.SendTextMessageAsync(_message.Chat.Id, "Make your choice: ", replyMarkup: inlineKeyboard);
    }
  }
}
