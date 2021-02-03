using System.Collections.Concurrent;

namespace TelegramBot.BotData
{
    public static class ChatSettings
    {
        public static ConcurrentQueue<ChatSettingsBotData> ChatSettingsData { get; set; }
    }
}