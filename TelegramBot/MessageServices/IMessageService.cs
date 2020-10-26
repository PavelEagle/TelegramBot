using System.Threading.Tasks;

namespace TelegramBot.MessageTypes
{
  internal interface IMessageService
  {
    Task ProcessMessage();
  }
}
