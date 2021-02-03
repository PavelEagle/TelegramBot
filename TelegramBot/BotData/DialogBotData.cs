using ProtoBuf;
using System.Collections.Concurrent;

namespace TelegramBot.BotData
{
    [ProtoContract]
    public class DialogBotData
    {
        [ProtoMember(1)] 
        public ConcurrentDictionary<long, ConcurrentQueue<string>> AnswerData { get; set; }
        [ProtoMember(2)] 
        public ConcurrentDictionary<long, ConcurrentQueue<string>> QuestionsData { get; set; }
        public int Count { get; set; }
    }
}