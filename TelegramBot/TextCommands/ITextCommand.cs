using System.Threading.Tasks;
using Telegram.Bot.Types;

namespace TelegramBot.TextCommands
{
  public interface ITextCommand
  {
    Task ProcessMessage(Message message);
  }
}
