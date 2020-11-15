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
      var keyboard = new ReplyKeyboardMarkup(new[]
      {
        new []
        {
          new KeyboardButton("/weather Moscow"),
          new KeyboardButton("/search Dance")
        },
        new[]
        {
          new KeyboardButton("/wiki Deagle") { Text = "Wiki"},
          new KeyboardButton("/roll")
        },
        new[]
        {
          new KeyboardButton("Геолокация") { RequestLocation = true},
          new KeyboardButton("/roll")
        }
      });

      await _botService.Client.SendTextMessageAsync(_message.Chat.Id, "Welcome! Type /help to show more info.", replyMarkup: keyboard);
    }
  }
}
