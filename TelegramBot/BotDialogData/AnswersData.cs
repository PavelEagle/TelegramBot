using System.Collections.Generic;
using System.Xml.Serialization;

namespace TelegramBot.BotDialogData
{
  [XmlType]
  public class AnswersData
  {
    [XmlElement(Order = 1)]
    public long QuestionId { get; set; }
    [XmlElement(Order = 2)]
    public List<string> Answers { get; set; }
  }
}
