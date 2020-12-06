using System;
using System.Threading.Tasks;
using Telegram.Bot.Types;
using TelegramBot.Common;
using TelegramBot.Services;

namespace TelegramBot.TextCommands
{
  public class SaveBotDataTextCommand : ITextCommand
  {
    private readonly IBotService _botService;
    public SaveBotDataTextCommand(IBotService botService)
    {
      _botService = botService;
    }

    public async Task ProcessMessage(Message message)
    {
      try
      {
        await DataService.SaveBotData();
        await DataService.SaveChatSettings();

        await _botService.Client.SendTextMessageAsync(message.Chat.Id, "Data has been saved");
      }
      catch(Exception ex)
      {
        await _botService.Client.SendTextMessageAsync(message.Chat.Id, $"Error: {ex}");
      }
    }
  }
}
