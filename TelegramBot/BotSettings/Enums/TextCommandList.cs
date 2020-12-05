using System.Collections.Generic;

namespace TelegramBot.Enums
{
  public static class TextCommandList
  {  
    public const string Start = "/start";
    public const string Api = "/api";
    public const string Weather = "/weather";
    public const string Wiki = "/wiki";
    public const string Help = "/help";
    public const string YoutubeSearch = "/youtube";
    public const string Roll = "/roll";
    public const string TrainBot = "/train";
    public const string CreateNewQuestion = "/train-new";
    public const string RemoveQuestion = "/train-deleter";
    public const string AddQuestion = "/train-add-question";
    public const string AddAnswer = "/train-add-answer";
    public const string SaveBotData = "/save-data";
    public const string SetVoice = "/voice";
    public const string GetSecretInfo = "/secret";
    public const string Exit = "/exit";

    // dictionary for create buttons
    public static Dictionary<string, string> HelpCommands = new Dictionary<string, string>
      {
        { Api, "Api"},
        { Weather, "Weather info"},
        { YoutubeSearch, "Youtube search"},
        { Wiki, "Wiki search" },
        { Roll, "Roll random number"},
        { TrainBot, "Train Bot"},
        { CreateNewQuestion, "Add New Question"},
        { RemoveQuestion, "Delete Question"},
        { AddQuestion, "Add Question"},
        { AddAnswer, "Add Answer"},
        { SaveBotData, "Save Data"},
        { SetVoice, "Voice"},
        { GetSecretInfo, "Get secret info"},
      };
  }
}
