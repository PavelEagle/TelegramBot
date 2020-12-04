using System.Collections.Concurrent;

namespace TelegramBot.BotDialogData
{
  public static class ChatSettings
  {
    public static ConcurrentQueue<ChatSettingsBotData> ChatSettingsData { get; set; }
  }
}
