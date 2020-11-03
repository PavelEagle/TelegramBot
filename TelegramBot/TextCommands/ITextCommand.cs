using System.Threading.Tasks;

namespace TelegramBot.TextCommands
{
  public interface ITextCommand
  {
    Task ProcessMessage();
  }
}
