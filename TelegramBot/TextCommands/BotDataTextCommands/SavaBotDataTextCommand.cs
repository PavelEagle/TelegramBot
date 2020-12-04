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
      var dialogData = "BotData/dialog-data.txt";

      await DataService.SaveData(CurrentDialogBotData.DialogBotData, dialogData);

      if (CurrentDialogBotData.DialogBotData.AnswerData.Count != CurrentDialogBotData.DialogBotData.QuestionsData.Count)
      {
        // add delete logic
      }

      await _botService.Client.SendTextMessageAsync(message.Chat.Id, "Data have been saved");
    }
  }
}
