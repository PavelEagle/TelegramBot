using System;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using RestSharp;
using Telegram.Bot.Types;
using TelegramBot.Services;

namespace TelegramBot.Commands
{
  public class WeatherCommand: ITextCommand
  {
    private readonly Message _message;
    private readonly IBotService _botService;
    public WeatherCommand(IBotService botService, Message message)
    {
      _message = message;
      _botService = botService;
    }

    public async Task ProcessMessage()
    {
      var host = "http://api.openweathermap.org";
      var uri = "data/2.5/weather?q=Saint+Petersburg&appid=88ec93c8bc578fb7e09367b86bce7577";

      var client = new RestClient(host);
      var request = new RestRequest(uri, DataFormat.Json);
      var response = await client.ExecuteAsync(request);
      var json = JObject.Parse(response.Content);

      var city = json["name"];
      var weather = json["weather"][0]["main"];
      var temp = Math.Round((double)json["main"]["temp"]-273.15, 2);
      var wind = json["wind"]["speed"];

      var result = $"City: {city},\nWeather: {weather},\nTemperature: {temp} °C,\nWind: {wind} m/s";

      await _botService.Client.SendTextMessageAsync(_message.Chat.Id, result);
    }
  }
}
