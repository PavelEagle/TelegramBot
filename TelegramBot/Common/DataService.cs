using System.Collections.Concurrent;
using System.IO;
using System.Threading.Tasks;
using TelegramBot.BotData;
using TelegramBot.BotSettings.Enums;

namespace TelegramBot.Common
{
  public static class DataService
  {
    public static async Task LoadAppData()
    {
      CurrentDialogBotData.DialogBotData = await LoadData<DialogBotData>(BotConstants.Paths.DialogData);

      if (CurrentDialogBotData.DialogBotData.QuestionsData != null)
        CurrentDialogBotData.DialogBotData.Count = CurrentDialogBotData.DialogBotData.QuestionsData.Count;

      // initialize if data is empty
      if (CurrentDialogBotData.DialogBotData.Count == 0)
      {
        CurrentDialogBotData.DialogBotData = new DialogBotData() { AnswerData = new ConcurrentDictionary<long, ConcurrentQueue<string>>(), QuestionsData = new ConcurrentDictionary<long, ConcurrentQueue<string>>() };
      }

      ChatSettings.ChatSettingsData = await LoadData<ConcurrentQueue<ChatSettingsBotData>>(BotConstants.Paths.Settings) ?? new ConcurrentQueue<ChatSettingsBotData>();
    }

    public static async Task SaveChatSettings()
    {
      await SaveData(ChatSettings.ChatSettingsData, BotConstants.Paths.Settings);
    }

    public static async Task SaveBotData()
    {
      var difference = CurrentDialogBotData.DialogBotData.QuestionsData.Count - CurrentDialogBotData.DialogBotData.AnswerData.Count;

      // delete unfinished bot data before save
      if (difference > 0)
      {
        for (var i = 0; i < difference; i++)
        {
          CurrentDialogBotData.DialogBotData.QuestionsData.TryRemove(CurrentDialogBotData.DialogBotData.QuestionsData.Count - 1 - i, out var result);
        }
      }
      else if (difference < 0)
      {
        for (var i = 0; i < -difference; i++)
        {
          CurrentDialogBotData.DialogBotData.AnswerData.TryRemove(CurrentDialogBotData.DialogBotData.AnswerData.Count - 1 - i, out var result);
        }
      }

      await SaveData(CurrentDialogBotData.DialogBotData, BotConstants.Paths.DialogData);
    }

    private static async Task SaveData<T>(T data, string filePath)
    {
      await using var tw = new FileStream(filePath, FileMode.Truncate);
      var savedData = Serialize(data);
      await tw.WriteAsync(savedData);
    }

    private static async Task<T> LoadData<T>(string filePath)
    {
      if (!File.Exists(filePath))
      {
        File.Create(filePath);
        return default;
      }

      var answersData = await File.ReadAllBytesAsync(filePath);
      return Deserialize<T>(answersData);
    }

    private static byte[] Serialize<T>(T tData)
    {
      using var ms = new MemoryStream();
      ProtoBuf.Serializer.Serialize(ms, tData);
      return ms.ToArray();
    }

    private static T Deserialize<T>(byte[] tData)
    {
      using var ms = new MemoryStream(tData);
      return ProtoBuf.Serializer.Deserialize<T>(ms);
    }
  }
}
