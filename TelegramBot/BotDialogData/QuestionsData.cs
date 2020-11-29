using ProtoBuf;
using System.Collections.Generic;

namespace TelegramBot.BotDialogData
{
  [ProtoContract]
  public class QuestionsData
  {
    [ProtoMember(1)]
    public List<string> Questions { get; set; }
   
    [ProtoMember(2)]
    public long QuestionId { get; set; }
  }
}
