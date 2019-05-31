using Rainbow.Common.Enums;
using Yunyong.Core;

namespace Rainbow.ViewModels.Tasks
{
    public class NotifyQueryOption:PagingQueryOption
    {
        public NotifyType? Type { get; set; }
    }
}