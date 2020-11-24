using System.Collections.Generic;

namespace TelegramBot.BotDialogData
{
  public interface IDialogBotData
  {
    public ICollection<DialogData> DialogData { get; set; }
  }
}
