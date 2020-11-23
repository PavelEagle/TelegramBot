using System.Threading.Tasks;
using AngleSharp;
using AngleSharp.Html.Parser;
using Telegram.Bot.Types;
using TelegramBot.Services;
using TelegramBot.Common;
using Telegram.Bot.Types.ReplyMarkups;
using TelegramBot.Commands;

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
      if (_message.Text.Trim().ToLower() == TextCommandList.Wiki)
      {
        var inlineKeyboard = new InlineKeyboardMarkup(new[]
        {
          new[] {InlineKeyboardButton.WithCallbackData("Travel", TextCommandList.Wiki + " Travel") },
          new[] {InlineKeyboardButton.WithCallbackData("Singing", TextCommandList.Wiki + " Singing") },
          new[] {InlineKeyboardButton.WithCallbackData("Samara", TextCommandList.Wiki + " Samara") }
        });

        await _botService.Client.SendTextMessageAsync(_message.Chat.Id, "Also you can read articles from wiki in English. Exapmle: /wiki {yourRequest}", replyMarkup: inlineKeyboard);
        return;
      }
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

      var firstParagraph = document.GetElementById("mf-section-0")?.GetElementsByTagName("p");
      var result = HtmlParserHelper.RemoveUnwantedTagsFromHtmlCollection(firstParagraph);

      if (string.IsNullOrEmpty(result))
      {
        await _botService.Client.SendTextMessageAsync(_message.Chat.Id, "Not found");
        return;
      }

      await _botService.Client.SendTextMessageAsync(_message.Chat.Id, result, replyMarkup: KeyboardBuilder.CreateExitButton());
    }
  }
}
