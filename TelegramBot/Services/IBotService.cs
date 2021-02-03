using Telegram.Bot;

namespace TelegramBot.Services
{
    public interface IBotService
    {
        TelegramBotClient Client { get; }
    }
}