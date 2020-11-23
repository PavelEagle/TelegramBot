using System;
using System.Threading.Tasks;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;
using TelegramBot.Commands;
using TelegramBot.Services;

namespace TelegramBot.TextCommands
{
  public class RollTextCommand: ITextCommand
  {
    private readonly Message _message;
    private readonly IBotService _botService;
    private readonly Random _rnd;
    public RollTextCommand(IBotService botService, Message message)
    {
      _message = message;
      _botService = botService;
      _rnd = new Random();
    }
    public async Task ProcessMessage()
    {
      var keyboard = new InlineKeyboardMarkup(new[]
      {
        new[]
        {
          InlineKeyboardButton.WithCallbackData("Try again!", TextCommandList.Roll),
          InlineKeyboardButton.WithCallbackData("Back", TextCommandList.Help),
        }
      });

      await _botService.Client.SendTextMessageAsync(_message.Chat.Id, _rnd.Next(1,101).ToString(), replyMarkup: keyboard);
    }
  }
}
