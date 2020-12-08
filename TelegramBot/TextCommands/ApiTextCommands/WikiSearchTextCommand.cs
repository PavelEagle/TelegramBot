using System.Threading.Tasks;
using AngleSharp;
using AngleSharp.Html.Parser;
using Telegram.Bot.Types;
using TelegramBot.Services;
using TelegramBot.Common;
using Telegram.Bot.Types.ReplyMarkups;
using TelegramBot.BotData;
using TelegramBot.Enums;

namespace TelegramBot.TextCommands
{
  public sealed class WikiSearchTextCommand : ITextCommand
  {
    private readonly IBotService _botService;
    private readonly ChatSettingsBotData _chatSettingsBotData;
    private readonly HtmlParser _htmlParser;
    public WikiSearchTextCommand(IBotService botService, ChatSettingsBotData chatSettingsBotData)
    {
      _botService = botService;
      _chatSettingsBotData = chatSettingsBotData;
      _htmlParser = new HtmlParser();
    }

    public async Task ProcessMessage(Message message)
    {
      if (!_chatSettingsBotData.WikiApiEnable)
      {
        var inlineKeyboard = new InlineKeyboardMarkup(new[]
        {
          new[] 
          {
            InlineKeyboardButton.WithCallbackData("Travel", "Travel"),
            InlineKeyboardButton.WithCallbackData("Singing", "Singing"),
            InlineKeyboardButton.WithCallbackData("Eagle", "Eagle")
          }
        });

        _chatSettingsBotData.WikiApiEnable = true;

        await _botService.Client.SendTextMessageAsync(message.Chat.Id, "Enter name of article or choose from list: ", replyMarkup: inlineKeyboard);
        return;
      }

      var config = Configuration.Default.WithDefaultLoader().WithCss();
      var context = BrowsingContext.New(config);
      if (string.IsNullOrEmpty(message.Text))
      {
        await _botService.Client.SendTextMessageAsync(message.Chat.Id, "Invalid request");
        return;
      }

      var source = await context.OpenAsync(BotConstants.Wiki.Url + message.Text);
      var document = _htmlParser.ParseDocument(source.Body.InnerHtml);

      var firstParagraph = document.GetElementById("mf-section-0")?.GetElementsByTagName("p");

      if (firstParagraph==null)
      {
        await _botService.Client.SendTextMessageAsync(message.Chat.Id, "Not found, try again");
        return;
      }

      var result = HtmlParserHelper.RemoveUnwantedTagsFromHtmlCollection(firstParagraph);
      var exitKeyboard = KeyboardBuilder.CreateExitButton();

      _chatSettingsBotData.WikiApiEnable = false;
      
      await _botService.Client.SendTextMessageAsync(message.Chat.Id, result, replyMarkup: exitKeyboard);
    }
  }
}
