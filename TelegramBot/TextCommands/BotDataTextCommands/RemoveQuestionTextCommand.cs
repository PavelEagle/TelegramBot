using System.Linq;
using System.Threading.Tasks;
using Telegram.Bot.Types;
using TelegramBot.BotData;
using TelegramBot.BotSettings.Enums;
using TelegramBot.Services;

namespace TelegramBot.TextCommands.BotDataTextCommands
{
    public sealed class RemoveQuestionTextCommand : ITextCommand
    {
        private readonly IBotService _botService;
        private readonly ChatSettingsBotData _chatSettingsBotData;

        public RemoveQuestionTextCommand(IBotService botService, ChatSettingsBotData chatSettingsBotData)
        {
            _botService = botService;
            _chatSettingsBotData = chatSettingsBotData;
        }

        public async Task ProcessMessage(Message message)
        {
            if (_chatSettingsBotData.AccountName != BotConstants.AccountName.Admin)
            {
                await _botService.Client.SendTextMessageAsync(message.Chat.Id, "Denied!");
                return;
            }

            if (message.Text == TextCommandList.RemoveQuestion)
            {
                _chatSettingsBotData.TrainingAction = TrainingActions.Remove;
                _chatSettingsBotData.ActiveCommand = ActiveCommand.Training;
                _chatSettingsBotData.LearningState = 1;

                await _botService.Client.SendTextMessageAsync(message.Chat.Id, "Enter the question: ");
                return;
            }

            if (_chatSettingsBotData.LearningState == 1)
            {
                var questionId = CurrentDialogBotData.DialogBotData.QuestionsData
                    .FirstOrDefault(x => x.Value.Contains(message.Text.ToLower())).Key;
                if (questionId == 0)
                {
                    await _botService.Client.SendTextMessageAsync(message.Chat.Id, "Question not found, try again ");
                    return;
                }

                CurrentDialogBotData.DialogBotData.QuestionsData.TryRemove(questionId, out var deletedQuestion);

                _chatSettingsBotData.TrainingAction = TrainingActions.NoTrain;
                _chatSettingsBotData.ActiveCommand = ActiveCommand.Default;
                _chatSettingsBotData.LearningState = 0;

                await _botService.Client.SendTextMessageAsync(message.Chat.Id,
                    $"Question \"{deletedQuestion.FirstOrDefault()}\" was deleted");
            }
        }
    }
}