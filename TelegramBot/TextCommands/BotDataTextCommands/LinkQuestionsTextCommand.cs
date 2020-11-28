using System.Threading.Tasks;
using Telegram.Bot.Types;
using TelegramBot.Services;

namespace TelegramBot.TextCommands
{
  public class LinkQuestionsTextCommand : ITextCommand
  {
    private readonly IBotService _botService;
    public LinkQuestionsTextCommand(IBotService botService)
    {
      _botService = botService;
    }

    public async Task ProcessMessage(Message message)
    {

      await _botService.Client.SendTextMessageAsync(message.Chat.Id, "Sorry, i don't understand");
    }
  }
}
