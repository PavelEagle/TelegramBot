using System;
using System.Threading.Tasks;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;
using TelegramBot.BotSettings.Enums;
using TelegramBot.Services;

namespace TelegramBot.TextCommands
{
  public sealed class RollTextCommand: ITextCommand
  {
    private readonly IBotService _botService;
    private readonly Random _rnd;
    public RollTextCommand(IBotService botService)
    {
      _botService = botService;
      _rnd = new Random();
    }
    public async Task ProcessMessage(Message message)
    {
      var keyboard = new InlineKeyboardMarkup(new[]
      {
        new[]
        {
          InlineKeyboardButton.WithCallbackData("Test your luck again!", TextCommandList.Roll),
          InlineKeyboardButton.WithCallbackData("Back to menu", TextCommandList.Help)
        }
      });

      await _botService.Client.SendTextMessageAsync(message.Chat.Id, $"Result: {_rnd.Next(1, 101)}", replyMarkup: keyboard);
    }
  }
}
