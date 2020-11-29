using System.Threading.Tasks;
using Telegram.Bot.Types;
using TelegramBot.Services;
using System.Linq;
using TelegramBot.BotDialogData;
using System.Collections.Generic;
using System;
using Telegram.Bot.Types.InputFiles;
using System.IO;

namespace TelegramBot.TextCommands
{
  public class DefaultTextCommand: ITextCommand
  {
    private readonly IBotService _botService;
    public DefaultTextCommand(IBotService botService)
    {
      _botService = botService;
    }

    public async Task ProcessMessage(Message message)
    {
      var resultMessage = string.Empty;
      var currentSettings = ChatSettings.ChatSettingsData.Where(x => x.ChatId == message.Chat.Id).Single();
      var questionId = DialogBotData.QuestionsData.Where(x => x.Questions.Contains(message.Text.ToLower())).Select(x=>x.QuestionId).FirstOrDefault();

      if (questionId == 0)
      {
        resultMessage = "Sorry, i don't understand";
      }

      var answerData = DialogBotData.AnswerData.Where(x => x.QuestionId == questionId).SingleOrDefault();
      if (answerData == null)
      {
        resultMessage = "Sorry, i don't understand";
      }
      else
      {
        resultMessage = GetRandomAnswer(answerData.Answers);
      }
      
      if (currentSettings.VoiceAnswer) 
      {
        var voiceMessage = await TextToSpeech.ToSpeech(resultMessage);
        await _botService.Client.SendAudioAsync(message.Chat.Id, new InputOnlineFile(new MemoryStream(voiceMessage)));
        return;
      };

      await _botService.Client.SendTextMessageAsync(message.Chat.Id, resultMessage);
    }

    private string GetRandomAnswer(IList<string> answers)
    {
      var random = new Random();
      return answers[random.Next(answers.Count)];
    }
  }
}
