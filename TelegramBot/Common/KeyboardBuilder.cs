using System.Linq;
using Telegram.Bot.Types.ReplyMarkups;
using TelegramBot.Enums;

namespace TelegramBot.Common
{
  public static class KeyboardBuilder
  {
    public static InlineKeyboardMarkup CreateExitButton()
    {
      return new InlineKeyboardMarkup(new[]
      { 
        new[] { InlineKeyboardButton.WithCallbackData("Menu", TextCommandList.Help) }
      });
    }

    public static InlineKeyboardMarkup CreateHelpMenu()
    {
      return new InlineKeyboardMarkup(new[]
      {
        new[]
        {
          InlineKeyboardButton.WithCallbackData(TextCommandList.HelpCommands[TextCommandList.Api], TextCommandList.Api),
          InlineKeyboardButton.WithCallbackData(TextCommandList.HelpCommands[TextCommandList.Roll], TextCommandList.Roll)
        },
        new[]
        {
          InlineKeyboardButton.WithCallbackData(TextCommandList.HelpCommands[TextCommandList.SaveBotData], TextCommandList.SaveBotData),
          InlineKeyboardButton.WithCallbackData(TextCommandList.HelpCommands[TextCommandList.SetVoice], TextCommandList.SetVoice)
        },
        new [] 
        {
          InlineKeyboardButton.WithCallbackData(TextCommandList.HelpCommands[TextCommandList.TrainBot], TextCommandList.TrainBot) 
        }
      });
    }


    public static InlineKeyboardMarkup CreateAdminHelpMenu()
    {
      var keyboard = CreateHelpMenu().InlineKeyboard.ToList();

      keyboard.RemoveAt(keyboard.Count-1);
      keyboard.Add(new[]
      {
        InlineKeyboardButton.WithCallbackData(TextCommandList.HelpCommands[TextCommandList.TrainBot], TextCommandList.TrainBot),
        InlineKeyboardButton.WithCallbackData(TextCommandList.HelpCommands[TextCommandList.GetSecretInfo], TextCommandList.GetSecretInfo)
      });

      return new InlineKeyboardMarkup(keyboard);
    }
  }
}
