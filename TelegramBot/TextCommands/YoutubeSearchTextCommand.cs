using System;
using System.Threading.Tasks;
using Telegram.Bot.Types;
using TelegramBot.Services;
using Google.Apis.Customsearch.v1;
using Google.Apis.Services;
using Telegram.Bot.Types.ReplyMarkups;
using TelegramBot.Common;
using TelegramBot.BotDialogData;
using System.Linq;
using TelegramBot.BotSettings;

namespace TelegramBot.TextCommands
{
  public class YoutubeSearchTextCommand : ITextCommand
  {
    private readonly IBotService _botService;
    public YoutubeSearchTextCommand(IBotService botService)
    {
      _botService = botService;
    }
    public async Task ProcessMessage(Message message)
    {
      var currentSettings = ChatSettings.ChatSettingsData.Where(x => x.ChatId == message.Chat.Id).Single();

      if (!currentSettings.IsYouTubeSearch)
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

        currentSettings.IsYouTubeSearch = true;
        await _botService.Client.SendTextMessageAsync(message.Chat.Id, "Try to search or choose from list: ", replyMarkup: inlineKeyboard);
        return;
      }

      var svc = new CustomsearchService(new BaseClientService.Initializer {ApiKey = RequestsConfiguration.YouTubeSearch.ApiKey});
      var listRequest = svc.Cse.List();
      listRequest.Q = message.Text;

      listRequest.Cx = RequestsConfiguration.YouTubeSearch.CxKey;
      var search = await listRequest.ExecuteAsync();

      var keyboard = KeyboardBuilder.CreateExitButton();

      if (search.Items == null || search.Items.Count == 0)
      {
        await _botService.Client.SendTextMessageAsync(message.Chat.Id, "No videos:( Try again: ", replyMarkup: keyboard);
        return;
      }

      currentSettings.IsYouTubeSearch = false;
      for (var index = 0; index < Math.Min(search.Items.Count, 3); index++)
      {
        await _botService.Client.SendTextMessageAsync(message.Chat.Id, search.Items[index].Link, replyMarkup: keyboard);
      }
    }
  }
}
