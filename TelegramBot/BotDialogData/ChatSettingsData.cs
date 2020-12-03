using ProtoBuf;

namespace TelegramBot.BotDialogData
{
  [ProtoContract]
  public class ChatSettingsBotData
  {
    [ProtoMember(1)]
    public long ChatId { get; set; }
    [ProtoMember(2)]
    public string AccountName { get; set; }
    [ProtoMember(3)]
    public bool SecretInfoAccess { get; set; }
    [ProtoMember(4)]
    public bool VoiceAnswer { get; set; }
    public int LearningState { get; set; }   // Learning State: 0 - default chat, 1 - enter question, 2 - enter answer
    public int CurrentQuestionId { get; set; }
    public bool IsWheather { get; set; }
    public bool IsWiki { get; set; }
    public bool IsYouTubeSearch { get; set; }
  }
}
