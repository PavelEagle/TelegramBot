using System.Threading.Tasks;
using AngleSharp;
using AngleSharp.Html.Parser;
using Telegram.Bot.Types;
using TelegramBot.Extensions;
using TelegramBot.Services;

namespace TelegramBot.TextCommands
{
  public class WikiSearchTextCommand : ITextCommand
  {
    private readonly Message _message;
    private readonly IBotService _botService;
    private readonly HtmlParser _htmlParser;
    public WikiSearchTextCommand(IBotService botService, Message message)
    {
      _message = message;
      _botService = botService;
      _htmlParser = new HtmlParser();
    }

    public async Task ProcessMessage()
    {
      var baseUrl = "https://en.wikipedia.org/wiki/";
      var config = Configuration.Default.WithDefaultLoader().WithCss();
      var context = BrowsingContext.New(config);
      var query = _message.Text.Substring(5).Trim();
      if (string.IsNullOrEmpty(query))
      {
        await _botService.Client.SendTextMessageAsync(_message.Chat.Id, "Invalid request");
        return;
      }

      var source = await context.OpenAsync(baseUrl+ query);
      var document = _htmlParser.ParseDocument(source.Body.InnerHtml);

      var textSection = document.GetElementById("mf-section-0");
      var result = textSection?.GetElementsByTagName("p").RemoveUnwantedTagsFromHtmlCollection();

      if (string.IsNullOrEmpty(result))
      {
        await _botService.Client.SendTextMessageAsync(_message.Chat.Id, "Not found");
        return;
      }

      await _botService.Client.SendTextMessageAsync(_message.Chat.Id, result);
    }
  }
}
