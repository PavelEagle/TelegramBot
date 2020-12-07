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
  public sealed class AddAnswerTextCommand : ITextCommand
  {
    private readonly IBotService _botService;
    private readonly ChatSettingsBotData _chatSettingsBotData;

    public AddAnswerTextCommand(IBotService botService, ChatSettingsBotData chatSettingsBotData)
    {
      _botService = botService;
      _chatSettingsBotData = chatSettingsBotData;
    }

    public async Task ProcessMessage(Message message)
    {
      if (message.Text == TextCommandList.AddAnswer)
      {
        await _botService.Client.SendTextMessageAsync(message.Chat.Id, "Enter the question: ");

        _chatSettingsBotData.TrainingAction = nameof(TrainingActions.AddAnswer);
        _chatSettingsBotData.LearningState = 1;

        return;
      }

      if (_chatSettingsBotData.LearningState == 1)
      {
        var questionId = CurrentDialogBotData.DialogBotData.QuestionsData.FirstOrDefault(x => x.Value.Contains(message.Text.ToLower())).Key;
        if (questionId == 0)
        {
          await _botService.Client.SendTextMessageAsync(message.Chat.Id, "Question not found ");
          return;
        }

        _chatSettingsBotData.LearningState = 2;
        _chatSettingsBotData.CurrentQuestionId = questionId;

        await _botService.Client.SendTextMessageAsync(message.Chat.Id, "Enter the answer you want to add:");
        return;
      }

      if (message.Text == TextCommandList.Exit)
      {
        _chatSettingsBotData.TrainingAction = nameof(TrainingActions.NoTrain);
        _chatSettingsBotData.LearningState = 0;

        var exitKeyboard = KeyboardBuilder.CreateExitButton();
        await _botService.Client.SendTextMessageAsync(message.Chat.Id, "Yeah!", replyMarkup: exitKeyboard);
        return;
      }
      if (_chatSettingsBotData.LearningState == 2)
      {
        CurrentDialogBotData.DialogBotData.AnswerData[_chatSettingsBotData.CurrentQuestionId].Enqueue(message.Text.ToLower());

        var inlineKeyboard = new InlineKeyboardMarkup(new[]
        {
          new[] {InlineKeyboardButton.WithCallbackData("Finish train ", TextCommandList.Exit) },
        });

        await _botService.Client.SendTextMessageAsync(message.Chat.Id, "Answer was added! Want to add another answer?", replyMarkup: inlineKeyboard);
      }
    }
  }
}
