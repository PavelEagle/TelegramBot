using System.Threading.Tasks;
using Telegram.Bot.Types;
using TelegramBot.Services;
using Telegram.Bot.Types.ReplyMarkups;
using TelegramBot.Commands;

namespace TelegramBot.TextCommands
{
  public class StartCommand: ITextCommand
  {
    private readonly IBotService _botService;
    public StartCommand(IBotService botService)
    {
      _botService = botService;
    }

    public async Task ProcessMessage(Message message)
    {
      var inlineKeyboard = new InlineKeyboardMarkup(new[]
       {
        new[] {InlineKeyboardButton.WithCallbackData("Help!", TextCommandList.Help) }
      });

      await _botService.Client.SendTextMessageAsync(message.Chat.Id, "Welcome! This is bot, bla-bla-bla. Click help to show more info", replyMarkup: inlineKeyboard);
    }
  }
}
