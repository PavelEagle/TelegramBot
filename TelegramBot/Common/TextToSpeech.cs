using System.Threading.Tasks;
using RestSharp;
using ApiConfiguration = TelegramBot.BotSettings.Enums.BotConstants.TextToSpeech;

namespace TelegramBot.Common
{
    public static class TextToSpeech
    {
        public static async Task<byte[]> ToSpeech(string message)
        {
            var result = message.Replace(" ", "%20").Replace(",", "%2C");
            var client = new RestClient($"{ApiConfiguration.Url}/?key={ApiConfiguration.ApiKey}&" +
                                        $"src={result}!&hl={ApiConfiguration.Language}&r=0&c={ApiConfiguration.AudioFormat}&f={ApiConfiguration.AudioSettings}");
            var request = new RestRequest(Method.GET);
            request.AddHeader("x-rapidapi-key", ApiConfiguration.RapidApiKey);
            request.AddHeader("x-rapidapi-host", ApiConfiguration.RapidApiHost);
            var response = await client.ExecuteAsync(request);

            return response.RawBytes;
        }
    }
}