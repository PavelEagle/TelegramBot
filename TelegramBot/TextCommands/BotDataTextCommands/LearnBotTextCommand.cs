using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;
using TelegramBot.BotDialogData;
using TelegramBot.Common;
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

      var currentSettings = ChatSettings.ChatSettingsData.Where(x => x.ChatId == message.Chat.Id).Single();

      if (message.Text== "/learn")
      {
        await _botService.Client.SendTextMessageAsync(message.Chat.Id, "Enter the question: ");
        currentSettings.LearningState = 1;
        currentSettings.CurrentQuestionId = DialogBotData.QuestionsData.Count + 1;
        return;
      }

      if (currentSettings.LearningState == 1)
      {
        if (DialogBotData.QuestionsData.Where(x => x.Questions.Contains(message.Text.ToLower())).Any())
        {
          await _botService.Client.SendTextMessageAsync(message.Chat.Id, "There is already such a question. Please, try again:");
          return;    
        }

        var question = new QuestionsData { QuestionId = currentSettings.CurrentQuestionId, Questions = new List<string> { message.Text.ToLower() } };
        DialogBotData.QuestionsData.Add(question);
        currentSettings.LearningState = 2;

        await _botService.Client.SendTextMessageAsync(message.Chat.Id, "Enter the answer: ");
        return;
      }
      else if (message.Text == "/exit")
      {
        currentSettings.LearningState = 0;

        var exitKeyboard = KeyboardBuilder.CreateHelpMenu();
        await _botService.Client.SendTextMessageAsync(message.Chat.Id, "Yeah!", replyMarkup: exitKeyboard);
      }
      else if (currentSettings.LearningState == 2)
      {
        if(!DialogBotData.AnswerData.Where(x => x.QuestionId== currentSettings.CurrentQuestionId).Any())
        {
          var question = new AnswersData { QuestionId = currentSettings.CurrentQuestionId, Answers = new List<string> { message.Text.ToLower() } };
          DialogBotData.AnswerData.Add(question);
        }
        else
        {
          DialogBotData.AnswerData.Where(x => x.QuestionId == currentSettings.CurrentQuestionId).SingleOrDefault().Answers.Add(message.Text.ToLower());
        }

        var inlineKeyboard = new InlineKeyboardMarkup(new[]
        {
          new[] {InlineKeyboardButton.WithCallbackData("Finish train ", "/exit") },
        });

        await _botService.Client.SendTextMessageAsync(message.Chat.Id, "Want to add another answer?", replyMarkup: inlineKeyboard);
      }
    }
  }
}
