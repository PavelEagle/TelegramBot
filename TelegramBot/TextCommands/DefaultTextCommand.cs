using ApiAiSDK;
using System.Threading.Tasks;
using Telegram.Bot.Types;
using TelegramBot.Services;

namespace TelegramBot.TextCommands
{
  public class DefaultTextCommand: ITextCommand
  {
    private readonly Message _message;
    private readonly IBotService _botService;
    public DefaultTextCommand(IBotService botService, Message message)
    {
      _message = message;
      _botService = botService;
    }
    public async Task ProcessMessage()
    {
      var token = "0f962c1104ea4663bb5fa1ca2bb105ad";
      var config = new AIConfiguration(token, SupportedLanguage.English);
      var apiAi = new ApiAi(config);
      var response = apiAi.TextRequest(_message.Text);
      var answer = response.Result.Fulfillment.Speech;

      if (string.IsNullOrEmpty(answer))
        await _botService.Client.SendTextMessageAsync(_message.Chat.Id, "Sorry, i don't understand");
      else
        await _botService.Client.SendTextMessageAsync(_message.Chat.Id, answer);
    }
  }
}
