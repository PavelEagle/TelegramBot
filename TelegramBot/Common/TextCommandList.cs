using System.Collections.Generic;

namespace TelegramBot.Commands
{
  public static class TextCommandList
  {  
    public const string Start = "/start";
    public const string Weather = "/weather";
    public const string Wiki = "/wiki";
    public const string Help = "/help";
    public const string YoutubeSearch = "/youtube";
    public const string Roll = "/roll";
    public const string TextToSpeech = "/speech";

    public static Dictionary<string, string> GetHelpCommands()
    {
      return new Dictionary<string, string>
      {
        { "Get weather info", Weather},
        { "Search videos on youtube", YoutubeSearch},
        { "Search article on wiki", Wiki},
        { "Roll random number from 1 to 100", Roll}
      };
    }
  }
}
