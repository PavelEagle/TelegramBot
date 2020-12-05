using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Telegram.Bot.Types;

namespace TelegramBot.Services
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

      var path = "D:\\Documents\\Git\\TelegramBot\\TelegramBot\\Photos";
      var filename = file.FileId + "." + file.FilePath.Split('.').Last();
      await using var saveImageStream = System.IO.File.Open(Path.Combine(path, filename), FileMode.Create);
      await _botService.Client.DownloadFileAsync(file.FilePath, saveImageStream);

      await _botService.Client.SendTextMessageAsync(_message.Chat.Id, "Thx for the Pics");
    }
  }
}
