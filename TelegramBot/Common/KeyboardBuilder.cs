using System.Collections.Generic;
using Telegram.Bot.Types.ReplyMarkups;
using TelegramBot.Commands;

namespace TelegramBot.Common
{
  public static class KeyboardBuilder
  {
    public static InlineKeyboardMarkup CreateExitButton()
    {
      return new InlineKeyboardMarkup(new[]
      { 
        new[] { InlineKeyboardButton.WithCallbackData("Back", TextCommandList.Help) }
      });
    }

    public static InlineKeyboardMarkup CreateHelpMenu()
    {
      var resultedList = new List<List<InlineKeyboardButton>>();

      foreach(var button in TextCommandList.GetHelpCommands())
      {
        resultedList.Add(new List<InlineKeyboardButton>() { InlineKeyboardButton.WithCallbackData(button.Key, button.Value) });
      }
      return new InlineKeyboardMarkup(resultedList);
    }
  }
}
