using System.Threading.Tasks;
using Telegram.Bot.Types;
using TelegramBot.BotData;
using TelegramBot.Common;
using TelegramBot.Enums;
using TelegramBot.Services;

namespace TelegramBot.TextCommands
{
  public class HelpTextCommand: ITextCommand
  {
    private readonly IBotService _botService;
    private readonly ChatSettingsBotData _chatSettingsBotData;

    public HelpTextCommand(IBotService botService, ChatSettingsBotData chatSettingsBotData)
    {
      _botService = botService;
      _chatSettingsBotData = chatSettingsBotData;
    }
    public async Task ProcessMessage(Message message)
    {
      var menu = (_chatSettingsBotData.AccountName == BotConstants.AccountName.Admin) ? KeyboardBuilder.CreateAdminHelpMenu(): KeyboardBuilder.CreateHelpMenu();

      await _botService.Client.SendTextMessageAsync(message.Chat.Id, "Make your choice: ", replyMarkup: menu);
    }
  }
}
