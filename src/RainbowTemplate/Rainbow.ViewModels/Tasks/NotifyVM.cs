using Rainbow.Common.Enums;
using Yunyong.Core.ViewModels;

namespace Rainbow.ViewModels.Tasks
{
    public class NotifyVM:VMBase
    {
        public NotifyType Type { get; set; }
        public string Title { get; set; }
    }
}