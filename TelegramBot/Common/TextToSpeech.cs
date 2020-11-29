using System.Threading.Tasks;
using RestSharp;

namespace TelegramBot.TextCommands
{
  public static class TextToSpeech
  {
    public async static Task<byte[]> ToSpeech(string message)
    {
      var url = "https://voicerss-text-to-speech.p.rapidapi.com";
      var apiKey = "c328bd4468784cee84244d155864fe74";
      var language = "en-us";
      var result = message.Replace(" ", "%20").Replace(",", "%2C");
      var client = new RestClient($"{url}/?key={apiKey}&src={result}!&hl={language}&r=0&c=mp3&f=8khz_8bit_mono");
      var request = new RestRequest(Method.GET);
      request.AddHeader("x-rapidapi-key", "2f43306bb5msh4ca4397952be799p12cbd5jsn1dff1cc7eeda");
      request.AddHeader("x-rapidapi-host", "voicerss-text-to-speech.p.rapidapi.com");
      var response = await client.ExecuteAsync(request);

      return response.RawBytes;
    }
  }
}
