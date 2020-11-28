namespace TelegramBot.BotDialogData
{
  public class ChatSettingsBotData
  {
    public long ChatId { get; set; }
    // Learning State: 0 - default chat, 1 - enter question, 2 - enter answer
    public int LearningState { get; set; }
    public int CurrentQuestionId { get; set; }

    public bool IsWheather { get; set; }
    public bool IsWiki { get; set; }
    public bool IsYouTubeSearch { get; set; }
    public bool VoiceAnswer { get; set; }
  }
}
