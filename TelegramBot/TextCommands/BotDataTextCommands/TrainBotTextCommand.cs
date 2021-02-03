using System.Threading.Tasks;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;
using TelegramBot.BotSettings.Enums;
using TelegramBot.Services;

namespace TelegramBot.TextCommands.BotDataTextCommands
{
    public sealed class TrainBotTextCommand : ITextCommand
    {
        private readonly IBotService _botService;

        public TrainBotTextCommand(IBotService botService)
        {
            _botService = botService;
        }

        public async Task ProcessMessage(Message message)
        {
            var inlineKeyboard = new InlineKeyboardMarkup(new[]
            {
                new[]
                {
                    InlineKeyboardButton.WithCallbackData(
                        TextCommandList.HelpCommands[TextCommandList.CreateNewQuestion],
                        TextCommandList.CreateNewQuestion),
                    InlineKeyboardButton.WithCallbackData(TextCommandList.HelpCommands[TextCommandList.RemoveQuestion],
                        TextCommandList.RemoveQuestion)
                },
                new[]
                {
                    InlineKeyboardButton.WithCallbackData(TextCommandList.HelpCommands[TextCommandList.AddQuestion],
                        TextCommandList.AddQuestion),
                    InlineKeyboardButton.WithCallbackData(TextCommandList.HelpCommands[TextCommandList.AddAnswer],
                        TextCommandList.AddAnswer)
                }
            });

            await _botService.Client.SendTextMessageAsync(message.Chat.Id, "Choose train options: ",
                replyMarkup: inlineKeyboard);
        }
    }
}