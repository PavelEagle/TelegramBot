using System.IO;
using System.Threading.Tasks;

namespace TelegramBot.BotDialogData
{
  public static class DataService
  {
    public static async Task SaveData<T>(T data, string filePath)
    {
      using (var tw = new FileStream(filePath, FileMode.Truncate))
      {
        var savedData = Serialize(data);
        await tw.WriteAsync(savedData);
      }
    }

    public static async Task<T> LoadData<T>(string filePath)
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
