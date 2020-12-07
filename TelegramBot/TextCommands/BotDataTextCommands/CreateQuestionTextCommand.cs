using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;
using TelegramBot.BotData;
using TelegramBot.Common;
using TelegramBot.Enums;
using TelegramBot.Services;

namespace TelegramBot.TextCommands
{
  public sealed class CreateQuestionTextCommand : ITextCommand
  {
    private readonly IBotService _botService;
    private readonly ChatSettingsBotData _chatSettingsBotData;

    public CreateQuestionTextCommand(IBotService botService, ChatSettingsBotData chatSettingsBotData)
    {
      _botService = botService;
      _chatSettingsBotData = chatSettingsBotData;
    }

    public async Task ProcessMessage(Message message)
    {
      if (message.Text== TextCommandList.CreateNewQuestion)
      {
        _chatSettingsBotData.TrainingAction = nameof(TrainingActions.Create);
        _chatSettingsBotData.LearningState = 1;
        _chatSettingsBotData.CurrentQuestionId = CurrentDialogBotData.DialogBotData.Count + 1;

        await _botService.Client.SendTextMessageAsync(message.Chat.Id, "Enter the question: ");
        return;
      }

      if (_chatSettingsBotData.LearningState == 1)
      {
        if (CurrentDialogBotData.DialogBotData.QuestionsData.Any(x => x.Value.Contains(message.Text.ToLower())))
        {
          await _botService.Client.SendTextMessageAsync(message.Chat.Id, "There is already such a question. Please, try again:");
          return;
        }

        CurrentDialogBotData.DialogBotData.QuestionsData.AddOrUpdate(_chatSettingsBotData.CurrentQuestionId, new ConcurrentQueue<string>(new List<string> { message.Text.ToLower() }),
          (key, oldValue) =>
          {
            oldValue.Enqueue(message.Text.ToLower());
            return oldValue;
          });
        _chatSettingsBotData.LearningState = 2;

        await _botService.Client.SendTextMessageAsync(message.Chat.Id, "Enter the answer: ");
        return;
      }

      if (message.Text == TextCommandList.Exit)
      {
        _chatSettingsBotData.TrainingAction = nameof(TrainingActions.NoTrain);
        _chatSettingsBotData.LearningState = 0;

        var exitKeyboard = KeyboardBuilder.CreateExitButton();
        await _botService.Client.SendTextMessageAsync(message.Chat.Id, "Yeah!", replyMarkup: exitKeyboard);
      }
      else if (_chatSettingsBotData.LearningState == 2)
      {
        if(CurrentDialogBotData.DialogBotData.AnswerData.All(x => x.Key != _chatSettingsBotData.CurrentQuestionId))
        {
          CurrentDialogBotData.DialogBotData.AnswerData.AddOrUpdate(_chatSettingsBotData.CurrentQuestionId, new ConcurrentQueue<string>(new List<string> { message.Text.ToLower() }),
          (key, oldValue) =>
          {
            oldValue.Enqueue(message.Text.ToLower());
            return oldValue;
          });
        }
        else
        {
          CurrentDialogBotData.DialogBotData.AnswerData.SingleOrDefault(x => x.Key== _chatSettingsBotData.CurrentQuestionId).Value.Enqueue(message.Text.ToLower());
        }

        var inlineKeyboard = new InlineKeyboardMarkup(new[]
        {
          new[] {InlineKeyboardButton.WithCallbackData("Finish train ", TextCommandList.Exit) },
        });

        await _botService.Client.SendTextMessageAsync(message.Chat.Id, "Want to add another answer?", replyMarkup: inlineKeyboard);
      }
    }
  }
}
