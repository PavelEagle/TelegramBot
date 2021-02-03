using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Telegram.Bot.Types;

namespace TelegramBot.Services.MessageServices
{
    public class PhotoMessageService : IMessageService
    {
        private readonly IBotService _botService;
        private readonly Message _message;

        public PhotoMessageService(IBotService botService, Message message)
        {
            _botService = botService;
            _message = message;
        }

        public async Task ProcessMessage()
        {
            var fileId = _message.Photo.LastOrDefault()?.FileId;
            var file = await _botService.Client.GetFileAsync(fileId);
            var fullPath = Path.Combine(Directory.GetCurrentDirectory(), "Photos");
            var filename = DateTime.Now.ToString("dd.MM.yyyy ") + RandomString(10) + ".jpg";

            // save photo
            await using var saveImageStream = System.IO.File.Open(Path.Combine(fullPath, filename), FileMode.Create);
            await _botService.Client.DownloadFileAsync(file.FilePath, saveImageStream);

            await _botService.Client.SendTextMessageAsync(_message.Chat.Id, "Thx for the Pics");
        }

        private static string RandomString(int length)
        {
            var random = new Random();
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length).Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}