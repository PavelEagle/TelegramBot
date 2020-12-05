using ProtoBuf;

namespace TelegramBot.BotData
{
  [ProtoContract]
  public class ChatSettingsBotData
  {
    [ProtoMember(1)]
    public long ChatId { get; set; }
    [ProtoMember(2)]
    public string AccountName { get; set; }
    [ProtoMember(3)]
    public bool VoiceAnswer { get; set; }  //voice answer setting
    public int LearningState { get; set; } // 0 - no train, 1 and 2 - train action
    public string TrainingAction { get; set; }  //train actions in enum TrainingActions.cs
    public long CurrentQuestionId { get; set; } // question Id in bot train mode
    public bool WeatherApiEnable { get; set; }
    public bool WikiApiEnable { get; set; }
    public bool YouTubeSearchApiEnable { get; set; }
  }
}
