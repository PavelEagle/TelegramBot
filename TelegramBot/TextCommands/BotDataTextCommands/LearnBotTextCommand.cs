using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Telegram.Bot.Types;
using TelegramBot.BotDialogData;
using TelegramBot.Services;

namespace TelegramBot.TextCommands
{
  public class LearnBotTextCommand : ITextCommand
  {
    private readonly IBotService _botService;
    public LearnBotTextCommand(IBotService botService)
    {
      _botService = botService;
    }

    public async Task ProcessMessage(Message message)
    {
      if (string.IsNullOrEmpty(message.Text)) 
      {
        await _botService.Client.SendTextMessageAsync(message.Chat.Id, "invalid format");
        return;
      }

      if (message.Text== "/learn")
      {
        await _botService.Client.SendTextMessageAsync(message.Chat.Id, "Enter the question: ");
        ChatSettings.ChatSettingsData.Where(x => x.ChatId == message.Chat.Id).Single().LearningState = 1;
      }

      var questionId = DialogBotData.QuestionsData.Count + 1;

      if (ChatSettings.ChatSettingsData.Where(x => x.ChatId == message.Chat.Id).Single().LearningState == 1)
      {
        if (!DialogBotData.QuestionsData.Where(x => x.Questions.Contains(message.Text)).Any())
        {
          var question = new QuestionsData { QuestionId = questionId, Questions = new List<string> { message.Text } };
          DialogBotData.QuestionsData.Add(question);
        }

        ChatSettings.ChatSettingsData.Where(x => x.ChatId == message.Chat.Id).Single().LearningState = 2;
        await _botService.Client.SendTextMessageAsync(message.Chat.Id, "Enter the answer: ");
      }
      else if (ChatSettings.ChatSettingsData.Where(x => x.ChatId == message.Chat.Id).Single().LearningState == 2)
      {
        if(!DialogBotData.AnswerData.Where(x => x.QuestionId== questionId).Any())
        {
          var question = new QuestionsData { QuestionId = questionId, Questions = new List<string> { message.Text } };
          DialogBotData.QuestionsData.Add(question);
        }
        else
        {
          DialogBotData.AnswerData.Where(x => x.QuestionId == questionId).Single().Answers.Add(message.Text);
        }

        await _botService.Client.SendTextMessageAsync(message.Chat.Id, "Want to add another answer? (press /exit if you want to exit");
      }
      else if (message.Text == "/exit")
        ChatSettings.ChatSettingsData.Where(x => x.ChatId == message.Chat.Id).Single().LearningState = 0;
        await _botService.Client.SendTextMessageAsync(message.Chat.Id, "Yeah!");
    }
  }
}
