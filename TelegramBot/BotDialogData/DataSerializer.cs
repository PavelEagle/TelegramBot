using System.Collections.Generic;
using System.IO;

namespace TelegramBot.BotDialogData
{
  public static class DataSerializer
  {
    public static byte[] Serialize<T>(ICollection<T> tData)
    {
      using var ms = new MemoryStream();
      ProtoBuf.Serializer.Serialize(ms, tData);
      return ms.ToArray();
    }

    public static HashSet<T> Deserialize<T>(byte[] tData)
    {
      using var ms = new MemoryStream(tData);
      return ProtoBuf.Serializer.Deserialize<HashSet<T>>(ms);
    }
  }
}
