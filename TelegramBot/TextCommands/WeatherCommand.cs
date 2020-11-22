using System;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using RestSharp;
using Telegram.Bot.Args;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;
using TelegramBot.Services;

namespace TelegramBot.TextCommands
{
  public class WeatherCommand : ITextCommand
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
      if (_message.Text.Trim().ToLower() == "/weather")
      {
        var inlineKeyboard = new InlineKeyboardMarkup(new[]
        {
          new[]
          {
            InlineKeyboardButton.WithCallbackData("Samara", "/weather Samara"),
            InlineKeyboardButton.WithCallbackData("Saint-Petersburg", "/weather Saint Petersburg"),
            InlineKeyboardButton.WithCallbackData("Moscow", "/weather Moscow")
          }
        });

        await _botService.Client.SendTextMessageAsync(_message.Chat.Id, "Make your choice: ", replyMarkup: inlineKeyboard);
      }
      
      else
      {
        var host = "http://api.openweathermap.org";
        var cityFromMessage = _message.Text.Substring(8).Trim().Replace(" ", "+");
        var uri = $"data/2.5/weather?q={cityFromMessage}&appid=88ec93c8bc578fb7e09367b86bce7577";

        var client = new RestClient(host);
        var request = new RestRequest(uri, DataFormat.Json);
        var response = await client.ExecuteAsync(request);
        var json = JObject.Parse(response.Content);

        if (json["cod"].ToString() == "404")
        {
          await _botService.Client.SendTextMessageAsync(_message.Chat.Id, "Unknown city");
          return;
        }

        if (json["name"] == null)
        {
          await _botService.Client.SendTextMessageAsync(_message.Chat.Id, "Server Error");
          return;
        }

        var city = json["name"];
        var weather = json["weather"][0]["main"];
        var temp = Math.Round((double)json["main"]["temp"] - 273.15, 2);
        var wind = json["wind"]["speed"];

        var result = $"City: {city},\nWeather: {weather},\nTemperature: {temp} °C,\nWind: {wind} m/s";
        await _botService.Client.SendTextMessageAsync(_message.Chat.Id, result);
      }
      
    }

    public async void Bot_OnQuiz(object sender, MessageEventArgs e)
    {
      var host = "http://api.openweathermap.org";

      var cityName = e.Message.Text;
      var uri = $"data/2.5/weather?q={cityName}&appid=88ec93c8bc578fb7e09367b86bce7577";

      var client = new RestClient(host);
      var request = new RestRequest(uri, DataFormat.Json);
      var response = client.ExecuteAsync(request);
      var json = JObject.Parse(response.Result.Content);

      if (json["cod"].ToString() == "404")
      {
        await _botService.Client.SendTextMessageAsync(e.Message.Chat.Id, "Unknown city");
        return;
      }

      if (json["name"] == null)
      {
        await _botService.Client.SendTextMessageAsync(e.Message.Chat.Id, "Server Error");
        return;
      }

      var city = json["name"];
      var weather = json["weather"][0]["main"];
      var temp = Math.Round((double)json["main"]["temp"] - 273.15, 2);
      var wind = json["wind"]["speed"];

      var result = $"City: {city},\nWeather: {weather},\nTemperature: {temp} °C,\nWind: {wind} m/s";
      await _botService.Client.SendTextMessageAsync(e.Message.Chat.Id, result);
    }
  }
}
