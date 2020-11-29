using System.Linq;
using System.Threading.Tasks;
using Telegram.Bot.Types;
using TelegramBot.BotDialogData;
using TelegramBot.Common;
using TelegramBot.Services;

namespace TelegramBot.TextCommands
{
  public class VoiceSettingTextCommand : ITextCommand
  {
    private readonly IBotService _botService;
    public VoiceSettingTextCommand(IBotService botService)
    {
      _botService = botService;
    }
    public async Task ProcessMessage(Message message)
    {
      var currentSettings = ChatSettings.ChatSettingsData.Where(x => x.ChatId == message.Chat.Id).Single();
      var infoMessage = string.Empty;

      if (!currentSettings.VoiceAnswer)
      {
        currentSettings.VoiceAnswer = true;
        infoMessage = "Voice: On";
      }
      else
      {
        currentSettings.VoiceAnswer = false;
        infoMessage = "Voice: Off";
      }

      var exitKeyboard = KeyboardBuilder.CreateExitButton();
      await _botService.Client.SendTextMessageAsync(message.Chat.Id, infoMessage, replyMarkup: exitKeyboard);
    }
  }
}
