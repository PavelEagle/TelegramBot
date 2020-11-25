using System.Collections.Generic;
using System.IO;

namespace TelegramBot.BotDialogData
{
  public static class DataSerializer
  {
    public static byte[] Serialize(List<DialogData> tData)
    {
      using var ms = new MemoryStream();
      ProtoBuf.Serializer.Serialize(ms, tData);
      return ms.ToArray();
    }

    public static List<DialogData> Deserialize(byte[] tData)
    {
      using var ms = new MemoryStream(tData);
      return ProtoBuf.Serializer.Deserialize<List<DialogData>>(ms);
    }
  }
}
