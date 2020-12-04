using System.Threading.Tasks;
using System.Linq;
using Telegram.Bot.Types;
using TelegramBot.BotDialogData;
using TelegramBot.Commands;
using TelegramBot.Services;
using TelegramBot.TextCommands;
using Microsoft.Extensions.Configuration;

namespace TelegramBot.MessageTypes
{
  public class TextMessageService: IMessageService
  {
    private readonly ITextCommand _command;
    private readonly Message _message;

    private TextMessageService(ITextCommand command, Message message)
    {
      _command = command;
      _message = message;
    }

    public static TextMessageService Create(IBotService botService, Message message)
    {
      if (!ChatSettings.ChatSettingsData.Where(x=>x.ChatId==message.Chat.Id).Any()) {
        //ChatSettings.ChatSettingsData.Add(new ChatSettingsBotData { ChatId=message.Chat.Id , LearningState = 0, VoiceAnswer = false});
      }

      var currentSettings = ChatSettings.ChatSettingsData.Where(x => x.ChatId == message.Chat.Id).Single();
      if (currentSettings.LearningState == 0)
      {
        if (!(currentSettings.IsWheather || currentSettings.IsWiki || currentSettings.IsYouTubeSearch))
        {
          return message.Text switch
          {
            var str when str.ToLower().StartsWith(TextCommandList.Start) => new TextMessageService(new StartCommand(botService), message),
            var str when str.ToLower().StartsWith(TextCommandList.Help) => new TextMessageService(new HelpTextCommand(botService), message),
            var str when str.ToLower().StartsWith(TextCommandList.Roll) => new TextMessageService(new RollTextCommand(botService), message),
            var str when str.ToLower().StartsWith(TextCommandList.LearnBot) => new TextMessageService(new LearnBotTextCommand(botService), message),
            var str when str.ToLower().StartsWith(TextCommandList.LinkQuestions) => new TextMessageService(new LinkQuestionsTextCommand(botService), message),
            var str when str.ToLower().StartsWith(TextCommandList.SaveBotData) => new TextMessageService(new SavaBotDataTextCommand(botService), message),
            var str when str.ToLower().StartsWith(TextCommandList.SaveSettings) => new TextMessageService(new SaveBotSettingTextCommand(botService), message),
            var str when str.ToLower().StartsWith(TextCommandList.SetVoice) => new TextMessageService(new VoiceSettingTextCommand(botService), message),
            var str when str.ToLower().StartsWith(TextCommandList.Weather) => new TextMessageService(new WeatherCommand(botService), message),
            var str when str.ToLower().StartsWith(TextCommandList.Wiki) => new TextMessageService(new WikiSearchTextCommand(botService), message),
            var str when str.ToLower().StartsWith(TextCommandList.YoutubeSearch) => new TextMessageService(new YoutubeSearchTextCommand(botService), message),
            var str when str.ToLower().StartsWith(TextCommandList.GetSecretInfo) => new TextMessageService(new GetSecretInfoTextCommand(botService), message),
            var str when str.ToLower().StartsWith(TextCommandList.SetSecretAccess) => new TextMessageService(new SetSecretAccessTextCommand(botService), message),
            _ => new TextMessageService(new DefaultTextCommand(botService), message)
          };
        }

        if (currentSettings.IsWiki)
          return new TextMessageService(new WikiSearchTextCommand(botService), message);

        if (currentSettings.IsWheather)
          return new TextMessageService(new WeatherCommand(botService), message);

        if (currentSettings.IsYouTubeSearch)
          return new TextMessageService(new YoutubeSearchTextCommand(botService), message);
      }

      return new TextMessageService(new LearnBotTextCommand(botService), message);
    }

    public static TextMessageService Create(IBotService botService, CallbackQuery callbackQuery, IConfiguration configuration)
    {
      var message = callbackQuery.Message;
      message.Text = callbackQuery.Data;
      return Create(botService, message);
    }

    public async Task ProcessMessage()
    {
      await _command.ProcessMessage(_message);
    }
  }
}
