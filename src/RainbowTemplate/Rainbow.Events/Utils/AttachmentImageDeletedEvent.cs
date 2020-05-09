using Yunyong.EventBus.Events;

namespace Rainbow.Events.Utils
{
    public class AttachmentImageDeletedEvent : EventBase
    {
        public string FileName { get; set; }
    }
}