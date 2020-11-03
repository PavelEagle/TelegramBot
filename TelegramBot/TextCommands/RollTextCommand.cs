using System;
using System.Threading.Tasks;
using Telegram.Bot.Types;
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
      await _botService.Client.SendTextMessageAsync(_message.Chat.Id, _rnd.Next(1,101).ToString());
    }
  }
}
