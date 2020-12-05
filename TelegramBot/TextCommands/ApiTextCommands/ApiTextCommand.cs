using System.Threading.Tasks;
using Telegram.Bot.Types;
using TelegramBot.Services;
using Telegram.Bot.Types.ReplyMarkups;
using TelegramBot.Enums;

namespace TelegramBot.TextCommands
{
  public class ApiTextCommand : ITextCommand
  {
    private readonly IBotService _botService;
    public ApiTextCommand(IBotService botService)
    {
      _botService = botService;
    }

    public async Task ProcessMessage(Message message)
    {
      var inlineKeyboard = new InlineKeyboardMarkup(new[]
      {
        new[]
        {
          InlineKeyboardButton.WithCallbackData(TextCommandList.HelpCommands[TextCommandList.Weather], TextCommandList.Weather),
          InlineKeyboardButton.WithCallbackData(TextCommandList.HelpCommands[TextCommandList.YoutubeSearch], TextCommandList.YoutubeSearch),
          InlineKeyboardButton.WithCallbackData(TextCommandList.HelpCommands[TextCommandList.Wiki], TextCommandList.Wiki),
        }
      });

      await _botService.Client.SendTextMessageAsync(message.Chat.Id, $"Choose Api:", replyMarkup: inlineKeyboard);
    }
  }
}