using System;
using System.Threading.Tasks;
using Google.Apis.Customsearch.v1;
using Google.Apis.Services;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;
using TelegramBot.BotData;
using TelegramBot.BotSettings.Enums;
using TelegramBot.Common;
using TelegramBot.Services;

namespace TelegramBot.TextCommands.ApiTextCommands
{
    public sealed class YoutubeSearchTextCommand : ITextCommand
    {
        private readonly IBotService _botService;
        private readonly ChatSettingsBotData _chatSettingsBotData;

        public YoutubeSearchTextCommand(IBotService botService, ChatSettingsBotData chatSettingsBotData)
        {
            _botService = botService;
            _chatSettingsBotData = chatSettingsBotData;
        }

        public async Task ProcessMessage(Message message)
        {
            if (_chatSettingsBotData.ActiveCommand != ActiveCommand.YouTubeSearch)
            {
                var inlineKeyboard = new InlineKeyboardMarkup(new[]
                {
                    new[]
                    {
                        InlineKeyboardButton.WithCallbackData("Dancing!", "Dancing"),
                        InlineKeyboardButton.WithCallbackData("Cats in space", "Cats in space"),
                        InlineKeyboardButton.WithCallbackData("Tame Impala", "Tame Impala")
                    }
                });

                _chatSettingsBotData.ActiveCommand = ActiveCommand.YouTubeSearch;

                await _botService.Client.SendTextMessageAsync(message.Chat.Id,
                    "Choose from list ot try to search something funny: ", replyMarkup: inlineKeyboard);
                return;
            }

            var svc = new CustomsearchService(new BaseClientService.Initializer
                {ApiKey = BotConstants.YouTubeSearch.ApiKey});
            var listRequest = svc.Cse.List();
            listRequest.Q = message.Text;

            listRequest.Cx = BotConstants.YouTubeSearch.CxKey;
            var search = await listRequest.ExecuteAsync();

            var keyboard = KeyboardBuilder.CreateExitButton();

            if (search.Items == null || search.Items.Count == 0)
            {
                await _botService.Client.SendTextMessageAsync(message.Chat.Id, "No videos:( Try again: ",
                    replyMarkup: keyboard);
                return;
            }

            var numberOfVideos = 3;

            for (var i = 0; i < Math.Min(search.Items.Count, numberOfVideos); i++)
            {
                if (i == Math.Min(search.Items.Count, numberOfVideos) - 1)
                {
                    await _botService.Client.SendTextMessageAsync(message.Chat.Id, search.Items[i].Link,
                        replyMarkup: keyboard);
                    break;
                }

                await _botService.Client.SendTextMessageAsync(message.Chat.Id, search.Items[i].Link);
            }

            _chatSettingsBotData.ActiveCommand = ActiveCommand.Default;
        }
    }
}