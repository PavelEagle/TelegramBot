using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Telegram.Bot.Types;
using TelegramBot.BotData;
using TelegramBot.Enums;
using TelegramBot.TextCommands;

namespace TelegramBot.Services
{
  public class TextMessageService : IMessageService
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
      if (!ChatSettings.ChatSettingsData.Any(x => x.ChatId == message.Chat.Id))
      {
        ChatSettings.ChatSettingsData.Enqueue(new ChatSettingsBotData { ChatId = message.Chat.Id, AccountName = message.Chat.Username, LearningState = 0, VoiceAnswer = false });
      }

      var currentSettings = ChatSettings.ChatSettingsData.Single(x => x.ChatId == message.Chat.Id);

      // bot training
      if (currentSettings.LearningState != 0)
        return currentSettings.TrainingAction switch
        {
          nameof(TrainingActions.Create) => textCommandCreationDictionary[TextCommandList.CreateNewQuestion].Invoke(message, botService, currentSettings),
          nameof(TrainingActions.AddQuestion) => textCommandCreationDictionary[TextCommandList.AddQuestion].Invoke(message, botService, currentSettings),
          nameof(TrainingActions.AddAnswer) => textCommandCreationDictionary[TextCommandList.AddAnswer].Invoke(message, botService, currentSettings),
          nameof(TrainingActions.Remove) => textCommandCreationDictionary[TextCommandList.RemoveQuestion].Invoke(message, botService, currentSettings),
          _ => textCommandCreationDictionary[TextCommandList.Default].Invoke(message, botService, currentSettings)
        };

      if (currentSettings.WikiApiEnable)
        return textCommandCreationDictionary[TextCommandList.Wiki].Invoke(message, botService, currentSettings);

      if (currentSettings.WeatherApiEnable)
        return textCommandCreationDictionary[TextCommandList.Weather].Invoke(message, botService, currentSettings);

      if (currentSettings.YouTubeSearchApiEnable)
        return textCommandCreationDictionary[TextCommandList.YoutubeSearch].Invoke(message, botService, currentSettings);

      var textCommand = textCommandCreationDictionary.FirstOrDefault(x => message.Text.ToLower().StartsWith(x.Key)).Value;

      return textCommand != null ? textCommand.Invoke(message, botService, currentSettings)
                                 : textCommandCreationDictionary[TextCommandList.Default].Invoke(message, botService, currentSettings);
    }

    public static TextMessageService Create(IBotService botService, CallbackQuery callbackQuery)
    {
      // create message from callback
      var message = callbackQuery.Message;
      message.Text = callbackQuery.Data;
      return Create(botService, message);
    }

    public async Task ProcessMessage()
    {
      await _command.ProcessMessage(_message);
    }

    // dictionary with delegates 
    private static readonly Dictionary<string, Func<Message, IBotService, ChatSettingsBotData, TextMessageService>> textCommandCreationDictionary = 
      new Dictionary<string, Func<Message, IBotService, ChatSettingsBotData, TextMessageService>>
      {
        { TextCommandList.Start, (message, botService, settings) => new TextMessageService(new StartCommand(botService), message) },
        { TextCommandList.Help, (message, botService, settings) => new TextMessageService(new HelpTextCommand(botService, settings), message) },
        { TextCommandList.Roll, (message, botService, settings) => new TextMessageService(new RollTextCommand(botService), message) },
        { TextCommandList.CreateNewQuestion, (message, botService, settings) => new TextMessageService(new CreateQuestionTextCommand(botService, settings), message) },
        { TextCommandList.RemoveQuestion, (message, botService, settings) => new TextMessageService(new RemoveQuestionTextCommand(botService, settings), message) },
        { TextCommandList.AddAnswer, (message, botService, settings) => new TextMessageService(new AddAnswerTextCommand(botService, settings), message) },
        { TextCommandList.AddQuestion, (message, botService, settings) => new TextMessageService(new AddQuestionsTextCommand(botService, settings), message) },
        { TextCommandList.TrainBot, (message, botService, settings) => new TextMessageService(new TrainBotTextCommand(botService), message) },
        { TextCommandList.SaveBotData, (message, botService, settings) => new TextMessageService(new SaveBotDataTextCommand(botService), message) },
        { TextCommandList.SetVoice, (message, botService, settings) => new TextMessageService(new VoiceSettingTextCommand(botService, settings), message) },
        { TextCommandList.Api, (message, botService, settings) => new TextMessageService(new ApiTextCommand(botService), message) },
        { TextCommandList.Weather, (message, botService, settings) => new TextMessageService(new WeatherCommand(botService, settings), message) },
        { TextCommandList.Wiki, (message, botService, settings) => new TextMessageService(new WikiSearchTextCommand(botService, settings), message) },
        { TextCommandList.YoutubeSearch, (message, botService, settings) => new TextMessageService(new YoutubeSearchTextCommand(botService, settings), message) },
        { TextCommandList.GetSecretInfo, (message, botService, settings) => new TextMessageService(new GetSecretInfoTextCommand(botService, settings), message) },
        { TextCommandList.Default, (message, botService, settings) => new TextMessageService(new DefaultTextCommand(botService, settings), message) },
      };
  }
}
