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
        new[] { InlineKeyboardButton.WithCallbackData("Menu", TextCommandList.Help) }
      });
    }

    public static InlineKeyboardMarkup CreateHelpMenu()
    {
      var buttonList = TextCommandList.GetHelpCommands();

      return new InlineKeyboardMarkup(new[]
      {
        new[]
        {
          InlineKeyboardButton.WithCallbackData(buttonList[TextCommandList.Weather], TextCommandList.Weather),
          InlineKeyboardButton.WithCallbackData(buttonList[TextCommandList.YoutubeSearch], TextCommandList.YoutubeSearch)
        },
        new[]
        {
          InlineKeyboardButton.WithCallbackData(buttonList[TextCommandList.Wiki], TextCommandList.Wiki),
          InlineKeyboardButton.WithCallbackData(buttonList[TextCommandList.Roll], TextCommandList.Roll)
        },
        new [] 
        {
          InlineKeyboardButton.WithCallbackData(buttonList[TextCommandList.LearnBot], TextCommandList.LearnBot) 
        },
        new[]
        {
          InlineKeyboardButton.WithCallbackData(buttonList[TextCommandList.SaveBotData], TextCommandList.SaveBotData),
          InlineKeyboardButton.WithCallbackData(buttonList[TextCommandList.SaveSettings], TextCommandList.SaveSettings),
          InlineKeyboardButton.WithCallbackData(buttonList[TextCommandList.SetVoice], TextCommandList.SetVoice)
        }
      });
    }

    public static InlineKeyboardMarkup CreateHelpMenuWithSecretAccess()
    {
      var buttonList = TextCommandList.GetHelpCommands();

      return new InlineKeyboardMarkup(new[]
      {
        new[]
        {
          InlineKeyboardButton.WithCallbackData(buttonList[TextCommandList.Weather], TextCommandList.Weather),
          InlineKeyboardButton.WithCallbackData(buttonList[TextCommandList.YoutubeSearch], TextCommandList.YoutubeSearch)
        },
        new[]
        {
          InlineKeyboardButton.WithCallbackData(buttonList[TextCommandList.Wiki], TextCommandList.Wiki),
          InlineKeyboardButton.WithCallbackData(buttonList[TextCommandList.Roll], TextCommandList.Roll)
        },
        new []
        {
          InlineKeyboardButton.WithCallbackData(buttonList[TextCommandList.LearnBot], TextCommandList.LearnBot)
        },
        new[]
        {
          InlineKeyboardButton.WithCallbackData(buttonList[TextCommandList.SaveBotData], TextCommandList.SaveBotData),
          InlineKeyboardButton.WithCallbackData(buttonList[TextCommandList.SaveSettings], TextCommandList.SaveSettings),
          InlineKeyboardButton.WithCallbackData(buttonList[TextCommandList.SetVoice], TextCommandList.SetVoice)
        },
        new[]
        {
          InlineKeyboardButton.WithCallbackData(buttonList[TextCommandList.GetSecretInfo], TextCommandList.GetSecretInfo)
        }
      });
    }

    public static InlineKeyboardMarkup CreateAdminHelpMenu()
    {
      var buttonList = TextCommandList.GetHelpCommands();

      return new InlineKeyboardMarkup(new[]
      {
        new[]
        {
          InlineKeyboardButton.WithCallbackData(buttonList[TextCommandList.Weather], TextCommandList.Weather),
          InlineKeyboardButton.WithCallbackData(buttonList[TextCommandList.YoutubeSearch], TextCommandList.YoutubeSearch)
        },
        new[]
        {
          InlineKeyboardButton.WithCallbackData(buttonList[TextCommandList.Wiki], TextCommandList.Wiki),
          InlineKeyboardButton.WithCallbackData(buttonList[TextCommandList.Roll], TextCommandList.Roll)
        },
        new []
        {
          InlineKeyboardButton.WithCallbackData(buttonList[TextCommandList.LearnBot], TextCommandList.LearnBot)
        },
        new[]
        {
          InlineKeyboardButton.WithCallbackData(buttonList[TextCommandList.SaveBotData], TextCommandList.SaveBotData),
          InlineKeyboardButton.WithCallbackData(buttonList[TextCommandList.SaveSettings], TextCommandList.SaveSettings),
          InlineKeyboardButton.WithCallbackData(buttonList[TextCommandList.SetVoice], TextCommandList.SetVoice)
        },
         new[]
        {
          InlineKeyboardButton.WithCallbackData(buttonList[TextCommandList.SetSecretAccess], TextCommandList.SetSecretAccess),
          InlineKeyboardButton.WithCallbackData(buttonList[TextCommandList.GetSecretInfo], TextCommandList.GetSecretInfo)
        }
      });
    }
  }
}
