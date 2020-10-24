using System.Threading.Tasks;

namespace TelegramBot.Commands
{
  public interface ITextCommand
  {
    Task ProcessMessage();
  }
}
