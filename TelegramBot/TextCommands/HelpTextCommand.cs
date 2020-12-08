using System.Text;
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
      var text = new StringBuilder("Make your choice: \n");

      text.Append("Api - get info about weather, search articles on Wikipedia.org and search videos on youtube;\n");
      text.Append("Roll - random number from 1 to 100;\n");
      text.Append("Voice - set voice answer setting;\n");
      text.Append("Train Bot - add or remove question or answers;\n");
      text.Append("Save data - save dialog bot data and chat settings\n");

      await _botService.Client.SendTextMessageAsync(message.Chat.Id, text.ToString(), replyMarkup: menu);
    }
  }
}
