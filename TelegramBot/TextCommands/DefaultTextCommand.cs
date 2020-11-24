using System.Threading.Tasks;
using Telegram.Bot.Types;
using TelegramBot.BotDialogData;
using TelegramBot.Services;
using System.Linq;

namespace TelegramBot.TextCommands
{
  public class DefaultTextCommand: ITextCommand
  {
    private readonly IBotService _botService;
    public DefaultTextCommand(IBotService botService)
    {
      _botService = botService;
    }

    public async Task ProcessMessage(Message message)
    {
      var test = DialogBotData.DialogData.Where(x => x.Question.Contains(message.Text));

      if (!test.Any())
      {
        await _botService.Client.SendTextMessageAsync(message.Chat.Id, "Sorry, i don't understand");
        return;
      }

      var result = DialogBotData.DialogData.Where(x => x.Question.Contains(message.Text))?.FirstOrDefault().Answers?.FirstOrDefault();
      await _botService.Client.SendTextMessageAsync(message.Chat.Id, result);
    }
  }
}
