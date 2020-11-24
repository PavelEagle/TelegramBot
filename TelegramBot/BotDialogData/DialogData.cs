using System.Collections.Generic;
using System.Xml.Serialization;

namespace TelegramBot.BotDialogData
{
  [XmlType]
  public class DialogData
  {
    [XmlElement(Order = 1)]
    public string Question { get; set; }
    [XmlElement(Order = 2)]
    public List<string> Answers { get; set; }

    public DialogData(string str, List<string> list)
    {
      Question = str;
      Answers = list;
    }
    public DialogData() { }
  }
}
