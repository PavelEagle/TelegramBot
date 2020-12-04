using System.Collections.Concurrent;

namespace TelegramBot.BotDialogData
{
  public static class DialogBotData
  {
    public static ConcurrentDictionary<long, ConcurrentQueue<string>> AnswerData { get; set; }
    public static ConcurrentDictionary<long, ConcurrentQueue<string>> QuestionsData { get; set; }
    public static int Count { get; set; }
  }
}
