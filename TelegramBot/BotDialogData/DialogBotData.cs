using System.Collections.Generic;

namespace TelegramBot.BotDialogData
{
  public static class DialogBotData
  {
    public static HashSet<AnswersData> AnswerData { get; set; }
    public static HashSet<QuestionsData> QuestionsData { get; set; }
    public static int Count { get; set; }
  }
}
