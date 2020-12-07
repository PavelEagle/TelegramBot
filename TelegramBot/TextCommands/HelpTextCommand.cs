using System.Threading.Tasks;
using Telegram.Bot.Types;
using TelegramBot.BotData;
using TelegramBot.Common;
using TelegramBot.Enums;
using TelegramBot.Services;

namespace TelegramBot.TextCommands
{
  public sealed class HelpTextCommand: ITextCommand
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
      var text = "Make your choice: \n"
                 + "Api - get info about weather, search articles on Wikipedia.org and search videos on youtube;\n"
                 + "Roll - random number from 1 to 100;\n"
                 + "Voice - set voice answer setting;\n"
                 + "Train Bot - add or remove question or answers;\n"
                 + "Save data - save dialog bot data and chat settings\n";

      await _botService.Client.SendTextMessageAsync(message.Chat.Id, text, replyMarkup: menu);
    }
  }
}
