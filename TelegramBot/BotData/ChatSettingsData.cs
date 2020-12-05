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
    public bool VoiceAnswer { get; set; }
    public int LearningState { get; set; }   // Learning State: 0 - default chat, 1 - enter question, 2 - enter answer
    public string TrainingAction { get; set; }
    public long CurrentQuestionId { get; set; }
    public bool WeatherApiEnable { get; set; }
    public bool WikiApiEnable { get; set; }
    public bool YouTubeSearchApiEnable { get; set; }
  }
}
