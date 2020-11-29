using ProtoBuf;
using System.Collections.Generic;

namespace TelegramBot.BotDialogData
{
  [ProtoContract]
  public class AnswersData
  {
    [ProtoMember(1)]
    public long QuestionId { get; set; }
    [ProtoMember(2)]
    public List<string> Answers { get; set; }
  }
}
