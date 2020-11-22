using System;
using System.Threading.Tasks;
using Telegram.Bot.Types;
using TelegramBot.Services;
using Google.Apis.Customsearch.v1;
using Google.Apis.Services;
using TelegramBot.Commands;
using Telegram.Bot.Types.ReplyMarkups;

namespace TelegramBot.TextCommands
{
  public class YoutubeSearchTextCommand : ITextCommand
  {
    private readonly Message _message;
    private readonly IBotService _botService;
    public YoutubeSearchTextCommand(IBotService botService, Message message)
    {
      _message = message;
      _botService = botService;
    }
    public async Task ProcessMessage()
    {
      if (_message.Text.Trim().ToLower() == TextCommandList.Search)
      {
        var inlineKeyboard = new InlineKeyboardMarkup(new[]
        {
          new[] {InlineKeyboardButton.WithCallbackData("Dancing!", "/youtube Dancing") },
          new[] {InlineKeyboardButton.WithCallbackData("Cats", "/youtube Cats") },
          new[] {InlineKeyboardButton.WithCallbackData("Space", "/youtube Space") }
        });

        await _botService.Client.SendTextMessageAsync(_message.Chat.Id, "Yeah, you can see youtube videos! Exapmle: /youtube {yourRequest}", replyMarkup: inlineKeyboard);
      }

      var query = _message.Text.Substring(7).Trim().Replace(" ", "+");
      var cx = "65e3d895a76958419";
      var apiKey = "AIzaSyD8bcAb-gJryXj3hZ_oeXq6T3Rih9hyUNA";
      var svc = new CustomsearchService(new BaseClientService.Initializer {ApiKey = apiKey});
      var listRequest = svc.Cse.List();
      listRequest.Q = query;

      listRequest.Cx = cx;
      var search = await listRequest.ExecuteAsync();

      if (search.Items == null || search.Items.Count == 0)
      {
        await _botService.Client.SendTextMessageAsync(_message.Chat.Id, "No videos:(");
        return;
      }

      for (var index = 0; index < Math.Min(search.Items.Count, 3); index++)
      {
        await _botService.Client.SendTextMessageAsync(_message.Chat.Id, search.Items[index].Link);
      }
    }
  }
}
