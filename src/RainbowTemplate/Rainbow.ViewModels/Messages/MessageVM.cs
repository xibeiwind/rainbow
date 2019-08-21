using System;
using Yunyong.Core.ViewModels;

namespace Rainbow.ViewModels.Messages
{
    public class MessageVM : VMBase
    {
        public string Title { get; set; }
        public string Summary { get; set; }
        public DateTime SendOn { get; set; }
        public Guid SendFromId { get; set; }
        public Guid SendToId { get; set; }
        public string SendFrom { get; set; }
        public string SendTo { get; set; }
    }
}