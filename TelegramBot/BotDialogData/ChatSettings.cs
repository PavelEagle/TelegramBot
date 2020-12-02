using System.Collections.Concurrent;

namespace TelegramBot.BotDialogData
{
  public static class ChatSettings
  {
    public static ConcurrentBag<ChatSettingsBotData> ChatSettingsData { get; set; }
  }
}
