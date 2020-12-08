using System;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using RestSharp;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;
using TelegramBot.BotData;
using TelegramBot.Common;
using TelegramBot.Enums;
using TelegramBot.Services;

namespace TelegramBot.TextCommands
{
  public sealed class WeatherCommand : ITextCommand
  {
    private readonly IBotService _botService;
    private readonly ChatSettingsBotData _chatSettingsBotData;

    public WeatherCommand(IBotService botService, ChatSettingsBotData chatSettingsBotData)
    {
      _botService = botService;
      _chatSettingsBotData = chatSettingsBotData;
    }

    public async Task ProcessMessage(Message message)
    {
      if (!_chatSettingsBotData.WeatherApiEnable)
      {
        var inlineKeyboard = new InlineKeyboardMarkup(new[]
        {
            new[]
            {
              InlineKeyboardButton.WithCallbackData("Samara", "Samara"),
              InlineKeyboardButton.WithCallbackData("Saint-Petersburg", "Saint Petersburg"),
              InlineKeyboardButton.WithCallbackData("Moscow", "Moscow")
            }
        });

        _chatSettingsBotData.WeatherApiEnable = true;

        await _botService.Client.SendTextMessageAsync(message.Chat.Id, "Enter the city or choose from list: ", replyMarkup: inlineKeyboard);
        return;
      }

      var uri = $"{BotConstants.Weather.Url}?q={message.Text}&appid={BotConstants.Weather.ApiKey}";
      var client = new RestClient(BotConstants.Weather.Host);
      var request = new RestRequest(uri, DataFormat.Json);
      var response = await client.ExecuteAsync(request);
      var json = JObject.Parse(response.Content);

      if (json["cod"].ToString() == "404")
      {
        await _botService.Client.SendTextMessageAsync(message.Chat.Id, "Unknown city, try again: ");
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
      var result = $"City: {city}, Weather: {weather},\nTemperature: {temp} °C, Wind: {wind} m/s";

      _chatSettingsBotData.WeatherApiEnable = false;

      var exitKeyboard = KeyboardBuilder.CreateExitButton();
      await _botService.Client.SendTextMessageAsync(message.Chat.Id, result, replyMarkup: exitKeyboard);
    }
  }
}
