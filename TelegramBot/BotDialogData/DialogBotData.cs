using System.Collections.Concurrent;

namespace TelegramBot.BotDialogData
{
  public static class DialogBotData
  {
    public static ConcurrentBag<AnswersData> AnswerData { get; set; }
    public static ConcurrentBag<QuestionsData> QuestionsData { get; set; }
    public static int Count { get; set; }
  }
}
