using System.Threading.Tasks;
using Telegram.Bot.Types;
using TelegramBot.Services;
using Telegram.Bot.Types.ReplyMarkups;

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
      var inlineKeyboard = new InlineKeyboardMarkup(new[]
       {
        new[] {InlineKeyboardButton.WithCallbackData("Help!", "/help") }
      });

      await _botService.Client.SendTextMessageAsync(_message.Chat.Id, "Welcome! This is bot, bla-bla-bla. Click help to show more info", replyMarkup: inlineKeyboard);
    }
  }
}
