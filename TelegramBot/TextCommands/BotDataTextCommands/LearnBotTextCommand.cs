using System.Collections.Concurrent;
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

      var currentSettings = ChatSettings.ChatSettingsData.Single(x => x.ChatId == message.Chat.Id);

      if (message.Text== "/learn")
      {
        await _botService.Client.SendTextMessageAsync(message.Chat.Id, "Enter the question: ");
        currentSettings.LearningState = 1;
        currentSettings.CurrentQuestionId = CurrentDialogBotData.DialogBotData.Count + 1;
        return;
      }

      if (currentSettings.LearningState == 1)
      {
        if (!CurrentDialogBotData.DialogBotData.QuestionsData.Any(x => x.Value.Contains(message.Text.ToLower())))
        {
          CurrentDialogBotData.DialogBotData.QuestionsData.AddOrUpdate(currentSettings.CurrentQuestionId, new ConcurrentQueue<string>(new List<string> { message.Text.ToLower() }),
          (key, oldValue) =>
          {
            oldValue.Enqueue(message.Text.ToLower());
            return oldValue;
          });
          currentSettings.LearningState = 2;

          await _botService.Client.SendTextMessageAsync(message.Chat.Id, "Enter the answer: ");
          return;
        }

        await _botService.Client.SendTextMessageAsync(message.Chat.Id, "There is already such a question. Please, try again:");
        return;
      }

      if (message.Text == "/exit")
      {
        currentSettings.LearningState = 0;

        var exitKeyboard = KeyboardBuilder.CreateHelpMenu();
        await _botService.Client.SendTextMessageAsync(message.Chat.Id, "Yeah!", replyMarkup: exitKeyboard);
      }
      else if (currentSettings.LearningState == 2)
      {
        if(!CurrentDialogBotData.DialogBotData.AnswerData.Any(x => x.Key== currentSettings.CurrentQuestionId))
        {
          CurrentDialogBotData.DialogBotData.AnswerData.AddOrUpdate(currentSettings.CurrentQuestionId, new ConcurrentQueue<string>(new List<string> { message.Text.ToLower() }),
          (key, oldValue) =>
          {
            oldValue.Enqueue(message.Text.ToLower());
            return oldValue;
          });
        }
        else
        {
          CurrentDialogBotData.DialogBotData.AnswerData.Where(x => x.Key== currentSettings.CurrentQuestionId).SingleOrDefault().Value.Enqueue(message.Text.ToLower());
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
