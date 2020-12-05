using System;
using System.Threading.Tasks;
using Telegram.Bot.Types;
using TelegramBot.Services;
using Google.Apis.Customsearch.v1;
using Google.Apis.Services;
using Telegram.Bot.Types.ReplyMarkups;
using TelegramBot.BotData;
using TelegramBot.Common;
using TelegramBot.Enums;

namespace TelegramBot.TextCommands
{
  public class YoutubeSearchTextCommand : ITextCommand
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
      if (!_chatSettingsBotData.YouTubeSearchApiEnable)
      {
        var inlineKeyboard = new InlineKeyboardMarkup(new[]
        {
          new[] 
          {
            InlineKeyboardButton.WithCallbackData("Dancing!", "Dancing"),
            InlineKeyboardButton.WithCallbackData("Cats", "Cats"),
            InlineKeyboardButton.WithCallbackData("Space", "Space")
          }
        });

        _chatSettingsBotData.YouTubeSearchApiEnable = true;
        await _botService.Client.SendTextMessageAsync(message.Chat.Id, "Try to search or choose from list: ", replyMarkup: inlineKeyboard);
        return;
      }

      var svc = new CustomsearchService(new BaseClientService.Initializer {ApiKey = BotConstants.YouTubeSearch.ApiKey});
      var listRequest = svc.Cse.List();
      listRequest.Q = message.Text;

      listRequest.Cx = BotConstants.YouTubeSearch.CxKey;
      var search = await listRequest.ExecuteAsync();

      var keyboard = KeyboardBuilder.CreateExitButton();

      if (search.Items == null || search.Items.Count == 0)
      {
        await _botService.Client.SendTextMessageAsync(message.Chat.Id, "No videos:( Try again: ", replyMarkup: keyboard);
        return;
      }

      _chatSettingsBotData.YouTubeSearchApiEnable = false;
      for (var i = 0; i < Math.Min(search.Items.Count, 3); i++)
      {
        if (i == Math.Min(search.Items.Count, 3) - 1)
        {
          var exitKeyboard = KeyboardBuilder.CreateExitButton();
          await _botService.Client.SendTextMessageAsync(message.Chat.Id, search.Items[i].Link, replyMarkup: exitKeyboard);
          break;
        }

        await _botService.Client.SendTextMessageAsync(message.Chat.Id, search.Items[i].Link);
      }
    }
  }
}
