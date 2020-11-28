using System.Threading.Tasks;
using Telegram.Bot.Types;
using TelegramBot.Services;
using System.Linq;
using TelegramBot.BotDialogData;

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
      var test = DialogBotData.QuestionsData.Where(x => x.Questions.Contains(message.Text)).Select(x=>x.QuestionId).FirstOrDefault();

      if (test==0)
      {
        await _botService.Client.SendTextMessageAsync(message.Chat.Id, "Sorry, i don't understand");
        return;
      }

      var result = DialogBotData.AnswerData.Where(x => x.QuestionId == test).FirstOrDefault();
      if (result==null)
      {
        await _botService.Client.SendTextMessageAsync(message.Chat.Id, "Sorry, i don't understand");
        return;
      }
      await _botService.Client.SendTextMessageAsync(message.Chat.Id, result.Answers.FirstOrDefault());
    }
  }
}
