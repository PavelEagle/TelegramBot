using System.Threading.Tasks;
using Telegram.Bot.Types;

namespace TelegramBot.Services
{
    public interface IUpdateService
    {
        Task EchoAsync(Update update);
    }
}