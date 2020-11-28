using System.Threading.Tasks;
using Telegram.Bot.Types;
using TelegramBot.Services;
using System.Linq;
using TelegramBot.BotDialogData;
using System.Collections.Generic;
using System;

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
      var questionId = DialogBotData.QuestionsData.Where(x => x.Questions.Contains(message.Text.ToLower())).Select(x=>x.QuestionId).FirstOrDefault();

      //var questionId = DialogBotData.QuestionsData.FirstOrDefault(x=>x.Questions.Where(x=>x.Contains(message.Text.ToUpper())).Any());

      if (questionId == 0)
      {
        await _botService.Client.SendTextMessageAsync(message.Chat.Id, "Sorry, i don't understand");
        return;
      }

      var result = DialogBotData.AnswerData.Where(x => x.QuestionId == questionId).SingleOrDefault();
      if (result==null)
      {
        await _botService.Client.SendTextMessageAsync(message.Chat.Id, "Sorry, i don't understand");
        return;
      }
      await _botService.Client.SendTextMessageAsync(message.Chat.Id, GetRandomAnswer(result.Answers));
    }

    private string GetRandomAnswer(IList<string> answers)
    {
      var random = new Random();
      return answers[random.Next(answers.Count)];
    }
  }
}
