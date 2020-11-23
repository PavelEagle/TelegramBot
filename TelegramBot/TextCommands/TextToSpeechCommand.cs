using System.IO;
using System.Threading.Tasks;
using RestSharp;
using Telegram.Bot.Types;
using Telegram.Bot.Types.InputFiles;
using TelegramBot.Services;

namespace TelegramBot.TextCommands
{
  public class TextToSpeechCommand: ITextCommand
  {
    private readonly Message _message;
    private readonly IBotService _botService;
    public TextToSpeechCommand(IBotService botService, Message message)
    {
      _message = message;
      _botService = botService;
    }
    public async Task ProcessMessage()
    {
      var url = "https://voicerss-text-to-speech.p.rapidapi.com";
      var apiKey = "c328bd4468784cee84244d155864fe74";
      var language = "en-us";
      var message = _message.Text.Replace(" ", "%20").Replace(",", "%2C").Substring(7).Trim();
      var client = new RestClient($"{url}/?key={apiKey}&src={message}!&hl={language}&r=0&c=mp3&f=8khz_8bit_mono");
      var request = new RestRequest(Method.GET);
      request.AddHeader("x-rapidapi-key", "2f43306bb5msh4ca4397952be799p12cbd5jsn1dff1cc7eeda");
      request.AddHeader("x-rapidapi-host", "voicerss-text-to-speech.p.rapidapi.com");
      var response = await client.ExecuteAsync(request);

      await _botService.Client.SendAudioAsync(_message.Chat.Id, new InputOnlineFile(new MemoryStream(response.RawBytes)));
    }
  }
}
