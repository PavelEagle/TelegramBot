using System.Threading.Tasks;
using RestSharp;
using ApiConfgiuration = TelegramBot.BotSettings.RequestsConfiguration.TextToSpeech; 

namespace TelegramBot.TextCommands
{
  public static class TextToSpeech
  {
    public async static Task<byte[]> ToSpeech(string message)
    {
      var result = message.Replace(" ", "%20").Replace(",", "%2C");
      var client = new RestClient($"{ApiConfgiuration.Url}/?key={ApiConfgiuration.ApiKey}&" +
        $"src={result}!&hl={ApiConfgiuration.Language}&r=0&c={ApiConfgiuration.AudioFormat}&f={ApiConfgiuration.AudioFormat}");
      var request = new RestRequest(Method.GET);
      request.AddHeader("x-rapidapi-key", ApiConfgiuration.RapidApiKey);
      request.AddHeader("x-rapidapi-host", ApiConfgiuration.RapidApiHost);
      var response = await client.ExecuteAsync(request);

      return response.RawBytes;
    }
  }
}
