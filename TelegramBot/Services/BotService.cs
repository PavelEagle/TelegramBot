using Microsoft.Extensions.Options;
using MihaZupan;
using Telegram.Bot;
using TelegramBot.BotSettings;

namespace TelegramBot.Services
{
    public class BotService : IBotService
    {
        public BotService(IOptions<BotConfiguration> config)
        {
            var config1 = config.Value;
            // use proxy
            Client = string.IsNullOrEmpty(config1.Socks5Host)
                ? new TelegramBotClient(config1.BotToken)
                : new TelegramBotClient(
                    config1.BotToken,
                    new HttpToSocks5Proxy(config1.Socks5Host, config1.Socks5Port));
        }

        public TelegramBotClient Client { get; }
    }
}