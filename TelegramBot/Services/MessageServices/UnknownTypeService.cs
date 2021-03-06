﻿using System.Threading.Tasks;
using Telegram.Bot.Types;

namespace TelegramBot.Services.MessageServices
{
    public class UnknownTypeService : IMessageService
    {
        private readonly IBotService _botService;
        private readonly Message _message;

        public UnknownTypeService(IBotService botService, Message message)
        {
            _botService = botService;
            _message = message;
        }

        public async Task ProcessMessage()
        {
            await _botService.Client.SendTextMessageAsync(_message.Chat.Id, "Dude, i don't know how do this");
        }
    }
}