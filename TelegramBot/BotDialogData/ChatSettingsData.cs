namespace TelegramBot.BotDialogData
{
  public class ChatSettingsBotData
  {
    public long ChatId { get; set; }
    // Learning State: 0 - default chat, 1 - enter question, 2 - enter answer
    public int LearningState { get; set; }
    public bool VoiceAnswer { get; set; }
  }
}
