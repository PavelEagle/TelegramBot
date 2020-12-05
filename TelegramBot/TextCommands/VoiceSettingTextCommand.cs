using System.Threading.Tasks;
using Telegram.Bot.Types;
using TelegramBot.BotData;
using TelegramBot.Common;
using TelegramBot.Services;

namespace TelegramBot.TextCommands
{
  public class VoiceSettingTextCommand : ITextCommand
  {
    private readonly IBotService _botService;
    private readonly ChatSettingsBotData _chatSettingsBotData;

    public VoiceSettingTextCommand(IBotService botService, ChatSettingsBotData chatSettingsBotData)
    {
      _botService = botService;
      _chatSettingsBotData = chatSettingsBotData;
    }
    public async Task ProcessMessage(Message message)
    {
      string infoMessage;

      if (!_chatSettingsBotData.VoiceAnswer)
      {
        _chatSettingsBotData.VoiceAnswer = true;
        infoMessage = "Voice: On";
      }
      else
      {
        _chatSettingsBotData.VoiceAnswer = false;
        infoMessage = "Voice: Off";
      }

      var exitKeyboard = KeyboardBuilder.CreateExitButton();
      await _botService.Client.SendTextMessageAsync(message.Chat.Id, infoMessage, replyMarkup: exitKeyboard);
    }
  }
}
