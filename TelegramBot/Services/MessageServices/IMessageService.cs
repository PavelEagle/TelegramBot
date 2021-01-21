using System.Threading.Tasks;

namespace TelegramBot.Services.MessageServices
{
  internal interface IMessageService
  {
    Task ProcessMessage();
  }
}
