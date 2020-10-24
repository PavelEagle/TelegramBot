using System.Threading.Tasks;

namespace TelegramBot.MessageTypes
{
  internal interface IMessageTypeService
  {
    Task ProcessMessage();
  }
}
