using System.Collections.Generic;
using System.Xml.Serialization;

namespace TelegramBot.BotDialogData
{
  [XmlType]
  public class QuestionsData
  {
    [XmlElement(Order = 1)]
    public List<string> Questions { get; set; }
   
    [XmlElement(Order = 2)]
    public long QuestionId { get; set; }
  }
}
