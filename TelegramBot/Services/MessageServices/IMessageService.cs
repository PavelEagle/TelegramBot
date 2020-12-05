using System.Threading.Tasks;

namespace TelegramBot.Services
{
  internal interface IMessageService
  {
    Task ProcessMessage();
  }
}
