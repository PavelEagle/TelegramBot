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
    public const string LearnBot = "/learn";
    public const string LinkQuestions = "/link";
    public const string SaveBotData = "/save-data";
    public const string SaveSettings = "/save-settings";
    public const string SetVoice = "/voice";

    public static Dictionary<string, string> GetHelpCommands()
    {
      return new Dictionary<string, string>
      {
        { Weather, "Get weather info"},
        { YoutubeSearch, "Search videos on youtube"},
        { Wiki, "Search article on wiki" },
        { Roll, "Roll random number"},
        { LearnBot, "Train Bot"},
        { SaveBotData, "Save Data"},
        { SaveSettings, "Save Settings"},
        { SetVoice, "Voice"}
      };
    }
  }
}
