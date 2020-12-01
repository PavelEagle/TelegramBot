using System.Threading.Tasks;
using AngleSharp;
using AngleSharp.Html.Parser;
using Telegram.Bot.Types;
using TelegramBot.Services;
using TelegramBot.Common;
using Telegram.Bot.Types.ReplyMarkups;
using TelegramBot.BotDialogData;
using System.Linq;

namespace TelegramBot.TextCommands
{
  public class WikiSearchTextCommand : ITextCommand
  {
    private readonly IBotService _botService;
    private readonly HtmlParser _htmlParser;
    private readonly WikiSearchConfiguration _wikiSearchConfiguration;
    public WikiSearchTextCommand(IBotService botService, WikiSearchConfiguration wikiSearchConfiguration)
    {
      _botService = botService;
      _htmlParser = new HtmlParser();
      _wikiSearchConfiguration = wikiSearchConfiguration;
    }

    public async Task ProcessMessage(Message message)
    {
      var currentSettings = ChatSettings.ChatSettingsData.Where(x => x.ChatId == message.Chat.Id).Single();

      if (!currentSettings.IsWiki)
      {
        var inlineKeyboard = new InlineKeyboardMarkup(new[]
        {
          new[] {InlineKeyboardButton.WithCallbackData("Travel", "Travel") },
          new[] {InlineKeyboardButton.WithCallbackData("Singing", "Singing") },
          new[] {InlineKeyboardButton.WithCallbackData("Eagle", "Eagle") }
        });

        currentSettings.IsWiki = true;
        await _botService.Client.SendTextMessageAsync(message.Chat.Id, "Enter name of artice: ", replyMarkup: inlineKeyboard);
        return;
      }

      var config = Configuration.Default.WithDefaultLoader().WithCss();
      var context = BrowsingContext.New(config);
      if (string.IsNullOrEmpty(message.Text))
      {
        await _botService.Client.SendTextMessageAsync(message.Chat.Id, "Invalid request");
        return;
      }

      var source = await context.OpenAsync(_wikiSearchConfiguration .Url + message.Text);
      var document = _htmlParser.ParseDocument(source.Body.InnerHtml);

      var firstParagraph = document.GetElementById("mf-section-0")?.GetElementsByTagName("p");
      var result = HtmlParserHelper.RemoveUnwantedTagsFromHtmlCollection(firstParagraph);

      if (string.IsNullOrEmpty(result))
      {
        await _botService.Client.SendTextMessageAsync(message.Chat.Id, "Not found");
        return;
      }

      currentSettings.IsWiki = false; ;
      var exitKeyboard = KeyboardBuilder.CreateExitButton();
      await _botService.Client.SendTextMessageAsync(message.Chat.Id, result, replyMarkup: exitKeyboard);
    }
  }
}
