using System.Threading.Tasks;
using Telegram.Bot.Types;
using TelegramBot.Services;
using System.Linq;
using System.Collections.Generic;
using System;
using Telegram.Bot.Types.InputFiles;
using System.IO;
using TelegramBot.BotData;
using TelegramBot.Common;

namespace TelegramBot.TextCommands
{
  public class DefaultTextCommand : ITextCommand
  {
    private readonly IBotService _botService;
    private readonly ChatSettingsBotData _chatSettingsBotData;

    public DefaultTextCommand(IBotService botService, ChatSettingsBotData chatSettingsBotData)
    {
      _botService = botService;
      _chatSettingsBotData = chatSettingsBotData;
    }

    public async Task ProcessMessage(Message message)
    {
      if (CurrentDialogBotData.DialogBotData.QuestionsData == null || CurrentDialogBotData.DialogBotData.AnswerData == null)
      {
        await _botService.Client.SendTextMessageAsync(message.Chat.Id, "Sorry, i don't understand");
        return;
      }

      string resultMessage;
      var questionId = CurrentDialogBotData.DialogBotData.QuestionsData.Where(x => x.Value.Contains(message.Text.ToLower())).Select(x=>x.Key).FirstOrDefault();

      if (questionId == 0)
      {
        resultMessage = "Sorry, i don't understand";
      }
      else
      {
        var answerData = CurrentDialogBotData.DialogBotData.AnswerData.SingleOrDefault(x => x.Key == questionId);
        if (answerData.Value.Count == 0)
        {
          resultMessage = "Sorry, i don't understand";
        }
        resultMessage = GetRandomAnswer(answerData.Value);
      }

      if (_chatSettingsBotData.VoiceAnswer) 
      {
        var voiceMessage = await TextToSpeech.ToSpeech(resultMessage);
        await _botService.Client.SendAudioAsync(message.Chat.Id, new InputOnlineFile(new MemoryStream(voiceMessage)));
        return;
      };

      await _botService.Client.SendTextMessageAsync(message.Chat.Id, resultMessage);
    }

    private static string GetRandomAnswer(IEnumerable<string> answers)
    {
      var random = new Random();
      return answers.ElementAt(random.Next(answers.Count()));
    }
  }
}
