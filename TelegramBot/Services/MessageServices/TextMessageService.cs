using System.Linq;
using System.Threading.Tasks;
using Telegram.Bot.Types;
using TelegramBot.BotData;
using TelegramBot.Enums;
using TelegramBot.TextCommands;

namespace TelegramBot.Services
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
      if (!ChatSettings.ChatSettingsData.Any(x => x.ChatId==message.Chat.Id)) {
        ChatSettings.ChatSettingsData.Enqueue(new ChatSettingsBotData { ChatId=message.Chat.Id ,AccountName = message.Chat.Username , LearningState = 0, VoiceAnswer = false});
      }

      var currentSettings = ChatSettings.ChatSettingsData.Single(x => x.ChatId == message.Chat.Id);

      if (currentSettings.LearningState == 0)
      {
        if (!(currentSettings.WikiApiEnable || currentSettings.WeatherApiEnable || currentSettings.YouTubeSearchApiEnable))
        {
          return message.Text switch
          {
            var str when str.ToLower().StartsWith(TextCommandList.Start) => new TextMessageService(new StartCommand(botService), message),
            var str when str.ToLower().StartsWith(TextCommandList.Help) => new TextMessageService(new HelpTextCommand(botService, currentSettings), message),
            var str when str.ToLower().StartsWith(TextCommandList.Roll) => new TextMessageService(new RollTextCommand(botService), message),
            var str when str.ToLower().StartsWith(TextCommandList.CreateNewQuestion) => new TextMessageService(new CreateQuestionTextCommand(botService, currentSettings), message),
            var str when str.ToLower().StartsWith(TextCommandList.RemoveQuestion) => new TextMessageService(new RemoveQuestionTextCommand(botService, currentSettings), message),
            var str when str.ToLower().StartsWith(TextCommandList.AddAnswer) => new TextMessageService(new AddAnswerTextCommand(botService, currentSettings), message),
            var str when str.ToLower().StartsWith(TextCommandList.AddQuestion) => new TextMessageService(new AddQuestionsTextCommand(botService, currentSettings), message),
            var str when str.ToLower().StartsWith(TextCommandList.TrainBot) => new TextMessageService(new TrainBotTextCommand(botService), message),
            var str when str.ToLower().StartsWith(TextCommandList.SaveBotData) => new TextMessageService(new SaveBotDataTextCommand(botService), message),
            var str when str.ToLower().StartsWith(TextCommandList.SetVoice) => new TextMessageService(new VoiceSettingTextCommand(botService, currentSettings), message),
            var str when str.ToLower().StartsWith(TextCommandList.Api) => new TextMessageService(new ApiTextCommand(botService), message),
            var str when str.ToLower().StartsWith(TextCommandList.Weather) => new TextMessageService(new WeatherCommand(botService, currentSettings), message),
            var str when str.ToLower().StartsWith(TextCommandList.Wiki) => new TextMessageService(new WikiSearchTextCommand(botService, currentSettings), message),
            var str when str.ToLower().StartsWith(TextCommandList.YoutubeSearch) => new TextMessageService(new YoutubeSearchTextCommand(botService, currentSettings), message),
            var str when str.ToLower().StartsWith(TextCommandList.GetSecretInfo) => new TextMessageService(new GetSecretInfoTextCommand(botService, currentSettings), message),
            _ => new TextMessageService(new DefaultTextCommand(botService, currentSettings), message)
          };
        }

        if (currentSettings.WikiApiEnable)
          return new TextMessageService(new WikiSearchTextCommand(botService, currentSettings), message);

        if (currentSettings.WeatherApiEnable)
          return new TextMessageService(new WeatherCommand(botService, currentSettings), message);

        if (currentSettings.YouTubeSearchApiEnable)
          return new TextMessageService(new YoutubeSearchTextCommand(botService, currentSettings), message);
      }

      return currentSettings.TrainingAction switch
      {
        nameof(TrainingActions.Create) => new TextMessageService(new CreateQuestionTextCommand(botService, currentSettings), message),
        nameof(TrainingActions.AddQuestion) => new TextMessageService(new AddQuestionsTextCommand(botService, currentSettings), message),
        nameof(TrainingActions.AddAnswer) => new TextMessageService(new AddAnswerTextCommand(botService, currentSettings), message),
        nameof(TrainingActions.Remove) => new TextMessageService(new RemoveQuestionTextCommand(botService, currentSettings), message),
        _ => new TextMessageService(new DefaultTextCommand(botService, currentSettings), message)
      };
    }

    public static TextMessageService Create(IBotService botService, CallbackQuery callbackQuery)
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
