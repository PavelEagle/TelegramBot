using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AngleSharp;
using AngleSharp.Html.Parser;
using Telegram.Bot.Types;
using TelegramBot.Commands;
using TelegramBot.Services;

namespace TelegramBot.TextCommands
{
  public class NewsCommand: ITextCommand
  {
    private readonly Message _message;
    private readonly IBotService _botService;
    public NewsCommand(IBotService botService, Message message)
    {
      _message = message;
      _botService = botService;
    }

    public async Task ProcessMessage()
    {
      var baseUrl = "http://mediametrics.ru/search/week.html#ru:tm:Iphone";
      var config = Configuration.Default.WithDefaultLoader().WithCss();
      var context = BrowsingContext.New(config);
      var sourse = await context.OpenAsync(baseUrl);

      var parser = new HtmlParser();
      var document = parser.ParseDocument(sourse.Body.TextContent);

      var urls = document.GetElementsByClassName("rs-link");

      var links = urls.Select(url => url.GetElementsByClassName("a").FirstOrDefault()?.GetAttribute("href")).Take(3).ToList();

      var result = new StringBuilder();
      foreach (var link in links)
      {
        result.Append(link);
        result.Append("\n");
      }

      await _botService.Client.SendTextMessageAsync(result.ToString(), "Welcome!");
    }
  }
}
