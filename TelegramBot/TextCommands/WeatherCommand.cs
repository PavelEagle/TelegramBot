using System;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using RestSharp;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;
using TelegramBot.BotDialogData;
using TelegramBot.Common;
using TelegramBot.Services;

namespace TelegramBot.TextCommands
{
  public class WeatherCommand : ITextCommand
  {
    private readonly IBotService _botService;
    private readonly WeatherApiConfiguration _weatherApiConfiguration;
    public WeatherCommand(IBotService botService, WeatherApiConfiguration weatherApiConfiguration)
    {
      _botService = botService;
      _weatherApiConfiguration = weatherApiConfiguration;
    }

    public async Task ProcessMessage(Message message)
    {
      var currentSettings = ChatSettings.ChatSettingsData.Where(x => x.ChatId == message.Chat.Id).Single();

      if (!currentSettings.IsWheather)
      {
        var inlineKeyboard = new InlineKeyboardMarkup(new[]
        {
            new[] {InlineKeyboardButton.WithCallbackData("Samara", "Samara") },
            new[] {InlineKeyboardButton.WithCallbackData("Saint-Petersburg", "Saint-Petersburg") },
            new[] {InlineKeyboardButton.WithCallbackData("Moscow", "Moscow") }
        });

        currentSettings.IsWheather = true;
        await _botService.Client.SendTextMessageAsync(message.Chat.Id, "Enter the city: ", replyMarkup: inlineKeyboard);
        return;
      }
      
      else
      {
        var uri = $"data/2.5/weather?q={message.Text}&appid={_weatherApiConfiguration.ApiKey}";

        var client = new RestClient(_weatherApiConfiguration.Host);
        var request = new RestRequest(uri, DataFormat.Json);
        var response = await client.ExecuteAsync(request);
        var json = JObject.Parse(response.Content);

        if (json["cod"].ToString() == "404")
        {
          await _botService.Client.SendTextMessageAsync(message.Chat.Id, "Unknown city");
          return;
        }

        if (json["name"] == null)
        {
          await _botService.Client.SendTextMessageAsync(message.Chat.Id, "Server Error");
          return;
        }

        var city = json["name"];
        var weather = json["weather"][0]["main"];
        var temp = Math.Round((double)json["main"]["temp"] - 273.15, 2);
        var wind = json["wind"]["speed"];
        var result = $"City: {city},\nWeather: {weather},\nTemperature: {temp} °C,\nWind: {wind} m/s";

        var exitKeyboard = KeyboardBuilder.CreateExitButton();

        currentSettings.IsWheather = false;
        await _botService.Client.SendTextMessageAsync(message.Chat.Id, result, replyMarkup: exitKeyboard);
      }
      
    }
  }
}
