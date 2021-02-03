using System.Threading.Tasks;
using Telegram.Bot.Types;
using TelegramBot.BotData;
using TelegramBot.BotSettings.Enums;
using TelegramBot.Common;
using TelegramBot.Services;

namespace TelegramBot.TextCommands
{
    public sealed class GetSecretInfoTextCommand : ITextCommand
    {
        private readonly IBotService _botService;
        private readonly ChatSettingsBotData _chatSettingsBotData;

        public GetSecretInfoTextCommand(IBotService botService, ChatSettingsBotData chatSettingsBotData)
        {
            _botService = botService;
            _chatSettingsBotData = chatSettingsBotData;
        }

        public async Task ProcessMessage(Message message)
        {
            var exitKeyboard = KeyboardBuilder.CreateExitButton();
            if (_chatSettingsBotData.AccountName == BotConstants.AccountName.Admin)
            {
                await _botService.Client.SendTextMessageAsync(message.Chat.Id, "Some Secret Info",
                    replyMarkup: exitKeyboard);
                return;
            }

            await _botService.Client.SendTextMessageAsync(message.Chat.Id, "Denied ", replyMarkup: exitKeyboard);
        }
    }
}