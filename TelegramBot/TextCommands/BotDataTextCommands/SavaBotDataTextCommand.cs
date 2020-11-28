using System.Threading.Tasks;
using Telegram.Bot.Types;
using TelegramBot.BotDialogData;
using TelegramBot.Services;

namespace TelegramBot.TextCommands
{
  public class SavaBotDataTextCommand : ITextCommand
  {
    private readonly IBotService _botService;
    public SavaBotDataTextCommand(IBotService botService)
    {
      _botService = botService;
    }

    public async Task ProcessMessage(Message message)
    {
      var pathAnswersData = "BotData/answers-data.txt";
      var pathQuestionsData = "BotData/questions-data.txt";

      await DataService.SaveData(DialogBotData.AnswerData, pathAnswersData);
      if (DialogBotData.AnswerData.Count != DialogBotData.QuestionsData.Count)
      {
        // add delete logic
      }
      await DataService.SaveData(DialogBotData.QuestionsData, pathQuestionsData);

      await _botService.Client.SendTextMessageAsync(message.Chat.Id, "Data have been saved");
    }
  }
}
