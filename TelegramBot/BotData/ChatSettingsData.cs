using ProtoBuf;
using TelegramBot.BotSettings.Enums;

namespace TelegramBot.BotData
{
    [ProtoContract]
    public class ChatSettingsBotData
    {
        [ProtoMember(1)] 
        public long ChatId { get; set; }
        [ProtoMember(2)] 
        public string AccountName { get; set; }
        [ProtoMember(3)] 
        public bool VoiceAnswer { get; set; } //voice answer setting
        public int LearningState { get; set; } // 0 - no train, 1 and 2 - train action
        public ActiveCommand ActiveCommand { get; set; } // 0 - no train, 1 and 2 - train action
        public TrainingActions TrainingAction { get; set; } //train actions in enum TrainingActions.cs
        public long CurrentQuestionId { get; set; } // question Id in bot train mode
    }
}