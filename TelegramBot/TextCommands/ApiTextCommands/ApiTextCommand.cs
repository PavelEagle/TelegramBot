using System.Threading.Tasks;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;
using TelegramBot.BotSettings.Enums;
using TelegramBot.Services;

namespace TelegramBot.TextCommands.ApiTextCommands
{
  public sealed class ApiTextCommand : ITextCommand
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