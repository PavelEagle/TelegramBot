namespace TelegramBot.BotSettings.Enums
{
  public static class BotConstants
  {
    internal class Weather
    {
      public const string Host = "http://api.openweathermap.org";
      public const string Url = "data/2.5/weather";
      public const string ApiKey = "88ec93c8bc578fb7e09367b86bce7577";
    }

    internal class YouTubeSearch
    {
      public const string CxKey = "65e3d895a76958419";
      public const string ApiKey = "AIzaSyD8bcAb-gJryXj3hZ_oeXq6T3Rih9hyUNA";
    }

    internal class TextToSpeech
    {
      public const string ApiKey = "c328bd4468784cee84244d155864fe74";
      public const string Url = "https://voicerss-text-to-speech.p.rapidapi.com";
      public const string Language = "en-us";
      public const string RapidApiKey = "2f43306bb5msh4ca4397952be799p12cbd5jsn1dff1cc7eeda";
      public const string RapidApiHost = "voicerss-text-to-speech.p.rapidapi.com";
      public const string AudioFormat = "mp3";
      public const string AudioSettings = "8khz_8bit_mono";
    }

    internal class Wiki
    {
      public const string Url = "https://en.wikipedia.org/wiki/";
    }

    internal class AccountName
    {
      public const string Admin = "PavelEagle";
    }

    internal class Paths
    {
      public const string DialogData = "data/dialog-data.txt";
      public const string Settings = "data/chat-settings.txt";
    }
  }
}
